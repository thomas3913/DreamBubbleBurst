using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    public AudioClip soundClip;  // The sound clip to play when clicked
    private AudioSource audioSource;

    public float minPitch = 0.5f;  // Minimum pitch value
    public float maxPitch = 1.5f;  // Maximum pitch value

    public GameObject smackground;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detect if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))  // 0 is for the left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            
            if (hit.collider == null || hit.collider.gameObject != smackground)
            {
                // Play the sound when the mouse is clicked
                PlayClickSound();
            }
        }
    }

    void PlayClickSound()
    {
        // Check if the sound clip is assigned
        if (soundClip != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);

            audioSource.PlayOneShot(soundClip);  // Play the sound once
        }
        else
        {
            Debug.LogWarning("No sound clip assigned!");
        }
    }
}
