using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform boat; // Boat transform để di chuyển player lên thuyền
    public Transform boatSeat; // Vị trí trên thuyền mà player sẽ ngồi
    public KeyCode boardKey = KeyCode.F; // Phím để lên thuyền

    public Transform destinationPoint; // Điểm đến của thuyền
    public float speed = 5f; // Tốc độ di chuyển của thuyền

    private bool isPlayerNearby = false; // Kiểm tra xem player có ở gần thuyền không
    private GameObject player; // Đối tượng player
    private bool playerOnBoat = false; // Biến cờ để kiểm tra xem player đã lên thuyền chưa
    private bool reachedDestination = false; // Biến cờ để kiểm tra xem thuyền đã đến điểm đến chưa
    private bool isMoving = false; // Biến cờ để kiểm tra xem thuyền đang di chuyển đến điểm đến hay không

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(boardKey) && !isMoving && !playerOnBoat)
        {
            StartMoving();
        }

        if (isMoving)
        {
            MoveToDestination();
            SyncPlayerWithBoat(); // Đồng bộ hóa vị trí của player với thuyền
        }
        else if (isPlayerNearby && Input.GetKeyDown(boardKey) && playerOnBoat)
        {
            ExitBoat();
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

    void StartMoving()
    {
        isMoving = true; // Bắt đầu di chuyển thuyền đến điểm đến
        reachedDestination = false; // Đặt lại trạng thái reachedDestination
    }

    void MoveToDestination()
    {
        float step = speed * Time.deltaTime;
        boat.position = Vector3.MoveTowards(boat.position, destinationPoint.position, step);

        if (boat.position == destinationPoint.position)
        {
            reachedDestination = true;
            isMoving = false; // Dừng di chuyển khi đến điểm đến
        }
    }

    void ExitBoat()
    {
        if (player != null)
        {
            player.transform.position = boatSeat.position; // Di chuyển player lên vị trí ngồi trên thuyền
            // Tắt hoặc vô hiệu hóa các script di chuyển của player nếu cần
            playerOnBoat = false; // Đánh dấu là player đã rời khỏi thuyền
            reachedDestination = false; // Cho phép thuyền di chuyển đến điểm đích tiếp theo nếu cần
            isMoving = false; // Dừng di chuyển thuyền nếu người chơi rời khỏi
        }
    }

    void SyncPlayerWithBoat()
    {
        if (playerOnBoat && player != null)
        {
            Vector3 offset = boat.position - transform.position;
            player.transform.position += offset;
        }
    }
}
