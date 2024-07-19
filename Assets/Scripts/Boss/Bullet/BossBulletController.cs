using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public PlayerHealthBar PlayerHealthBar;
    public int bulletDamage = 5;
    public float speed = 100f;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthBar = FindObjectOfType<PlayerHealthBar>();
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar = PlayerHealthBar.GetComponent<PlayerHealthBar>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthBar.takeDamage(bulletDamage);
        }
        
    }
}
