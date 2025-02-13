using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaveGenerator : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public float fillPercent = 0.5f;
    public int smoothIterations = 5;
    public TilemapManager tilemapManager;

    void Start()
    {
        GenerateCaves();
    }

    public void GenerateCaves()
    {
        bool[,] map = new bool[width, height];

        // Wypełnianie mapy losowymi wartościami
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = Random.value < fillPercent;
            }
        }

        // Wygładzanie mapy
        for (int i = 0; i < smoothIterations; i++)
        {
            map = SmoothMap(map);
        }

        // Rysowanie jaskiń na tilemapie
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y])
                {
                    tilemapManager.SetTile(new Vector3Int(x, y, 0), true);
                }
            }
        }
    }

    bool[,] SmoothMap(bool[,] map)
    {
        // ... (implementacja algorytmu wygładzania)
        return map;
    }
}