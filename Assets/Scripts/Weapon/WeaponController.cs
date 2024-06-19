using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePoint;
    public float TimeBetweenFire;
    public float BulletForce;

    float _timeBetweenFire;
    float localScaleY_Weapon;

    // Start is called before the first frame update
    void Start()
    {
        localScaleY_Weapon = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
    }

    // Rotate weapon
    void RotateWeapon()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(transform.localScale.x, -localScaleY_Weapon, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, localScaleY_Weapon, transform.localScale.z);
        }
    }

    
}
