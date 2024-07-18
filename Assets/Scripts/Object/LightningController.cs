using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    PlayerHealthBar playerHealthBar;
    private void Start()
    {
        playerHealthBar = GetComponent<PlayerHealthBar>();
        playerHealthBar = FindObjectOfType<PlayerHealthBar>();
        if (playerHealthBar != null)
        {
            playerHealthBar = playerHealthBar.GetComponent<PlayerHealthBar>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealthBar.takeDamage(10);
        }
    }
    
}
