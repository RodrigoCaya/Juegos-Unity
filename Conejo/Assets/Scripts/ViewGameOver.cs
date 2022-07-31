using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ViewGameOver : MonoBehaviour{
    public static ViewGameOver sharedInstance;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI coinsLabel;
    void Awake(){
        sharedInstance = this;
    }

    public void ShowCoinsLabel(){
        if(GameManager.sharedInstance.gameState == GameState.GameOver){
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
        }
    }

    public void ShowScore(){
        if(GameManager.sharedInstance.gameState == GameState.GameOver){
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }
}
