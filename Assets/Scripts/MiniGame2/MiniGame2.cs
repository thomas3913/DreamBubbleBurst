using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGame2 : MonoBehaviour
{
    private int gridSize = 1;

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
                Debug.Log("hit");
                // hit.collider.gameObject.pare
                Fish fish = hit.collider.gameObject.GetComponentInParent<Fish>();
                if (fish != null) {
                    mouseStartPosition = getWorldMousePosition();
                    fishStartPosition = fish.transform.position;
                    draggingFish = fish;
                                        
                    Debug.Log("hit fish");
                    Debug.Log(getWorldMousePosition());
                }
            }
        } else if (!mouse.leftButton.isPressed && draggingFish) {

            Debug.Log("release fish");

            if (SnapToGrid(draggingFish)) {
                // fitting
            } else {
                // return outside
            }

            draggingFish = null;
        }

        if (draggingFish != null) {

            // Vector3 p = Input.mousePosition;
            // p.z = 0;
            // Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            // draggingFish.transform.position = pos;

            Vector3 offset = (mouseStartPosition - getWorldMousePosition());
            Vector3 offsetModified = new Vector3(offset.x, offset.y, 0);
            // Vector3 offset3D = new Vector3(offset.x, offset.y, fishStartPosition.z);

            // float mouseDeltaX = offset.x;
            // float mouseDeltaY = offset.y;
            // Vector3 fishPosition = draggingFish.transform.position;
            draggingFish.transform.position = fishStartPosition - offsetModified;
            // draggingFish.transform.position = new Vector3(fishPosition.x + mouseDeltaX, fishPosition.y + mouseDeltaY, fishPosition.z);
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

        draggingFish.transform.position = new Vector3(modified_x, modified_y, 0);

        return true;
    }

    Vector3 getWorldMousePosition() {
        Vector3 p = Input.mousePosition;
        p.z = 0;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        return pos;
    }
}
