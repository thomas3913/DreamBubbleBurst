using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour
{

    public bool left;
    public bool right;


    private List<string> directionList;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directionList = new List<string>();
        
        if(left){
            directionList.Add("left");
        }

        if(right){
            directionList.Add("right");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            other.gameObject.GetComponent<Player_Cat>().SetPossibleDirections(directionList);
            other.gameObject.GetComponent<Player_Cat>().directionChanged = false;
            other.gameObject.GetComponent<Player_Cat>().colliderCenterX = transform.position.x;
            other.gameObject.GetComponent<Player_Cat>().colliderCenterY = transform.position.y;
        }
    }
}
