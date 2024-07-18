using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Parameter;

public class BulletController : MonoBehaviour
{
    static GameParameterBulletController _param = new();
    public enum BulletType
    {
        Bullet,
        Lazer,
        Bullet3Aka,
        Bullet4Shotgun
    }

    public BulletType bulletType;
    public WeaponBreakUnknownController weaponBreakController;

    Renderer rend;
    Rigidbody2D rb;
    WeaponController wc;
    int collisionWallCounter;

    // Line Renderer
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        wc = FindAnyObjectByType<WeaponController>();
        collisionWallCounter = 0;

        if (bulletType == BulletType.Lazer)
        {
            lineRenderer = GetComponent<LineRenderer>();
            edgeCollider = GetComponent<EdgeCollider2D>();
        }
        weaponBreakController = FindObjectOfType<WeaponBreakUnknownController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!rend.isVisible && bulletType == BulletType.Bullet)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bulletType != BulletType.Lazer)
        {
            // Enemy
            if (collision.gameObject.tag == "Enemy")
            {
                // Take Damage to enemy
                Destroy(gameObject);
            }
            // Wall
            if (collision.gameObject.tag == "Wall" && bulletType != BulletType.Bullet3Aka)
            {
                rb.AddForce(-transform.right * wc.BulletForce, ForceMode2D.Impulse);
                collisionWallCounter++;
                if (collisionWallCounter == 2)
                {
                    Destroy(gameObject);
                    collisionWallCounter = 0;
                }
            }
            else if ((collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Bullet1") && bulletType == BulletType.Bullet3Aka)
            {
                Destroy(gameObject);
            }
            // Player
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
            // Break Unknown Tile
            if (collision.gameObject.CompareTag("Unknown"))
            {
                Vector2 collisionPoint = collision.GetContact(0).point;
                weaponBreakController.BreakUnknown(collisionPoint);
                Destroy(gameObject);
            }
            
        }
    }
    

}
