using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public int numberOfFish;

    public TMP_Text scoreText;

    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(int value){
        this.score += value;
        scoreText.text = "Score: " + this.score;
    }
}
