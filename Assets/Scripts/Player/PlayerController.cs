using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Transform boat;
    public Transform boatSeat;
    public float maxDistanceToF = 2f;
    private bool onBoat = false;
    public float speed = 5f;
    private Rigidbody2D _rb;
    private Animator _anim;
    private bool canPressF = false;
    private Vector3 boatPositionOffset;
    private BoatController boatController;
    public LayerMask explosionLayerMask;
    public Tilemap breakUnknownTiles;
    public BreakUnknownController breakUnknownPrefabs;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        boatPositionOffset = transform.position - boat.position;
        boatController = boat.GetComponent<BoatController>(); 
    }

    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rb.velocity = movement * speed;
        if (movement != Vector2.zero)
        {
            _anim.SetFloat("moveX", movement.x);
            _anim.SetFloat("moveY", movement.y);
            _anim.SetBool("run", true);
        }
        else
        {
            _anim.SetBool("run", false);
        }

        float distanceToBoat = Vector2.Distance(transform.position, boat.position);

        if (distanceToBoat <= maxDistanceToF && !boatController.IsMoving()) 
        {
            canPressF = true;
        }
        else
        {
            canPressF = false;
        }

        if (canPressF && Input.GetKeyDown(KeyCode.F))
        {
            if (!onBoat)
            {
                transform.position = boatSeat.position;
                boatPositionOffset = transform.position - boat.position;
                onBoat = true;

                if (boatController != null)
                {
                    boatController.StartMoving();
                }
            }
            else
            {
                transform.parent = null;
                transform.position = boat.position + boatPositionOffset;
                onBoat = false;
            }
        }

        if (onBoat)
        {
            transform.position = boat.position + boatPositionOffset;
        }
    }
    private void BreakUnknown(Vector2 position)
    {
        Vector3Int cell = breakUnknownTiles.WorldToCell(position);
        TileBase tile = breakUnknownTiles.GetTile(cell);
        if (tile != null)
        {
            Instantiate(breakUnknownPrefabs, position, Quaternion.identity);
            breakUnknownTiles.SetTile(cell, null);
        }
    }
}
