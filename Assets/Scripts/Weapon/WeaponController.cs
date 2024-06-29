using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePoint;
    public float TimeBetweenFire;
    public float BulletForce;
    public int LazerLength;
    //public LineRenderer lineRenderer;
    public float maxLaserDistance = 100f;  // Khoảng cách tối đa của tia laser

    LineRenderer lineRenderer;
    float _timeBetweenFire;
    float localScaleY_Weapon;
    string _bulletType;
    GameObject lazer;


    // Start is called before the first frame update
    void Start()
    {

        localScaleY_Weapon = transform.localScale.y;
        _bulletType = Bullet.GetComponent<BulletController>().bulletType.ToString();
        lazer = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        lazer.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
        _timeBetweenFire -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _timeBetweenFire < 0)
        {
            if (_bulletType == "Lazer")
            {
                lazer.SetActive(true);
                lazer.GetComponent<LineRenderer>().enabled = true;
                lineRenderer = lazer.GetComponent<LineRenderer>();
                FireWithLazerLength();
            }
            else
            {
                FireBullet();
            }

        }
        else
        {
            lazer.SetActive(false);
        }
        
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
    // Fire bullet|lazer
    void FireBullet()
    {
        _timeBetweenFire = TimeBetweenFire;
        // Effect
        if (_bulletType == "Lazer")
        {
            //FireWithLazerLength();
        }
        else
        {
            GameObject bulletTmp = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            bulletTmp.transform.rotation = transform.rotation;
            rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
        }
    }

    // Lazer 
    void FireWithLazerLength()
    {
        RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, FirePoint.right);
        lineRenderer.SetPosition(0, FirePoint.position);

        if (Input.GetMouseButton(0))
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, FirePoint.position + FirePoint.right * 100);
        }
    }


}
