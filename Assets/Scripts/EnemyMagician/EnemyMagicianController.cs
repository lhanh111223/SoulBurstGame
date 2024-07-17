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
    public float nextattackTime = 0f;

    [SerializeField]
    private float moveSpeed = 2f;
    public float moveDuration = 5f;
    private bool hasMoved = false;
    private Facetoplayer faceToPlayer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        faceToPlayer = GetComponent<Facetoplayer>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        StartCoroutine(MoveAndPerformAction());
    }

    public IEnumerator PerformAttackSequence()
    {
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("isSkill", true);

        yield return new WaitForSeconds(2f);
        _anim.SetBool("isSkill", false);

        _anim.SetBool("isTele", true);

        _anim.SetBool("isAppear", true);
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

    IEnumerator MoveAndPerformAction()
    {
        float startTime = Time.time;
        while (Time.time - startTime < moveDuration)
        {
            if (player != null)
            {
                MoveTowardsPlayer();
                UpdateLocalScale();
            }
            yield return null;
        }

        hasMoved = true;
        _anim.SetBool("isWalk", false);
        StartCoroutine(PerformAttackSequence());
    }

    void MoveTowardsPlayer()
    {
        _anim.SetBool("isWalk", true);
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        Vector2 newPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        _rb.MovePosition(newPosition);
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
