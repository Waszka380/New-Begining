using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap wallTilemap;
    public TileBase groundTile;
    public TileBase wallTile;

    public void SetTile(Vector3Int position, bool isWall)
    {
        Tilemap tilemap = isWall ? wallTilemap : groundTilemap;
        TileBase tile = isWall ? wallTile : groundTile;
        tilemap.SetTile(position, tile);
    }

    public bool IsTileWalkable(Vector3Int position)
    {
        return !wallTilemap.HasTile(position);
    }
}