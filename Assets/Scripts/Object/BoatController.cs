using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform boat;
    public Transform boatSeat;
    public KeyCode boardKey = KeyCode.F;

    public Transform destinationPoint;
    public float speed = 5f;

    private bool isPlayerNearby = false;
    private GameObject player;
    private bool playerOnBoat = false;
    private bool isMoving = false;
    private bool boardKeyPressed = false; 

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(boardKey) && !boardKeyPressed)
        {
            BoardBoat();
        }

        if (isMoving)
        {
            MoveToDestination();
            SyncPlayerWithBoat();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }

    void BoardBoat()
    {
        player.transform.position = boatSeat.position; 
        player.GetComponent<Collider2D>().isTrigger = true; 
        playerOnBoat = true; 
        boardKeyPressed = true; 
        StartMoving();
    }

    void StartMoving()
    {
        isMoving = true;
    }

    void MoveToDestination()
    {
        float step = speed * Time.deltaTime;
        boat.position = Vector3.MoveTowards(boat.position, destinationPoint.position, step);

        if (boat.position == destinationPoint.position)
        {
            isMoving = false;
            ExitBoat();
        }
    }

    void ExitBoat()
    {
        if (player != null)
        {
            player.transform.position = boatSeat.position;
            player.GetComponent<Collider2D>().isTrigger = false; 
            playerOnBoat = false;
            isMoving = false;
        }
    }

    void SyncPlayerWithBoat()
    {
        if (playerOnBoat && player != null)
        {
            player.transform.position = boatSeat.position; 
        }
    }
}
