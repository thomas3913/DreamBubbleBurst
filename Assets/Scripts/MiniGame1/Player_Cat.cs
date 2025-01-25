using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cat : MonoBehaviour
{
    public float speed = 0.1f;

    private Collider2D playerCollider;

    public LayerMask CrossingLayer;

    private Rigidbody2D rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.up * speed * Time.deltaTime;
        Vector2 newPosition = rb.position + Vector2.up * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        bool collidesWithTrigger = Physics2D.IsTouchingLayers(playerCollider, CrossingLayer);

        if(collidesWithTrigger){
            //Debug.Log("COLLIDE");

            if(Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("SPACE");
            }
        }        
    }


    
    public void SetPossibleDirections(List<string> directionList)
    {
        Debug.Log(directionList.Count);
    }

    
}
