using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMagicianController : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    private Rigidbody2D _rb;
    public GameObject lightningPrefab;
    public float attackCooldown = 1f;
    public float distanceBehindPlayer = 1f;
    private Transform player;

    [SerializeField]
    public float moveSpeed = 2f;
    public float moveDuration = 3f;
    public float attackDistance = 1.5f;

    private bool hasAttacked = false;
    private bool isPerformingSkill = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        StartCoroutine(ActionRoutine());
    }

    private IEnumerator ActionRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(MoveAndAttackPlayer());
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(PerformAttackSequence());
        }
    }

    public IEnumerator PerformAttackSequence()
    {
        isPerformingSkill = true;

        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("isSkill", true);

        yield return new WaitForSeconds(2f);
        _anim.SetBool("isSkill", false);

        _anim.SetBool("isTele", true);
        _anim.SetBool("isAppear", true);

        yield return new WaitForSeconds(4f);
        _anim.SetBool("isTele", false);
        _anim.SetBool("isAppear", false);

        isPerformingSkill = false;
    }

    void TeleportBehindPlayer()
    {
        if (player != null)
        {
            UpdateLocalScale();
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 teleportPosition = player.position - directionToPlayer * distanceBehindPlayer;
            transform.position = teleportPosition;
        }
    }

    public void CastLightningSkill()
    {
        if (player != null)
        {
            UpdateLocalScale();
            Vector3 lightningPosition = new Vector3(player.position.x, player.position.y + 0.1f, player.position.z);
            GameObject lightningInstance = Instantiate(lightningPrefab, lightningPosition, Quaternion.identity);
            Destroy(lightningInstance, 1f);
        }
    }

    IEnumerator MoveAndAttackPlayer()
    {
        float startTime = Time.time;
        while (Time.time - startTime < moveDuration && !isPerformingSkill)
        {
            if (player != null)
            {
                MoveTowardsPlayer();
                UpdateLocalScale();
            }
            yield return null;
        }

        _anim.SetBool("isWalk", false);
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer > attackDistance)
        {
            _anim.SetBool("isWalk", true);
            Vector2 newPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
            _rb.MovePosition(newPosition);
            _anim.SetBool("isAttack", false);
            hasAttacked = false;
        }
        else
        {
            _anim.SetBool("isWalk", false);
            if (!hasAttacked && !isPerformingSkill)
            {
                _anim.SetBool("isAttack", true);
                hasAttacked = true; 
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _anim.SetBool("isAttack", false);
        hasAttacked = false;
    }

    void UpdateLocalScale()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
