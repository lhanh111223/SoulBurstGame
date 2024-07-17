using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Parameter;

public class WeaponPickup : MonoBehaviour
{
    static GameParameterWeaponPickup _param = new();

    public enum WeaponType
    {
        Weapon1, // Normal gun
        Weapon2, // Lazer gun
        Weapon3, // Aka gun
        Weapon4 // shotgun 3 bullets
    }

    public WeaponType weaponType;
    bool isPickable = false;
    GameObject player;
    void OnWeaponPickup(GameObject player)
    {
        string s = _param.WEAPON_TYPE_SHOTGUN;
        switch (weaponType)
        {
            case WeaponType.Weapon1:
                player.GetComponent<PickupController>().OnPickupWeapon(1);
                break;
            case WeaponType.Weapon2:
                player.GetComponent<PickupController>().OnPickupWeapon(2);
                break;
            case WeaponType.Weapon3:
                player.GetComponent<PickupController>().OnPickupWeapon(3);
                break;
            case WeaponType.Weapon4:
                player.GetComponent<PickupController>().OnPickupWeapon(4);
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
