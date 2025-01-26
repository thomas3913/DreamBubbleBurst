using UnityEngine;
using System.Collections;
using Unity.Audio;

public class SwipePfote : MonoBehaviour
{
    public float duration = 2f; // Dauer der Bewegung (in Sekunden)
    public float startDelay = 40f; // Verz�gerung, bis die Bewegung beginnt (in Sekunden)
    public AudioClip wiperSound; // Der Sound, der abgespielt wird
    private AudioSource audioSource; // Die AudioSource-Komponente
    private Vector3 startPosition; // Startposition der Pfote
    private Vector3 endPosition; // Endposition der Pfote
    private bool isMoving = false; // Flag, um zu �berpr�fen, ob die Bewegung begonnen hat

    void Start()
    {
        // Initialisierung
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position; // Startposition (unterer linker Rand)
        endPosition = new Vector3(Screen.width, startPosition.y, startPosition.z); // Endposition (unterer rechter Rand)
        endPosition = Camera.main.ScreenToWorldPoint(endPosition); // Weltkoordinaten

        // Verz�gerung der Bewegung mit Coroutine
        StartCoroutine(StartMovementAfterDelay(startDelay));
    }

    void Update()
    {
        // Wenn die Bewegung gestartet wurde, bewege das GameObject
        if (isMoving)
        {
            // Berechne die aktuelle Zeit auf der Bewegungsdauer
            float t = Mathf.Clamp01((Time.time - startDelay) / duration);

            // Berechne die neue Position mit einer Sinus-Funktion f�r den Scheibenwischer-Effekt
            float yOffset = Mathf.Sin(t * Mathf.PI); // Sinuskurve f�r sanfte vertikale Bewegung
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, t);
            newPosition.y += yOffset;

            // Setze die neue Position
            transform.position = newPosition;

            // Wenn die Bewegung abgeschlossen ist, stoppe
            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }

    // Coroutine f�r die Verz�gerung
    IEnumerator StartMovementAfterDelay(float delay)
    {
        // Warten f�r die angegebene Verz�gerung
        yield return new WaitForSeconds(delay);

        // Beginne die Bewegung
        isMoving = true;

        // Spiele den Sound ab
        if (audioSource != null && wiperSound != null)
        {
            audioSource.PlayOneShot(wiperSound);
        }
    }
}
