using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VegetationGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile grassTile;

    public GameObject treePrefab;
    public GameObject smallGrassPrefab;
    public GameObject bushPrefab;

    [Range(0, 1)] public float treeDensity = 0.1f;
    [Range(0, 1)] public float grassDensity = 0.5f;
    [Range(0, 1)] public float bushDensity = 0.2f;

    public float treeScale = 1f;
    public float grassScale = 0.5f;
    public float bushScale = 0.7f;


    public void GenerateVegetation()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile == grassTile)
                {
                    GenerateTree(tilePosition);
                    GenerateGrass(tilePosition);
                    GenerateBush(tilePosition);
                }
            }
        }
    }

    void GenerateTree(Vector3Int tilePosition)
    {
        if (Random.value < treeDensity)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            GameObject tree = Instantiate(treePrefab, worldPosition, Quaternion.identity);
            tree.transform.localScale = Vector3.one * treeScale;
            tree.transform.parent = transform; // Ustawiamy VegetationGenerator jako rodzica
        }
    }

    void GenerateGrass(Vector3Int tilePosition)
    {
        if (Random.value < grassDensity)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            GameObject grass = Instantiate(smallGrassPrefab, worldPosition, Quaternion.identity);
            grass.transform.localScale = Vector3.one * grassScale;
            grass.transform.parent = transform; // Ustawiamy VegetationGenerator jako rodzica

        }
    }

    void GenerateBush(Vector3Int tilePosition)
    {
        if (Random.value < bushDensity)
        {
            Vector3 worldPosition = tilemap.GetCellCenterWorld(tilePosition);
            GameObject bush = Instantiate(bushPrefab, worldPosition, Quaternion.identity);
            bush.transform.localScale = Vector3.one * bushScale;
            bush.transform.parent = transform; // Ustawiamy VegetationGenerator jako rodzica
        }
    }
}