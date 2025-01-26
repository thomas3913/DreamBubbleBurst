using UnityEngine;
using UnityEngine.Audio;

public class WischPfote : MonoBehaviour
{
    public float width = 5f;           // Horizontale Bewegungsbreite des Scheibenwischers
    public float height = 2f;          // Vertikale Höhe der Bewegung (halbkreisartig)
    public float speed = 2f;           // Geschwindigkeit der Bewegung
    public float interval = 40f;       // Zeitintervall für den Zyklus (in Sekunden)

    private Vector3 startPosition;     // Startposition des Objekts
    private float timeElapsed = 0f;    // Verstrichene Zeit für den Zyklus
    private bool isMoving = false;     // Flag, ob das Objekt sich momentan bewegt
    private bool cycleComplete = false; // Flag, um zu verhindern, dass das Objekt während des Intervalls bewegt wird

    public Animation animationComponent;  // Die Animation-Komponente des GameObjects
    private AudioSource audioSource;       // Die AudioSource, die den Sound abspielt
    public AudioClip soundClip;            // Der AudioClip, der abgespielt werden soll
    void Start()
    {
        startPosition = transform.position; // Startposition des Objekts speichern

        animationComponent = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();

        // Sicherstellen, dass eine AudioSource und ein AudioClip vorhanden sind
        if (audioSource == null || soundClip == null)
        {
            Debug.LogError("AudioSource oder AudioClip fehlt!");
            return;
        }

        // Starten der Animation und Abspielen des Sounds
        PlayAnimationWithSound();
    }

    void  PlayAnimationWithSound()
    {
        // Die Animation abspielen
        animationComponent.Play();

        // Den Sound abspielen, sobald die Animation startet
        audioSource.PlayOneShot(soundClip);

    }

    void Update()
    {
        if (cycleComplete)
        {
            // Wenn der Zyklus abgeschlossen ist, warte auf das nächste Intervall
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= interval)
            {
                cycleComplete = false; // Zyklus zurücksetzen
                timeElapsed = 0f; // Zeit zurücksetzen
            }
        }
        else
        {
            // Startet die Bewegung einmal und lässt das Objekt darüber schweifen
            if (!isMoving)
            {
                isMoving = true; // Bewegung starten
                StartCoroutine(SwipeMovement());
            }
        }
    }

    System.Collections.IEnumerator SwipeMovement()
    {
        float elapsedTime = 40f;

        while (elapsedTime < 1f) // Bewege das Objekt einmal
        {
            elapsedTime += Time.deltaTime * speed;

            // Berechnung der Bewegung (Hin- und Herbewegung in Form eines Scheibenwischers)
            float movementProgress = Mathf.PingPong(elapsedTime, 1f);

            // Vertikale Bewegung für den Halbkreis-Effekt
            float verticalMovement = Mathf.Sin(movementProgress * Mathf.PI) * height;

            // Horizontale Bewegung (von links nach rechts)
            float horizontalMovement = Mathf.Lerp(0f, width, movementProgress);

            // Setze die neue Position
            transform.position = startPosition + new Vector3(horizontalMovement, verticalMovement, 0f);

            yield return null; // Warten, bis der nächste Frame kommt
        }

        // Nachdem die Bewegung abgeschlossen ist, warten wir 30 Sekunden
        cycleComplete = true;
        isMoving = false; // Die Bewegung ist beendet
    }

    
}
