using UnityEngine;
using UnityEngine.UI;


public class UISpriteAnimator : MonoBehaviour
{
    public Image uiImage1;
    public Image uiImage2;
    public Image uiImage3;

    public Image fullBorder;

    public Sprite[] frames1;
    public Sprite[] frames2;
    public Sprite[] frames3;

    public Sprite[] frames_fullBorder;

    public float frameRate = 0.1f; // Time per frame

    private int currentFrame;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frames1.Length; // Loop animation
            uiImage1.sprite = frames1[currentFrame];
            uiImage2.sprite = frames2[currentFrame];
            uiImage3.sprite = frames3[currentFrame];

            fullBorder.sprite = frames_fullBorder[currentFrame];
        }
    }
}
