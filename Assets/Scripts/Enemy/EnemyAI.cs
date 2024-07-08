using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    public Transform player;
    public float moveSpeed = 3.0f;
    public float attackCooldown = 1.0f;
    private bool isAttacking = false;
    private bool isPlayerInRange = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (isPlayerInRange)
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                MoveTowardsPlayer();
            }

            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void MoveTowardsPlayer()
    {
        _anim.SetBool("isMoving", true);
        _anim.SetBool("isAttack", false);
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        _rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
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
