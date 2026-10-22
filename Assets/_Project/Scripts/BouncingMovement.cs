using UnityEngine;

public class BouncingMovement : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    [Tooltip("Velocità di spostamento orizzontale")]
    public float moveSpeed = 3f;

    [Tooltip("Raggio dell'area di movimento casuale")]
    public float wanderRadius = 10f;

    [Tooltip("Tempo di pausa prima di scegliere una nuova destinazione")]
    public float changeDirectionInterval = 3f;

    [Header("Impostazioni Salto (Bounce)")]
    [Tooltip("Altezza massima di ogni singolo salto")]
    public float bounceHeight = 1f;

    [Tooltip("Frequenza dei salti (salti al secondo)")]
    public float bounceFrequency = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;

    void Start()
    {
        // Memorizza il punto di origine iniziale
        startPosition = transform.position;
        GetNewTargetPosition();
    }

    void Update()
    {
        // 1. Gestione cambio direzione a intervalli di tempo
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            GetNewTargetPosition();
            timer = 0f;
        }

        // 2. Movimento orizzontale verso il target (X e Z)
        Vector3 currentPos = transform.position;
        Vector3 destination = new Vector3(targetPosition.x, currentPos.y, targetPosition.z);

        transform.position = Vector3.MoveTowards(currentPos, destination, moveSpeed * Time.deltaTime);

        // 3. Calcolo dell'effetto rimbalzo/salto sull'asse Y usando il valore assoluto del seno
        float bounceOffsetY = Mathf.Abs(Mathf.Sin(Time.time * Mathf.PI * bounceFrequency)) * bounceHeight;

        // Applica l'offset del salto mantenendo l'altezza base dello Start
        transform.position = new Vector3(
            transform.position.x,
            startPosition.y + bounceOffsetY,
            transform.position.z
        );
    }

    void GetNewTargetPosition()
    {
        // Calcola un punto casuale all'interno di un cerchio attorno alla posizione iniziale
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        targetPosition = startPosition + new Vector3(randomPoint.x, 0f, randomPoint.y);
    }

    // Disegna l'area di movimento nell'Editor (Scene View)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? startPosition : transform.position;
        Gizmos.DrawWireSphere(center, wanderRadius);
    }
}