using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f; 
    public float nextWPDistance = 1f;
    public SpriteRenderer characterSR;
    private Rigidbody2D _rb;
    private Animator _anim;
    public Transform player;

    public Seeker seeker;
    Path path;
    Coroutine moveCoutine;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        if (player != null)
        {
            InvokeRepeating("CalculatePath", 0f, 1f);
        }
        
    }

    void CalculatePath()
    {
        if (seeker.IsDone() && player != null)
        {
            seeker.StartPath(transform.position, player.position, OnPathCallback);
        }
        
    }

    void OnPathCallback(Path p)
    {
        if (p.error)
        {
            return;
        }
        path = p;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoutine != null)
        {
            StopCoroutine(moveCoutine);
        }
        moveCoutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        while (path != null && currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector3 force = direction * moveSpeed * Time.deltaTime;
            transform.position += force;
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);

            if (distance < nextWPDistance) 
            {
                currentWP++;
                
                if (currentWP >= path.vectorPath.Count)
                {
                    yield break;
                }
            }

            
            if (force.x != 0)
            {
                characterSR.flipX = force.x < 0; 
            }

            yield return null;
        }
    }

}
