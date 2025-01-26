using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public int numberOfFish;

    public int collectedFish;

    private int stars;

    public GameObject endScreen;
    public GameObject decorations;

    public GameObject[] inactiveStars;
    public GameObject[] activeStars;

    public Button mainMenuButton;
    public Button playAgainButton;
    public Button nextLevelButton;

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

            this.levelFinished("win");
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
        
    }

    public int getStars(){
        return this.stars;
    }

    public void levelFinished(string mode){

        if(mode == "death"){
            this.stars = 0;

            mainMenuButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, -120);
            playAgainButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(120, -120);
            nextLevelButton.gameObject.SetActive(false);
        }



        if(SceneManager.Instance.getCurrentScene() == "Minigame_1b"){
            mainMenuButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, -120);
            playAgainButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(120, -120);
            nextLevelButton.gameObject.SetActive(false);
        }

        endScreen.SetActive(true);
        decorations.SetActive(false);

        int starCount = Mathf.Clamp(this.getStars(), 0, inactiveStars.Length);

        for (int i = 0; i < inactiveStars.Length; i++)
        {
            if (i < starCount)
            {
                // Activate stars up to starCount
                activeStars[i].SetActive(true);
                activeStars[i].GetComponent<StarAnimation>().popUp();
                inactiveStars[i].SetActive(false);
            }
            else
            {
                // Deactivate remaining stars
                activeStars[i].SetActive(false);
                inactiveStars[i].SetActive(true);
                inactiveStars[i].GetComponent<StarAnimation>().popUp();
            }
        }

        




    }

    public void loadScene(string scene){
        SceneManager.Instance.loadScene(scene);
    }
}
