using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PulseLight : MonoBehaviour
{
    [Header("Impostazioni Ciclo RGB")]
    [Tooltip("Velocità di cambio colore nell'arcobaleno")]
    public float hueSpeed = 0.2f;

    [Tooltip("Saturazione del colore (0 = bianco, 1 = colore puro)")]
    [Range(0f, 1f)]
    public float saturation = 1f;

    [Header("Impostazioni Pulsazione")]
    [Tooltip("Intensità minima (0 = spento)")]
    public float minIntensity = 0f;

    [Tooltip("Intensità massima HDR")]
    public float maxIntensity = 2f;

    [Tooltip("Velocità della pulsazione dell'intensità")]
    public float pulseSpeed = 3f;

    private Material targetMaterial;
    private float currentHue;
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        targetMaterial = rend.material;
        targetMaterial.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (targetMaterial == null) return;

        // 1. Calcola il nuovo colore RGB ruotando la tonalità (Hue) nel tempo
        currentHue = Mathf.Repeat(currentHue + hueSpeed * Time.deltaTime, 1f);
        Color baseColor = Color.HSVToRGB(currentHue, saturation, 1f);

        // 2. Calcola l'intensità usando la funzione sinusoidale
        float wave = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, wave);

        // 3. Applica colore ed emissione
        if (currentIntensity <= 0.001f)
        {
            targetMaterial.SetColor(EmissionColorID, Color.black);
            targetMaterial.DisableKeyword("_EMISSION");
        }
        else
        {
            targetMaterial.EnableKeyword("_EMISSION");

            // Calcola il colore finale HDR combinando il colore RGB corrente con l'intensità
            Color finalColor = baseColor * Mathf.Pow(2f, currentIntensity);
            targetMaterial.SetColor(EmissionColorID, finalColor);
        }
    }
}