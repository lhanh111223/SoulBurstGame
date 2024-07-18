using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeaponBreakUnknownController : MonoBehaviour
{
    public BreakUnknownController breakUnknownPrefabs;
    public Tilemap breakUnknownTiles;

    void Start()
    {
        GameObject tilemapObject = GameObject.FindWithTag("Unknown");
        if (tilemapObject != null)
        {
            breakUnknownTiles = tilemapObject.GetComponent<Tilemap>();
        }
    }
    public void BreakUnknown(Vector2 position)
    {
        if (breakUnknownTiles == null)
        {
           
            return;
        }

        Vector3Int cell = breakUnknownTiles.WorldToCell(position);
        TileBase tile = breakUnknownTiles.GetTile(cell);
        if (tile != null)
        {
            Instantiate(breakUnknownPrefabs, position, Quaternion.identity);
            breakUnknownTiles.SetTile(cell, null);
        }
    }
}
