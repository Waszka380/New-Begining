using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeGenerator : MonoBehaviour
{
    public Tilemap tilemapPodłoże; // Tilemap z podłożem (trawa, piasek itp.)
    public Tilemap tilemapDrzewa; // Tilemap na drzewa
    public GameObject prefabDrzewa;
    public float skalaDrzew = 2f;
    public float prógDrzewa = 0.6f;

    void Start()
    {
        GenerujDrzewa();
    }

    void GenerujDrzewa()
    {
        BoundsInt granice = tilemapPodłoże.cellBounds; // Pobieramy granice tilemapy z podłożem

        for (int x = granice.min.x; x < granice.max.x; x++)
        {
            for (int y = granice.min.y; y < granice.max.y; y++)
            {
                Vector3Int pozycjaKomórki = new Vector3Int(x, y, 0);
                TileBase tilePodłoże = tilemapPodłoże.GetTile(pozycjaKomórki);

                if (tilePodłoże != null && tilePodłoże.name.Contains("Grass")) // Sprawdzamy, czy kafelek podłoża to trawa (można dostosować)
                {
                    float sampleXDrzewa = (float)x / 100 * skalaDrzew + Random.value * 10; // Dodajemy Random.value dla większej różnorodności
                    float sampleYDrzewa = (float)y / 100 * skalaDrzew + Random.value * 10;
                    float perlinValueDrzewa = Mathf.PerlinNoise(sampleXDrzewa, sampleYDrzewa);

                    if (perlinValueDrzewa > prógDrzewa)
                    {
                        Vector3 pozycjaDrzewa = tilemapPodłoże.GetCellCenterWorld(pozycjaKomórki); // Pozycja ze środka komórki podłoża
                        GameObject drzewo = Instantiate(prefabDrzewa, pozycjaDrzewa, Quaternion.identity);

                        // Dodajemy drzewo jako dziecko Tilemapy z drzewami, żeby uniknąć bałaganu w hierarchii
                        drzewo.transform.SetParent(tilemapDrzewa.transform);

                        // Opcjonalnie: Ustawiamy pozycję drzewa względem Tilemapy z drzewami
                        // Vector3Int pozycjaWTilemapieDrzewa = tilemapDrzewa.WorldToCell(pozycjaDrzewa);
                        // tilemapDrzewa.SetTile(pozycjaWTilemapieDrzewa, jakiśTileDrzewa); // Jeśli chcesz użyć Tile w Tilemapie z drzewami
                    }
                }
            }
        }
    }
}