using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoTile : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles; // Lista kafelków do autotilingu

    void Start()
    {
        AutoTileMap();
    }

    public void AutoTileMap()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase currentTile = tilemap.GetTile(cellPosition);

                if (currentTile != null && ContainsTile(currentTile)) // Sprawdzamy, czy dany kafelek jest na liście
                {
                    int index = System.Array.IndexOf(tiles, currentTile);
                    Tile tileToSet = GetTileToSet(cellPosition, index);
                    tilemap.SetTile(cellPosition, tileToSet);
                }
            }
        }
    }

    bool ContainsTile(TileBase tile)
    {
        foreach (Tile t in tiles)
        {
            if (t == tile)
            {
                return true;
            }
        }
        return false;
    }

    Tile GetTileToSet(Vector3Int position, int index)
    {
        // Utwórz maskę bitową na podstawie sąsiednich kafelków
        int mask = 0;
        mask |= HasTile(position + new Vector3Int(0, 1, 0), index) ? 1 : 0; // Góra
        mask |= HasTile(position + new Vector3Int(1, 0, 0), index) ? 2 : 0; // Prawo
        mask |= HasTile(position + new Vector3Int(0, -1, 0), index) ? 4 : 0; // Dół
        mask |= HasTile(position + new Vector3Int(-1, 0, 0), index) ? 8 : 0; // Lewo

        // Wybierz odpowiedni kafelek na podstawie maski bitowej
        switch (mask)
        {
            case 0: return tiles[index]; // Izolowany kafelek
            case 1: return tiles[index + 1]; // Linia pionowa
            case 2: return tiles[index + 2]; // Linia pozioma
            case 3: return tiles[index + 3]; // Kąt górny prawy
            case 4: return tiles[index + 4]; // Kąt dolny prawy
            case 5: return tiles[index + 5]; // Kąt dolny lewy
            case 6: return tiles[index + 6]; // Kąt górny lewy
            case 7: return tiles[index + 7]; // Trójnik górny
            case 8: return tiles[index + 8]; // Trójnik prawy
            case 9: return tiles[index + 9]; // Trójnik dolny
            case 10: return tiles[index + 10]; // Trójnik lewy
            case 11: return tiles[index + 11]; // Czwórnik
            default: return tiles[index]; // Domyślny kafelek
        }
    }

    bool HasTile(Vector3Int position, int index)
    {
        TileBase tile = tilemap.GetTile(position);
        return tile != null && tile == tiles[index];
    }
}