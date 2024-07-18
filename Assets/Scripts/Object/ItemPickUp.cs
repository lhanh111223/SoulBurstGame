using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    private int randomHealth;
    private int randomMana;
    private int randomCoin;
    private int randomAcidNFire;
    private void Start()
    {
        randomHealth = UnityEngine.Random.Range(10, 20);
        randomMana = UnityEngine.Random.Range(20, 50);
        randomCoin = UnityEngine.Random.Range(0, 20);
        randomAcidNFire = UnityEngine.Random.Range(1, 10);
    }
    public enum ItemType
    {
        Health,
        Mana,
        Coin,
        Acid,
        Fire,
    }
    public ItemType type;
    private void OnItemPickUp(GameObject player)
    {
        PlayerHealthBar playerHealthBar = HealthManaManage.Instance.playerHealthBar;
        PlayerManaBar playerManaBar = HealthManaManage.Instance.playerManaBar;
        PlayerCoinBar playerCoinBar = HealthManaManage.Instance.playerCoinBar;
        switch (type)
        {
            case ItemType.Health:
                playerHealthBar.IncreaseHealth(randomHealth);
                Destroy(gameObject);
                break;
            case ItemType.Mana:
                playerManaBar.IncreaseMana(randomMana);
                Destroy(gameObject);
                break;
            case ItemType.Coin:
                playerCoinBar.IncreaseCoin(randomCoin);
                Destroy(gameObject);
                break;
            case ItemType.Acid:
                playerHealthBar.takeDamage(randomAcidNFire);
                break;
            case ItemType.Fire:
                playerHealthBar.takeDamage(randomAcidNFire);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickUp(collision.gameObject);
        }
    }
}
