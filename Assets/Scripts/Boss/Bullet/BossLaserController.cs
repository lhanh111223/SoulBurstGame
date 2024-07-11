using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserController : MonoBehaviour
{
    public Transform boss;
    public Transform player;
    public SpriteRenderer laserRenderer; 
    public float laserWidth = 0.1f; 

    void Update()
    {
        if (boss != null && player != null)
        {
            Vector3 bossPosition = boss.position;
            Vector3 playerPosition = player.position;

            float distance = Vector3.Distance(bossPosition, playerPosition);

            AdjustLaserLength(distance, bossPosition, playerPosition);
        }
    }

    void AdjustLaserLength(float length, Vector3 startPosition, Vector3 endPosition)
    {
        transform.position = startPosition;

        Vector3 direction = (endPosition - startPosition).normalized;

        transform.rotation = Quaternion.LookRotation(direction);

        laserRenderer.size = new Vector2(length, laserWidth);
    }
}
