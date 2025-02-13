using UnityEngine;

public class PokazZmienKursor : MonoBehaviour
{
    public Texture2D domyslnyKursor;
    public Texture2D kursorPoKliknieciu;

    private bool czyKliknieto = false;

    void Start()
    {
        // Początkowo pokazujemy kursor i ustawiamy jego domyślny wygląd
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Opcjonalnie, aby kursor mógł opuścić okno gry
        Cursor.SetCursor(domyslnyKursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseDown()
    {
        // Zmieniamy wygląd kursora po kliknięciu
        if (!czyKliknieto)
        {
            Cursor.SetCursor(kursorPoKliknieciu, Vector2.zero, CursorMode.Auto);
            czyKliknieto = true;
        }
        else
        {
            Cursor.SetCursor(domyslnyKursor, Vector2.zero, CursorMode.Auto);
            czyKliknieto = false;
        }
    }
}