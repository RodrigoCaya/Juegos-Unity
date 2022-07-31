using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ViewInGame : MonoBehaviour{
    public static ViewInGame sharedInstance;
    public TextMeshProUGUI coinsLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI highScoreLabel;
    void Awake(){
        sharedInstance = this;
    }
    // Update is called once per frame
    void Update(){
        if(GameManager.sharedInstance.gameState == GameState.Playing){
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }

    public void UpdateHighScoreLabel(){
        if(GameManager.sharedInstance.gameState == GameState.Playing){
            highScoreLabel.text = PlayerPrefs.GetFloat("highscore",0).ToString("f0");
        }
    }

    public void UpdateCoinsLabel(){
        if(GameManager.sharedInstance.gameState == GameState.Playing){
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
        }
    }
}
