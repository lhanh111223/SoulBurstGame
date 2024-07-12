using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Tilemap doorTilemap;
    public GameObject enemyPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    private GameObject[] enemyInstances = new GameObject[2];

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (doorTilemap != null)
            {
                doorTilemap.gameObject.SetActive(true);
            }

            if (enemyInstances[0] == null)
            {
                SpawnEnemies();
            }
            else
            {
                foreach (GameObject enemy in enemyInstances)
                {
                    if (enemy != null)
                    {
                        enemy.SetActive(true);
                    }
                }
            }
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyInstances.Length; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0
            );

            enemyInstances[i] = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        // Kiểm tra nếu tất cả các enemy đã bị tiêu diệt
        bool allEnemiesDestroyed = true;

        foreach (GameObject enemy in enemyInstances)
        {
            if (enemy != null)
            {
                allEnemiesDestroyed = false;
                break;
            }
        }

        if (allEnemiesDestroyed && doorTilemap != null)
        {
            doorTilemap.gameObject.SetActive(false);
        }
    }
}
