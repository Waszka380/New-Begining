using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Gracz, za którym kamera ma podążać
    public float smoothSpeed = 0.125f; // Szybkość podążania kamery
    public Vector3 offset = new Vector3(0, 0, -10); // Odległość kamery od gracza
    public float zoomSpeed = 5f; // Szybkość przybliżania i oddalania
    public float minZoom = 5f; // Minimalny rozmiar kamery
    public float maxZoom = 20f; // Maksymalny rozmiar kamery

    private Camera cam; // Komponent kamery

    private void Start()
    {
        cam = GetComponent<Camera>(); // Pobieramy komponent kamery
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Podążanie za graczem
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Przybliżanie i oddalanie za pomocą scrolla myszy
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");

            cam.orthographicSize -= zoomInput * zoomSpeed; // Zmieniamy rozmiar kamery w zależności od ruchu scrolla

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom); // Ograniczamy zoom do minimum i maksimum
        }
    }
}