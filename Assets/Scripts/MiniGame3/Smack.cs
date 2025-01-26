using UnityEngine;
using System.Collections;

public class Smack : MonoBehaviour
{
    private float speed = 35.0f;

    private Vector3 start;
    private Vector3 targetPosition;

    private float arrivalThreshold = 0.1f;

    public float targetScale = 1.0F;
    private float currentScale = 1.0F;
    private float scaleSpeed = 5F;

    public GameObject smackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // targetScale = 1.0F;
        start = transform.position;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (Vector3.Distance(transform.position, start) < arrivalThreshold && (hit.collider == null || hit.collider.gameObject != smackground))
            {
                targetScale = 0.8F;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                Debug.Log(targetPosition);
                // targetScale = 1.3F; ???
            }
        }

        if (Vector3.Distance(transform.position, targetPosition) < arrivalThreshold) // Input.GetMouseButtonUp(0) && 
        {
            targetScale = 1.0F;
            targetPosition = start;
        }

        float scaleDifference = Mathf.Abs(currentScale - targetScale);
        if (scaleDifference > 0.01F) {
            // Debug.Log(scaleDifference);
            // Debug.Log("scale!");
            float delta = Time.deltaTime * scaleSpeed;
            // Debug.Log(delta);

            if (Mathf.Abs(delta) > scaleDifference) {
                delta = scaleDifference;
            }
            // Debug.Log(Mathf.Abs(delta));

            if (currentScale > targetScale) {
                currentScale -= delta;
            } else {
                currentScale += delta;
            }

            // Debug.Log(currentScale);

            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }
}
