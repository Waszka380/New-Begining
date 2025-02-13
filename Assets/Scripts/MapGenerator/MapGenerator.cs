using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public float scale = 20f;
    public float offsetX = 0f;
    public float offsetY = 0f;

    public Tilemap tilemap;
    public Tile grassTile;
    public Tile waterTile;
    public Tile lavaTile;
    public Tile sandTile; // Dodajemy Tile dla piasku
    public Tile dirtTile; // Dodajemy Tile dla ziemi
    public Tile rockTile;

    public Transform player;
    public int chunkWidth = 20;
    public int chunkHeight = 20;
    public int chunkDistance = 50;

    private Dictionary<Vector2Int, bool[,]> chunks = new Dictionary<Vector2Int, bool[,]>();

    void Start()
    {
        offsetX = Random.Range(0f, 1000f);
        offsetY = Random.Range(0f, 1000f);

        GenerateMap();
    }

    void Update()
    {
        UpdateChunks();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sampleX = (float)x / width * scale + offsetX;
                float sampleY = (float)y / height * scale + offsetY;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);

                Tile tile = GetTile(perlinValue);
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    Tile GetTile(float value)
    {
        if (value < 0.2f) // Zmieniamy zakres dla wody
        {
            return waterTile;
        }
        else if (value < 0.22f) 
        {
            return sandTile;
        }
        else if (value < 0.6f) 
        {
            return grassTile;
        }
        else if (value < 0.7f)
        {
            return rockTile;
        }
        else if (value < 0.73f)
        {
            return dirtTile;
        }
        else 
        {
            return lavaTile;
        }
    }

    void UpdateChunks()
    {
        Vector2Int playerChunk = new Vector2Int(Mathf.FloorToInt(player.position.x / chunkWidth), Mathf.FloorToInt(player.position.y / chunkHeight));

        for (int x = -chunkDistance / chunkWidth; x <= chunkDistance / chunkWidth; x++)
        {
            for (int y = -chunkDistance / chunkHeight; y <= chunkDistance / chunkHeight; y++)
            {
                Vector2Int chunkPos = playerChunk + new Vector2Int(x, y);
                if (!chunks.ContainsKey(chunkPos))
                {
                    GenerateChunk(chunkPos);
                }
                else
                {
                    ActivateChunk(chunkPos);
                }
            }
        }

        List<Vector2Int> chunksToHide = new List<Vector2Int>();
        foreach (var chunk in chunks)
        {
            if (Mathf.Abs(chunk.Key.x * chunkWidth - player.position.x) > chunkDistance ||
                Mathf.Abs(chunk.Key.y * chunkHeight - player.position.y) > chunkDistance)
            {
                chunksToHide.Add(chunk.Key);
            }
        }

        foreach (var chunkPos in chunksToHide)
        {
            HideChunk(chunkPos);
        }
    }

    void GenerateChunk(Vector2Int chunkPos)
    {
        bool[,] walkable = new bool[chunkWidth, chunkHeight];
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                int worldX = chunkPos.x * chunkWidth + x;
                int worldY = chunkPos.y * chunkHeight + y;

                float sampleX = (float)worldX / width * scale + offsetX;
                float sampleY = (float)worldY / height * scale + offsetY;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);

                Tile tile = GetTile(perlinValue);
                tilemap.SetTile(new Vector3Int(worldX, worldY, 0), tile);

                walkable[x, y] = tile == grassTile || tile == dirtTile || tile == sandTile; // Dodajemy warunek dla dirt i sand
            }
        }
        chunks[chunkPos] = walkable;
    }

    void HideChunk(Vector2Int chunkPos)
    {
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                int worldX = chunkPos.x * chunkWidth + x;
                int worldY = chunkPos.y * chunkHeight + y;
                tilemap.SetTile(new Vector3Int(worldX, worldY, 0), null);
            }
        }
    }

    void ActivateChunk(Vector2Int chunkPos)
    {
        if (chunks.ContainsKey(chunkPos))
        {
            bool[,] walkable = chunks[chunkPos];
            for (int x = 0; x < chunkWidth; x++)
            {
                for (int y = 0; y < chunkHeight; y++)
                {
                    int worldX = chunkPos.x * chunkWidth + x;
                    int worldY = chunkPos.y * chunkHeight + y;

                    float sampleX = (float)worldX / width * scale + offsetX;
                    float sampleY = (float)worldY / height * scale + offsetY;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);

                    Tile tile = GetTile(perlinValue);
                    tilemap.SetTile(new Vector3Int(worldX, worldY, 0), tile);
                }
            }
        }
    }
}