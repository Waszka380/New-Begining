using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MinimapGenerator : MonoBehaviour
{
    public Tilemap worldTilemap;
    public RawImage minimapImage;
    public int minimapWidth = 400; // Zwiększony rozmiar
    public int minimapHeight = 400; // Zwiększony rozmiar
    public Button minimapButton;
    public Transform player; // Dodajemy publiczną zmienną dla gracza

    private Texture2D minimapTexture;
    private bool isMinimapVisible = false;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        CreateMinimap();
        minimapButton.onClick.AddListener(ToggleMinimap);
        lastPlayerPosition = player.position; // Inicjalizujemy ostatnią pozycję gracza
    }

    void Update()
    {
        if (isMinimapVisible)
        {
            if (player.position != lastPlayerPosition) // Sprawdzamy, czy gracz się poruszył
            {
                UpdateMinimap();
                lastPlayerPosition = player.position; // Aktualizujemy ostatnią pozycję gracza
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMinimap();
        }
    }

    void CreateMinimap()
    {
        minimapTexture = new Texture2D(minimapWidth, minimapHeight, TextureFormat.RGBA32, false);
        minimapImage.texture = minimapTexture;
        minimapImage.enabled = false; // Początkowo ukrywamy minimapę
    }

    void UpdateMinimap()
    {
        Color32[] pixels = new Color32[minimapWidth * minimapHeight];

        for (int x = 0; x < minimapWidth; x += 2) // Pobieramy tile co 2 piksele
        {
            for (int y = 0; y < minimapHeight; y += 2)
            {
                float worldX = (float)x / minimapWidth * worldTilemap.size.x;
                float worldY = (float)y / minimapHeight * worldTilemap.size.y;

                Vector3Int tilePosition = new Vector3Int(Mathf.FloorToInt(worldX), Mathf.FloorToInt(worldY), 0);
                TileBase tile = worldTilemap.GetTile(tilePosition);

                Color32 color = Color.gray; // Domyślny kolor

                if (tile != null)
                {
                    if (tile.name == "Grass")
                    {
                        color = Color.green;
                    }
                    else if (tile.name == "Water")
                    {
                        color = Color.blue;
                    }
                }
                else
                {
                    color = Color.clear;
                }

                // Interpolacja kolorów (przykład)
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        pixels[(y + j) * minimapWidth + (x + i)] = color;
                    }
                }
            }
        }

        minimapTexture.SetPixels32(pixels);
        minimapTexture.Apply();
    }

    public void ToggleMinimap()
    {
        isMinimapVisible = !isMinimapVisible;
        minimapImage.enabled = isMinimapVisible;
        minimapButton.GetComponentInChildren<Text>().text = isMinimapVisible ? "Ukryj minimapę" : "Pokaż minimapę";
    }
}