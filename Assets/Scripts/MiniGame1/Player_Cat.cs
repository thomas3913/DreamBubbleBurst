using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Cat : MonoBehaviour
{
    public ScoreCount scoreCount;

    public float speed = 0.1f;

    private Collider2D playerCollider;

    public LayerMask CrossingLayer;

    private Rigidbody2D rb;

    private List<string> possibleDirections;

    private string currentDirection;
    private string verticalDirection;
    private string horizontalDirection;

    public bool directionChanged;

    public float colliderCenter;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        possibleDirections = new List<string>();

        currentDirection = "vertical";
        verticalDirection = "up";
        horizontalDirection = "left";

        directionChanged = false;

        colliderCenter = 0.0f;

    

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.up * speed * Time.deltaTime;
        if(this.currentDirection == "vertical"){
            if(this.verticalDirection == "up"){
                Vector2 newPosition = rb.position + Vector2.up * speed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
            }
            else if(this.verticalDirection == "down"){
                Vector2 newPosition = rb.position + Vector2.down * speed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
            }
        }
        else if(this.currentDirection == "horizontal"){
            if(this.horizontalDirection == "left"){
                Vector2 newPosition = rb.position + Vector2.left * speed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
            }
            else if(this.horizontalDirection == "right"){
                Vector2 newPosition = rb.position + Vector2.right * speed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);
            }
        }

        bool collidesWithCrossing = Physics2D.IsTouchingLayers(playerCollider, CrossingLayer);

        if(collidesWithCrossing){
            if(this.currentDirection == "vertical"){
                if(Input.GetKeyDown(KeyCode.LeftArrow) & possibleDirections.Contains("left") & this.directionChanged == false){
                    this.currentDirection = "horizontal";
                    this.horizontalDirection = "left";
                    this.directionChanged = true;
                }

                if(Input.GetKeyDown(KeyCode.RightArrow) & possibleDirections.Contains("right") & this.directionChanged == false){
                    this.currentDirection = "horizontal";
                    this.horizontalDirection = "right";
                    this.directionChanged = true;
                }
            }
            else if(this.currentDirection == "horizontal" & this.directionChanged == false){
                if(this.possibleDirections.Count == 2){
                    
                }
                else{
                    if(this.horizontalDirection == "left"){
                        if(transform.position.x <= colliderCenter){
                            this.currentDirection = "vertical";
                            this.directionChanged = true;
                        }
                    }

                    else if(this.horizontalDirection == "right"){
                        if(transform.position.x >= colliderCenter){
                            this.currentDirection = "vertical";
                            this.directionChanged = true;
                        }
                    }
                }


                
                
            }
            
        }
   
    }


    
    public void SetPossibleDirections(List<string> directionList)
    {
        this.possibleDirections = directionList;
    }

    public void turnPointEntered(){
        if(this.verticalDirection == "up"){
            this.verticalDirection = "down";
        }

        else if(this.verticalDirection == "down" ){
            this.verticalDirection = "up";
        }
    }

    public void collectableEntered(string type){

        scoreCount.updateScore(type);   

    }

    public void deathAreaEntered(){
        SceneManager.Instance.reloadCurrentScene();
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    
}
