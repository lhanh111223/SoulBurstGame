using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum BulletType
    {
        Bullet,
        Lazer
    }

    public BulletType bulletType;

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
            UpdateCollider();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rend.isVisible && bulletType == BulletType.Bullet)
        {
            Destroy(gameObject);
        }
        

        if (bulletType == BulletType.Lazer)
        {
            UpdateCollider();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bulletType == BulletType.Bullet)
        {
            // Enemy
            if (collision.gameObject.tag == "Enemy")
            {
                // Take Damage to enemy
                Destroy(gameObject);
            }
            // Wall
            if (collision.gameObject.tag == "Wall")
            {
                rb.AddForce(-transform.right * wc.BulletForce, ForceMode2D.Impulse);
                collisionWallCounter++;
                if (collisionWallCounter == 2)
                {
                    Destroy(gameObject);
                    collisionWallCounter = 0;
                }

            }
            // Player
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }

    void UpdateCollider()
    {
        if (lineRenderer == null || edgeCollider == null) return;
        gameObject.transform.position = wc.getLazer().transform.position;
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        Vector2[] colliderPoints = new Vector2[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            colliderPoints[i] = new Vector2(positions[i].x, positions[i].y);
        }

        edgeCollider.points = colliderPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.Lazer)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                // Take Damage to enemy
                Debug.Log("Hit Enemy");
            }
        }
    }
}
