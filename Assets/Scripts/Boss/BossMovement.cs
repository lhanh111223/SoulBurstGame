using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Transform player;
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    public float moveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        seeker = GetComponent<Seeker>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void CalculatePath()
    {


        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, player.position, OnPathCallBack);

        }

    }
    private void OnPathCallBack(Path pathCalculated)
    {
        if (pathCalculated.error)
        {
            return;
        }
        path = pathCalculated;

        // move to target
        MoveToTarget();

    }

    private void MoveToTarget()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector3 force = direction * moveSpeed * Time.deltaTime;
            transform.position += force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);

            if (distance < 0.5)
            {
                currentWP++;
            }
            if (force.x != 0)
            {
                if (force.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 0);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 0);
                }
            }
            yield return null;
        }
    }
}
