using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private PlayerCoinBar playerCoinBar;

    void Start()
    {
        currentHealth = maxHealth;
        playerCoinBar = FindObjectOfType<PlayerCoinBar>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        int coinAmount = Random.Range(1, 11);

        if (playerCoinBar != null)
        {
            playerCoinBar.IncreaseCoin(coinAmount);
        }
        Destroy(gameObject);
    }
}
