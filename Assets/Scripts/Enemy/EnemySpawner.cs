using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Tilemap doorTilemap;
    public GameObject[] enemyPrefabs;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    private GameObject[] enemyInstances;
    private bool eventTriggered = false;

    private void Start()
    {
        enemyInstances = new GameObject[enemyPrefabs.Length * 2];
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !eventTriggered)
        {
            eventTriggered = true;

            if (doorTilemap != null)
            {
                doorTilemap.gameObject.SetActive(true);
            }
            if (enemyInstances[0] == null)
            {
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies()
    {
        int index = 0;
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            for (int i = 0; i < 1; i++)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    0
                );

                enemyInstances[index] = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                index++;
            }
        }
    }

    void Update()
    {
        bool allEnemiesDestroyed = true;

        foreach (GameObject enemy in enemyInstances)
        {
            if (enemy != null)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null && enemyHealth.currentHealth > 0)
                {
                    allEnemiesDestroyed = false;
                }
            }
        }

        if (allEnemiesDestroyed && doorTilemap != null)
        {
            doorTilemap.gameObject.SetActive(false);
        }
    }
}
