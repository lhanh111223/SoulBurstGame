using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving && currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector3 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    isMoving = false;
                }
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        currentWaypointIndex = 0;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
