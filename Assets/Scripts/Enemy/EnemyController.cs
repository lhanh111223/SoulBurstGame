using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private Animator _anim;
    private Rigidbody2D _rb;
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet1"))
        {
            if (enemyHealth != null)
            {   
                enemyHealth.TakeDamage(10);
                _anim.SetBool("isHit", true);
                StartCoroutine(ResetHitAnimation());
            }
            
        }
    }
    private IEnumerator ResetHitAnimation()
    {
        
        yield return new WaitForSeconds(0.3f);
        _anim.SetBool("isHit", false);
    }
    void Update()
    {
        if (enemyHealth == null)
        {
            Destroy(gameObject);
        }
    }
}
