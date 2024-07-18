using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletController;
using Assets.Scripts.Parameter;
public class WeaponController : MonoBehaviour
{
    static GameParameterWeaponController _param = new();

    [Header("Attack")]
    public GameObject Bullet;
    public Transform FirePoint;
    public float TimeBetweenFire;
    public float BulletForce;

    [Header("Lazer")]
    public float LazerLongTime;
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;

    // Time and Type
    float _timeBetweenFire;
    float localScaleY_Weapon;
    GameObject lazer;
    string _bulletType; 
    string _weaponType;

    // Lazer Timer
    float _lazerLongTime;
    // Player HP - Mana
    Player player;
    float angleOffset = 15f;


    // Start is called before the first frame update
    void Start()
    {

        localScaleY_Weapon = transform.localScale.y;
        _bulletType = Bullet.GetComponent<BulletController>().bulletType.ToString();

        lazer = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        lazer.SetActive(false);
        if (_bulletType == _param.BULLET_TYPE_WEAPON2_LAZER)
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
            if (_bulletType == _param.BULLET_TYPE_WEAPON2_LAZER)
            {
                if (player.Mana > 0)
                {
                    lazer.SetActive(true);
                    lazer.GetComponent<LineRenderer>().enabled = true;
                    lineRenderer = lazer.GetComponent<LineRenderer>();
                    FireLazer();
                }
                else
                    lazer.SetActive(false);
            }
            else
            {
                if (player.Mana > 0)
                    FireBullet();
            }
        }
        else
            lazer.SetActive(false);
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
        if (_bulletType == _param.BULLET_TYPE_WEAPON3_AKA)
        {
            Vector3 firePointPosition1 = FirePoint.position + FirePoint.right * 1 / 2;
            Vector3 firePointPosition2 = FirePoint.position;
            player.DecreaseMana(10);
            GameObject bulletTmp1 = Instantiate(Bullet, firePointPosition1, Quaternion.identity);
            Rigidbody2D rb1 = bulletTmp1.GetComponent<Rigidbody2D>();
            bulletTmp1.transform.rotation = transform.rotation;
            rb1.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
            GameObject bulletTmp2 = Instantiate(Bullet, firePointPosition2, Quaternion.identity);
            Rigidbody2D rb2 = bulletTmp2.GetComponent<Rigidbody2D>();
            bulletTmp1.transform.rotation = transform.rotation;
            rb2.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
        }
        else if (_bulletType == _param.BULLET_TYPE_WEAPON4_SHOTGUN)
        {
            // Tạo viên đạn ở giữa
            GameObject bulletCenter = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rbCenter = bulletCenter.GetComponent<Rigidbody2D>();
            rbCenter.velocity = FirePoint.right * BulletForce;

            // Tạo viên đạn bên trái
            GameObject bulletLeft = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
            rbLeft.velocity = Quaternion.Euler(0, 0, angleOffset) * FirePoint.right * BulletForce;

            // Tạo viên đạn bên phải
            GameObject bulletRight = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
            rbRight.velocity = Quaternion.Euler(0, 0, -angleOffset) * FirePoint.right * BulletForce;
        }
        else
        {
            GameObject bulletCenter = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rbCenter = bulletCenter.GetComponent<Rigidbody2D>();
            rbCenter.velocity = FirePoint.right * BulletForce;
        }

    }



    //Lazer
    void FireLazer()
    {
        if (player.Mana <= 0) return;

        _lazerLongTime -= Time.deltaTime;
        if (_lazerLongTime < 0)
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
        // Lấy số lượng điểm từ Line Renderer
        int pointCount = lineRenderer.positionCount;

        // Tạo một mảng các điểm Vector2
        Vector2[] points = new Vector2[pointCount];

        // Chuyển đổi các điểm từ Vector3 sang Vector2 và offset bởi FirePoint
        for (int i = 0; i < pointCount; i++)
        {
            Vector3 lineRendererPos = lineRenderer.GetPosition(i);
            points[i] = new Vector2(lineRendererPos.x - FirePoint.position.x, lineRendererPos.y - FirePoint.position.y);
        }

        // Cập nhật các điểm cho Edge Collider 2D
        edgeCollider.points = points;

        // Cập nhật vị trí của collider để khớp với FirePoint
        edgeCollider.transform.position = FirePoint.position;
    }

}