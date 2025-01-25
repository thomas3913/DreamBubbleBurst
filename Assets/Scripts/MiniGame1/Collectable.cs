using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public string type;

    public bool finish;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            other.gameObject.GetComponent<Player_Cat>().collectableEntered(type);

            if(finish){
                
                
            }
            else{
                Destroy(gameObject,0.1f);
            }
            

        }
    }
}
