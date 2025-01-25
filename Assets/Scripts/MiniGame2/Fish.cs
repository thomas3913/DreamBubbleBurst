using UnityEngine;

public class Fish : MonoBehaviour
{

    public int width = 0;
    public int height = 0;

    private float speed = 10.0F;
    private bool snapping = false;
    private Vector3 targetPosition = new Vector3();
    private Vector3 lastPosition = new Vector3();
    private Vector3 originalPosition = new Vector3();
    private float snapProgress = 0.0F;
    private Rigidbody2D RB = null;
    public MiniGame2 minigame = null;
    public LayerMask CollideLayer;

    // private bool active = false;

    public bool isOutside = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // void setActive(bool active) {
    //     this->active = active;
    // }

    // Update is called once per frame
    void FixedUpdate()
    {

        // if (!active) return;

        if (snapping) {
            if (snapProgress < 1.0F) {
                snapProgress += Time.deltaTime * speed;
                MoveFish(Vector3.Lerp(lastPosition, targetPosition, snapProgress));
                if (snapProgress >= 1.0F) {
                    MoveFish(targetPosition);                    
                }
            } else {

                snapping = false;
                if (!isOutside && isIlligal()) {
                // if (!minigame.isOutside(this, targetPosition) && isIlligal()) {
                    Snap(minigame.getOutsidePosition(this, targetPosition), 5.0F);
                    isOutside = true;
                } else {
                    Highlight(false);
                    minigame.checkComplete();
                }
                
            }
        }
    }
    public void StartDrag(Vector3 startPosition) {
        originalPosition = startPosition;
    }
    public void MoveFish(Vector3 position) {
        Debug.Log(position.z);
        RB.MovePosition(position);
    }
    public void Snap(Vector3 position, float speed) {
        if (snapping) return;
        
        this.speed = speed;

        lastPosition = transform.position;
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

    public void Highlight(bool highlight) {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (highlight) {
            spriteRenderer.gameObject.transform.localScale = new Vector3(1.1F,1.1F,1.1F);
            spriteRenderer.sortingOrder = 1;
        }
        else {
            spriteRenderer.gameObject.transform.localScale = new Vector3(1.0F,1.0F,1.0F);
            spriteRenderer.sortingOrder = 0;
        }
    }

    private bool isIlligal() {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("check collider");
            // Debug.Log(CollideLayer);
            if(collider.IsTouchingLayers()) {
                Debug.Log("collide with fish");
                return true;
            }
        }
        return false;
    }
}
