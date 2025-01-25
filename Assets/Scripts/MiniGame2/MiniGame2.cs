using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class MiniGame2 : MonoBehaviour
{
    private int gridSize = 1;

    public int width = 0;
    public int height = 0;

    private Fish draggingFish = null;
    private Vector3 mouseStartPosition = new Vector3(0,0,0);
    private Vector3 fishStartPosition = new Vector3(0,0,0);

    private List<Fish> inactiveFishes = new List<Fish>();
    private List<Fish> activeFishes = new List<Fish>();

    System.Random random = new System.Random();

    Vector3 spawn = new Vector3(-5.0F, 0, 0.0F);

    Fish[] allFish = null;

    public string nextScene;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allFish = gameObject.GetComponentsInChildren<Fish>();
        StartGame();
    }

    void StartGame() {
        foreach (Fish fish in allFish) {
            inactiveFishes.Add(fish);
            fish.gameObject.SetActive(false);
        }             
        ServeFish();
    }

    void ServeFish() {
        Fish fish = inactiveFishes[random.Next(inactiveFishes.Count)];
        inactiveFishes.Remove(fish);
        activeFishes.Add(fish);
        Vector3 spawnPosition = new Vector3(-width * 0.5F - (fish.width * 0.5F + 2), 0, 0);
        fish.transform.position = spawnPosition;
        fish.isOutside = true;

        SpriteRenderer spriteRenderer = fish.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.gameObject.transform.localScale = new Vector3(0, 0, 0);

        for (int i = 0; i < random.Next(4); i++) {
            fish.rotate();
        }
        fish.gameObject.SetActive(true);
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
                    fish.minigame = this;
                    mouseStartPosition = getWorldMousePosition();
                    
                    OrderFish(fish);
                    
                    fishStartPosition = fish.transform.position;
                    // fishStartPosition = new Vector3(fish.transform.position.x, fish.transform.position.y, -0.5F);
                    draggingFish = fish;
                    draggingFish.StartDrag(fishStartPosition);
                                        
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
            draggingFish.MoveFish((fishStartPosition - offsetModified));
            // draggingFish.transform.position = fishStartPosition - offsetModified;
        }
        
    }

    void OrderFish(Fish activeFish) {

        foreach (Fish fish in activeFishes) {                        
            // Vector3 fishPosition = fish.transform.position;
            // fishPosition.z = 0.5F;
            // gameObject.sortingOrder = -1;
            // fish.MoveFish(fishPosition);
            // fish.transform.localScale = new Vector3(1,1,1);
            fish.Highlight(false);
        }
        
        // Vector3 activeFishPosition = activeFish.transform.position;
        // activeFish.transform.localScale = new Vector3(1.1F,1.1F,1.1F);
        activeFish.Highlight(true);
        // activeFishPosition.z = -0.5F;
        // activeFish.MoveFish(activeFishPosition);
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

        Vector3 targetPosition = new Vector3(modified_x, modified_y, fishPosition.z);

        if (isOutside(draggingFish, targetPosition)) {
            Debug.Log("OUTSIDE!");
            draggingFish.Snap(getOutsidePosition(draggingFish, targetPosition), 10.0F);
            draggingFish.isOutside = true;
            // draggingFish.Snap(fishStartPosition, 5.0F);
            return false;
        } else {
            draggingFish.Snap(targetPosition, 10.0F);
            draggingFish.isOutside = false;
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

    public bool isOutside(Fish fish, Vector3 position) {
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

    public Vector3 getOutsidePosition(Fish fish, Vector3 position) {
        int left = (int)(position.x - fish.width * 0.5F);
        int right = (int)(position.x + fish.width * 0.5F);
        int top = (int)(position.y + fish.height * 0.5F);
        int bottom = (int)(position.y - fish.height * 0.5F);
        if (position.x < 0) {
            return new Vector3(-width / 2 - (fish.width * 0.5F + 2.0F), position.y, position.z); 
        }
        if (position.x >= 0) {
            return new Vector3(width / 2 + (fish.width * 0.5F + 2.0F), position.y, position.z); 
        }
        if (top > height / 2) {
            return new Vector3(right + (fish.width * 0.5F), position.y, position.z); 
        }
        if (bottom < -height / 2) {
            return new Vector3(left - (fish.width * 0.5F), position.y, position.z); 
        }
        return position;
    }

    public bool checkComplete() {
        bool aFishIsOutside = false;
        foreach (Fish fish in activeFishes) {
            if (fish.isOutside) {
                aFishIsOutside = true;
            }
        }
        if (inactiveFishes.Count > 0 && !aFishIsOutside) {
            ServeFish();
            return false;
        }

        if (!aFishIsOutside) {
            SceneManager.Instance.loadScene(nextScene);
        }

        return !aFishIsOutside;
    }

}
