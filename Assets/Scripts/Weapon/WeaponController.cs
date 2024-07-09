using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletController;

public class WeaponController : MonoBehaviour
{
    [Header("Attack")]
    public GameObject Bullet;
    public Transform FirePoint;
    public float TimeBetweenFire;
    public float BulletForce;
    public int LazerLength;

    [Header("Lazer")]
    public float LazerLongTime;
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;

    // Time and Type
    float _timeBetweenFire;
    float localScaleY_Weapon;
    string _bulletType;
    GameObject lazer;

    // Lazer Timer
    float _lazerLongTime;
    // Player HP - Mana
    Player player;



    // Start is called before the first frame update
    void Start()
    {

        localScaleY_Weapon = transform.localScale.y;
        _bulletType = Bullet.GetComponent<BulletController>().bulletType.ToString();
        lazer = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        lazer.SetActive(false);
        if (_bulletType == "Lazer")
        {
            lineRenderer = lazer.GetComponent<LineRenderer>();
            edgeCollider = lazer.GetComponent<EdgeCollider2D>();
            _lazerLongTime = LazerLongTime;
        }
        player = FindAnyObjectByType<Player>();
        
    }

    public GameObject getLazer()
    {
        return lazer;
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
                if(player.Mana > 0)
                {
                    lazer.SetActive(true);
                    lazer.GetComponent<LineRenderer>().enabled = true;
                    lineRenderer = lazer.GetComponent<LineRenderer>();
                    FireWithLazerLength();
                }
                else
                {
                    lazer.SetActive(false);
                }
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
        player.DecreaseMana(10);

        _timeBetweenFire = TimeBetweenFire;
        GameObject bulletTmp = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        bulletTmp.transform.rotation = transform.rotation;
        rb.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);

    }

    //Lazer
    void FireWithLazerLength()
    {
        if (player.Mana <= 0) return;

        _lazerLongTime -= Time.deltaTime;
        if(_lazerLongTime < 0)
        {
            player.DecreaseMana(1);
            _lazerLongTime = LazerLongTime;
        }

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
        UpdateCollider();
    }



    void UpdateCollider()
    {
        if (lineRenderer.enabled)
        {
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);

            Vector2[] colliderPoints = new Vector2[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                colliderPoints[i] = new Vector2(positions[i].x, positions[i].y);
            }

            edgeCollider.points = colliderPoints;
        }
        else
        {
            edgeCollider.points = new Vector2[0]; // Clear the collider points if the laser is not active
        }
    }

}