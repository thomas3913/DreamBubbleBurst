using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGame2 : MonoBehaviour
{
    private int gridSize = 1;

    public int width = 0;
    public int height = 0;

    private Fish draggingFish = null;
    private Vector3 mouseStartPosition = new Vector3(0,0,0);
    private Vector3 fishStartPosition = new Vector3(0,0,0);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // foreach(Collider  in gameObject.GetComponentsInChildren<>()) {
            
        // }
        
    }

    // Update is called once per frame
    void Update()
    {

        Mouse mouse = Mouse.current;

        if (mouse.leftButton.isPressed && draggingFish == null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
                
            if(hit.collider != null) { //  && hit.collider.transform == thisTransform
                // raycast hit this gameobject
                // hit.collider.gameObject.pare
                Fish fish = hit.collider.gameObject.GetComponentInParent<Fish>();
                if (fish != null && !fish.isSnapping()) {
                    mouseStartPosition = getWorldMousePosition();
                    fishStartPosition = fish.transform.position;
                    draggingFish = fish;
                    draggingFish.StartDrag();
                                        
                    // Debug.Log("hit fish");
                    // Debug.Log(getWorldMousePosition());
                }
            }
        } else if (!mouse.leftButton.isPressed && draggingFish) {

            // Debug.Log("release fish");

            if (SnapToGrid(draggingFish)) {
                // fitting
            } else {
                // return outside
            }

            draggingFish = null;
        }

        if (draggingFish != null) {

            if (Input.GetKeyDown("r"))
            {
                draggingFish.rotate();
            }

            Vector3 offset = (mouseStartPosition - getWorldMousePosition());
            Vector3 offsetModified = new Vector3(offset.x, offset.y, 0);
            draggingFish.MoveFish(fishStartPosition - offsetModified);
            // draggingFish.transform.position = fishStartPosition - offsetModified;
        }
        
    }

    // snap an item to the grid, or return it if it doesn't fit
    bool SnapToGrid(Fish fish) {

        Vector3 fishPosition = fish.transform.position;


        float modified_x = (int)Mathf.Round(fishPosition.x / gridSize) * gridSize;
        float modified_y = (int)Mathf.Round(fishPosition.y / gridSize) * gridSize;

        if (fish.width % 2 == 1) {
            if (fishPosition.x > modified_x) modified_x += gridSize * 0.5F;
            else modified_x -= gridSize * 0.5F;
        }

        if (fish.height % 2 == 1) {
            if (fishPosition.y > modified_y) modified_y += gridSize * 0.5F;
            else modified_y -= gridSize * 0.5F;
        }

        Vector3 targetPosition = new Vector3(modified_x, modified_y, 0);

        if (isOutside(draggingFish, targetPosition)) {
            draggingFish.Snap(fishStartPosition, 5.0F);
            return false;
        } else {
            draggingFish.Snap(targetPosition, 10.0F);
            return true;
        }

        return true;
    }

    Vector3 getWorldMousePosition() {
        Vector3 p = Input.mousePosition;
        p.z = 0;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        return pos;
    }

    bool isOutside(Fish fish, Vector3 position) {
        int left = (int)(position.x - fish.width * 0.5F);
        int right = (int)(position.x + fish.width * 0.5F);
        int top = (int)(position.y + fish.height * 0.5F);
        int bottom = (int)(position.y - fish.height * 0.5F);
        if (left < -width / 2) {
            return true; 
        }
        if (right > width / 2) {
            return true; 
        }
        if (top > height / 2) {
            return true; 
        }
        if (bottom < -height / 2) {
            return true; 
        }
        return false;
    }

}
