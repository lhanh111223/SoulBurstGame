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
    private bool boardKeyPressed = false; // Biến theo dõi số lần phím F đã được nhấn

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
        player.transform.position = boatSeat.position; // Đặt vị trí của người chơi lên vị trí ghế ngồi trên thuyền
        player.GetComponent<Collider2D>().isTrigger = true; // Kích hoạt trigger của người chơi
        playerOnBoat = true; // Đánh dấu rằng người chơi đã lên thuyền
        boardKeyPressed = true; // Đánh dấu rằng phím F đã được nhấn
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
            player.GetComponent<Collider2D>().isTrigger = false; // Tắt trigger của người chơi
            playerOnBoat = false;
            isMoving = false;
        }
    }

    void SyncPlayerWithBoat()
    {
        if (playerOnBoat && player != null)
        {
            player.transform.position = boatSeat.position; // Đồng bộ vị trí của người chơi với vị trí ghế ngồi trên thuyền
        }
    }
}
