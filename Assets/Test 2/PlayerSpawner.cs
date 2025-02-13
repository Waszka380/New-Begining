using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab gracza do spawnerowania
    public Transform spawnPoint; // Punkt, w którym gracz ma się pojawić (opcjonalne)

    private Vector3 spawnPosition; // Zmienna do przechowywania pozycji spawnu

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Debug.Log("Funkcja SpawnPlayer() została wywołana.");

        if (spawnPoint != null)
        {
            // Użyj zdefiniowanego punktu spawnu, jeśli istnieje
            spawnPosition = spawnPoint.position;
            Debug.Log("Używam punktu spawnu: " + spawnPosition);
        }
        else
        {
            // W przeciwnym razie, znajdź środek mapy

            // Załóżmy, że mapa jest reprezentowana przez obiekt z komponentem Terrain lub Tilemap
            Terrain terrain = FindObjectOfType<Terrain>();
            Tilemap tilemap = FindObjectOfType<Tilemap>();

            if (terrain != null)
            {
                // Dla terenu, użyj jego rozmiarów
                spawnPosition = new Vector3(terrain.terrainData.size.x / 2f, 0f, terrain.terrainData.size.z / 2f);
                Debug.Log("Znaleziono Terrain. Środek mapy: " + spawnPosition);
            }
            else if (tilemap != null)
            {
                // Dla Tilemap, znajdź jego granice
                BoundsInt bounds = tilemap.cellBounds;
                spawnPosition = tilemap.GetCellCenterWorld(new Vector3Int(Mathf.FloorToInt(bounds.center.x), Mathf.FloorToInt(bounds.center.y), 0)); // Poprawiona linia
                Debug.Log("Znaleziono Tilemap. Środek mapy: " + spawnPosition);
            }
            else
            {
                // Jeśli nie znaleziono terenu ani Tilemap, użyj domyślnej pozycji (środek świata)
                spawnPosition = Vector3.zero;
                Debug.LogWarning("Nie znaleziono obiektu Terrain ani Tilemap w scenie. Gracz zostanie spawnerowany na środku świata.");
            }
        }

        // Utwórz gracza na środku mapy
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Gracz został spawnerowany na pozycji: " + spawnPosition);
        }
        else
        {
            Debug.LogError("Nie przypisano prefabu gracza do skryptu PlayerSpawner!");
        }
    }
}
