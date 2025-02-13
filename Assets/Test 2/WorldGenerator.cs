using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public float scale = 0.1f;
    public TilemapManager tilemapManager;
    public BiomeGenerator biomeGenerator;
    public CaveGenerator caveGenerator;
    public PrefabSpawner prefabSpawner;
    public Transform player; // Dodajemy referencję do gracza
    public int chunkWidth = 16; // Szerokość chunka
    public int chunkHeight = 16; // Wysokość chunka
    public int renderDistance = 3; // Odległość renderowania w chunkach

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        GenerateWorld();
    }

    void Update()
    {
        if (player == null) return;

        Vector2Int playerChunk = new Vector2Int(Mathf.FloorToInt(player.position.x / chunkWidth), Mathf.FloorToInt(player.position.z / chunkHeight));

        // Ładowanie chunków
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int chunkPos = new Vector2Int(playerChunk.x + x, playerChunk.y + y);
                if (!activeChunks.ContainsKey(chunkPos))
                {
                    GenerateChunk(chunkPos);
                }
            }
        }

        // Usuwanie chunków
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunk in activeChunks)
        {
            if (Mathf.Abs(chunk.Key.x - playerChunk.x) > renderDistance || Mathf.Abs(chunk.Key.y - playerChunk.y) > renderDistance)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkPos in chunksToRemove)
        {
            Destroy(activeChunks[chunkPos]);
            activeChunks.Remove(chunkPos);
        }
    }

    void GenerateWorld()
    {
        biomeGenerator.GenerateBiomes(); // Generujemy biomy
        caveGenerator.GenerateCaves(); // Generujemy jaskinie

        for (int x = 0; x < width; x += chunkWidth)
        {
            for (int y = 0; y < height; y += chunkHeight)
            {
                GenerateChunk(new Vector2Int(x / chunkWidth, y / chunkHeight));
            }
        }
    }

    void GenerateChunk(Vector2Int chunkPos)
    {
        GameObject chunk = new GameObject("Chunk_" + chunkPos.x + "_" + chunkPos.y);
        chunk.transform.parent = transform; // Ustawiamy WorldGenerator jako rodzica chunka

        Biome currentBiome = biomeGenerator.GetBiomeAt(chunkPos.x * chunkWidth, chunkPos.y * chunkHeight);

        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                int worldX = chunkPos.x * chunkWidth + x;
                int worldY = chunkPos.y * chunkHeight + y;

                if (currentBiome != null)
                {
                    foreach (TileBase tile in currentBiome.biomeTiles)
                    {
                        tilemapManager.SetTile(new Vector3Int(worldX, worldY, 0), tile == tilemapManager.wallTile);
                    }

                    prefabSpawner.SpawnPrefabs(currentBiome, new Vector3(worldX, 0, worldY));
                }
                else
                {
                    float value = Mathf.PerlinNoise(worldX * scale, worldY * scale);
                    bool isWall = value > 0.5f;
                    tilemapManager.SetTile(new Vector3Int(worldX, worldY, 0), isWall);
                }
            }
        }

        activeChunks.Add(chunkPos, chunk);
    }
}