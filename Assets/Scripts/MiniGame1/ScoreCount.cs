using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public int numberOfFish;

    public int collectedFish;

    public TMP_Text scoreText;

    private int stars;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stars = 0;
        collectedFish = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(string type){
        if(type == "endItem"){
            this.stars += 1;
        }
        else if(type == "fish"){
            this.collectedFish += 1;

            if(collectedFish == 1){
                this.stars += 1;
            }
            if(collectedFish == numberOfFish){
                this.stars += 1;
            }
        }

        scoreText.text = "Stars: " + this.getStars();     
        
    }

    public int getStars(){
        return this.stars;
    }
}
