using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public PlayerHealthBar PlayerHealthBar;
    public int bulletDamage = 5;
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthBar.takeDamage(bulletDamage);
        }
        
    }
}
