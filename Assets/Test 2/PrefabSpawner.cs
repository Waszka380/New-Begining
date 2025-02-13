using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Biome[] biomes;

    public void SpawnPrefabs(Biome biome, Vector3 position)
    {
        foreach (GameObject prefab in prefabs)
        {
            // ... (sprawdzanie, czy prefab pasuje do biomu)
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}