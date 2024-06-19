using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupZone : MonoBehaviour
{
    public GameObject[] PickableWeapons;

    private void Start()
    {
        Vector3 offset = new Vector3(0, -1, 0);
        foreach(var weapon in PickableWeapons)
        {
            offset += new Vector3(0, 2, 0);
            Instantiate(weapon, transform.position + offset, Quaternion.identity);
        }
    }
}
