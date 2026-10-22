using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Impostazioni Rotazione")]
    [Tooltip("Velocità di rotazione sull'asse Y (gradi al secondo)")]
    public float rotationSpeed = 45f;

    [Header("Impostazioni Fluttuazione")]
    [Tooltip("Velocità del movimento su e giù")]
    public float floatSpeed = 2f;
    [Tooltip("Distanza massima del movimento in altezza")]
    public float floatAmplitude = 0.25f;

    private Vector3 startPos;

    void Start()
    {
        // Memorizza la posizione iniziale dell'oggetto all'avvio del gioco
        startPos = transform.position;
    }

    void Update()
    {
        // 1. Gestisce la rotazione sull'asse Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // 2. Calcola la nuova posizione Y usando un'onda sinusoidale in base al tempo
        float newY = startPos.y + (Mathf.Sin(Time.time * floatSpeed) * floatAmplitude);

        // Applica la nuova posizione mantenendo invariate la X e la Z
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}