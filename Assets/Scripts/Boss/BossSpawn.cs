using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossSpawn : MonoBehaviour
{
    public Tilemap doorTilemap;
    public GameObject Boss;
    public GameObject HealthBarBoss;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (doorTilemap != null)
            {
                doorTilemap.gameObject.SetActive(true);
                Boss.SetActive(true);
                HealthBarBoss.SetActive(true);
            }

            
        }
    }
}
