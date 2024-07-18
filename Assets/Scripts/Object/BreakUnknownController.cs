
using System.Collections.Generic;
using UnityEngine;


public class BreakUnknownController : MonoBehaviour
{
    public float breakUnknownTime = 1f;
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;

    public GameObject[] spawnableItem;
    private GameObject itemCreated;

    void Start()
    {
        Destroy(gameObject, breakUnknownTime);
    }
    
    private void OnDestroy()
    {
        if (spawnableItem.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItem.Length);
            float roundedX = Mathf.Round(transform.position.x)+0.5f;
            float roundedY = Mathf.Round(transform.position.y)+0.5f;
            Vector3 spawnPosition = new Vector3(roundedX, roundedY, transform.position.z);
            itemCreated = Instantiate(spawnableItem[randomIndex], spawnPosition, Quaternion.identity);
            if(itemCreated.name.Contains("Acid") || itemCreated.name.Contains("Fire")) 
            {
                Destroy(itemCreated, 3.5f);
            }
        }
    }
    
}
