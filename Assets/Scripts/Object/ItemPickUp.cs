using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    
    public enum ItemType
    {
        Health,
        Mana,
        Coin,
        Acid,
        Fire,
    }
    public ItemType type;
    //private void OnItemPickUp(GameObject player)
    //{

    //    switch (type)
    //    {
    //        case ItemType.Health:
    //            player.GetComponent<PlayerController>().IncreaseHealth();
    //            break;
    //        case ItemType.Mana:
    //            player.GetComponent<PlayerController>().IncreaseMana();
    //            break;
    //        case ItemType.Gold:
    //            player.GetComponent<PlayerController>().IncreaseGold();
    //            break;
    //    }
    //    Destroy(gameObject);
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        OnItemPickUp(collision.gameObject);
    //    }
    //}
}
