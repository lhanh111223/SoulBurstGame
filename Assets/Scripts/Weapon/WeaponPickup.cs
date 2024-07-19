using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum WeaponType
    {
        Weapon1,
        Weapon2
    }

    public WeaponType weaponType;
    bool isPickable = false;
    GameObject player;
    void OnWeaponPickup(GameObject player)
    {
        switch (weaponType)
        {
            case WeaponType.Weapon1:
                player.GetComponent<PickupController>().OnPickupWeapon(1);
                break;
            case WeaponType.Weapon2:
                player.GetComponent<PickupController>().OnPickupWeapon(2);
                break;
        }
    }

    private void Update()
    {
        if (isPickable && Input.GetKeyDown(KeyCode.Q) && player)
        {
            OnWeaponPickup((GameObject)this.player);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.player = collision.gameObject;
            isPickable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPickable = false;
            this.player = null;
        }
    }
}
