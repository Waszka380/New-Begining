using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    public Biome[] biomes;
    public int width = 100;
    public int height = 100;

    private Biome[,] biomeMap; // Mapa biomów

    void Start()
    {
        GenerateBiomes();
    }

    public void GenerateBiomes()
    {
        biomeMap = new Biome[width, height]; // Inicjalizacja mapy

        // ... (Tutaj logika generowania biomów i wypełniania biomeMap) ...
        // Przykład: Losowe przypisywanie biomów
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                biomeMap[x, y] = biomes[Random.Range(0, biomes.Length)];
            }
        }
    }

    public Biome GetBiomeAt(int x, int y)
    {
        // Ważne: Sprawdzenie, czy współrzędne są w zakresie mapy
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return biomeMap[x, y];
        }
        else
        {
            Debug.LogWarning("Współrzędne poza zakresem mapy!");
            return null; // Lub możesz zwrócić jakiś domyślny biom
        }
    }
}