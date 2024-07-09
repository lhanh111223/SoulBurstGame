using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float nextWPDistance = 1f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public SpriteRenderer characterSR;
    private Rigidbody2D _rb;
    private Animator _anim;
    public Transform player;

    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool isAttacking = false;
    bool isPlayerInRange = false;

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
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        while (path != null && currentWP < path.vectorPath.Count)
        {
            if (isAttacking)
            {
                yield return null;
                continue;
            }

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

            float playerDistance = Vector2.Distance(transform.position, player.position);
            if (playerDistance < attackRange)
            {
                isPlayerInRange = true;
                StartCoroutine(AttackPlayer());
            }
            else
            {
                isPlayerInRange = false;
                _anim.SetBool("isMoving", true);
                _anim.SetBool("isAttack", false);
            }

            yield return null;
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        _anim.SetBool("isMoving", false);
        _anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
            _anim.SetBool("isAttack", true);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
            _anim.SetBool("isAttack", false);
        }
    }
}
