using UnityEngine;

public class DelayedBackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundMusic; // Drag and drop your AudioSource here
    public float delayTime = 2f; // Delay time in seconds

    void Start()
    {
        if (backgroundMusic != null)
        {
            StartCoroutine(PlayMusicWithDelay());
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned to the script.");
        }
    }

    System.Collections.IEnumerator PlayMusicWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        backgroundMusic.Play();
    }
}

