using UnityEngine;

public class Fish : MonoBehaviour
{

    public int width = 0;
    public int height = 0;

    private float speed = 10.0F;
    private bool snapping = false;
    private Vector3 targetPosition = new Vector3();
    private Vector3 originalPosition = new Vector3();
    private float snapProgress = 0.0F;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (snapping && snapProgress < 1.0F) {
            snapProgress += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, snapProgress);
            if (snapProgress >= 1.0F) {
                transform.position = targetPosition;
                snapping = false;
            }
        }
    }

    public void Snap(Vector3 position) {
        if (snapping) return;

        originalPosition = transform.position;
        targetPosition = position;
        snapping = true;
        snapProgress = 0;
    }
    public bool isSnapping() {
        return snapping;
    }
    public void rotate() {
        transform.Rotate(0, 0, -90, Space.Self);
        int saveWidth = width;
        width = height;
        height = saveWidth;
    }
}
