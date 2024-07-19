using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserController : MonoBehaviour
{
    
    public int laserDamage = 5;
    public PlayerHealthBar PlayerHealthBar;

    void Start()
    {
        PlayerHealthBar = FindObjectOfType<PlayerHealthBar>();
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar = PlayerHealthBar.GetComponent<PlayerHealthBar>();
        }
    }
    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthBar.takeDamage(laserDamage);
        }
    }
}
