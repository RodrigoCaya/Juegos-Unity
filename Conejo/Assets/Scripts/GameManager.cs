using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    Menu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour{
    public GameState gameState = GameState.Menu;
    public static GameManager sharedInstance;
    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public int collectedCoins = 0;

    void Awake(){
        sharedInstance = this;
    }

    void Start(){
        gameState = GameState.Menu;
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    void Update(){
        // if(gameState == GameState.Menu || gameState == GameState.GameOver){
        //     if(Input.GetKeyDown(KeyCode.Return)){
        //         StartGame();
        //     }
        // }
    }

    //Al empezar el juego
    public void StartGame(){
        if(gameState == GameState.Menu || gameState == GameState.GameOver){
            PlayerController.sharedInstance.StartGame();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
            ChangeGameState(GameState.Playing);
            ViewInGame.sharedInstance.UpdateHighScoreLabel();
            collectedCoins = 0;
            ViewInGame.sharedInstance.UpdateCoinsLabel();
        }
    }

    //Al terminar el juego
    public void GameOver(){
        LevelGenerator.sharedInstance.RemoveAllTheBlocks();
        ChangeGameState(GameState.GameOver);
        ViewGameOver.sharedInstance.ShowCoinsLabel();
        ViewGameOver.sharedInstance.ShowScore();
    }

    //Volver al menu
    public void ReturnToMenu(){
        ChangeGameState(GameState.Menu);
    }

    void ChangeGameState(GameState newGameState){
        if(newGameState == GameState.Menu ){
            //TODO: Mostrar el menu
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }else if(newGameState == GameState.Playing){
            //TODO: Ocultar el menu
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }else if(newGameState == GameState.GameOver){
            //TODO: Mostrar el menu de game over
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }
        gameState = newGameState;
    }

    public void CollectCoin(){
        collectedCoins++;
        //Debug.Log("Monedas recogidas "+ collectedCoins);
        ViewInGame.sharedInstance.UpdateCoinsLabel();
    }
}
