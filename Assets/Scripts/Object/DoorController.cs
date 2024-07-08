using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    public Tilemap doorTilemap; 
    public GameObject enemyPrefab; 
    private GameObject enemyInstance;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            doorTilemap.gameObject.SetActive(true);
            if (enemyInstance == null)
            {
                enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                enemyInstance.SetActive(true);
            }
        }
    }
}
