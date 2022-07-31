using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public static PlayerController sharedInstance;
    public float jumpForce = 20.0f;
    public LayerMask groundLayer;
    public Animator animator;
    public float runnigSpeed = 3.0f;
    private Rigidbody2D rb2d;
    private Vector3 startPosition;

    List<Vector3> positions;

    void Awake(){
        sharedInstance = this;
        animator.SetBool("isAlive", true);
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    public void StartGame(){
        animator.SetBool("isAlive", true);
        this.transform.position = startPosition;
        rb2d.velocity = new Vector2(0, 0);
        positions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update(){
        if(GameManager.sharedInstance.gameState == GameState.Playing){
            animator.SetBool("isGrounded", IsGrounded());
            //Jump
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                Jump();
            }
        }
    }

    void FixedUpdate(){//se ejecuta cada intervalo de tiempo fijo
        if(GameManager.sharedInstance.gameState == GameState.Playing){
            //movimiento
            if(rb2d.velocity.x < runnigSpeed){
                rb2d.velocity = new Vector2(runnigSpeed, rb2d.velocity.y);
            }
        }
    }


    void Jump(){
        if(IsGrounded()){
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Vector2.up vector unitario
        }
    }

    bool IsGrounded(){
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayer.value)){//Desde la distancia 1.0f del centro del conejo hace un raycast hacia abajo para ver si toca el suelo
            return true;
        }else{
            return false;
        }
    }

    public void KillPlayer(){
        GameManager.sharedInstance.GameOver();
        animator.SetBool("isAlive", false);
        if(PlayerPrefs.GetFloat("highscore",0) < this.GetDistance()){
            PlayerPrefs.SetFloat("highscore", this.GetDistance());
        }
    }

    public float GetDistance(){
        float distanceTravelled = Vector2.Distance(new Vector2(startPosition.x,0),new Vector2(this.transform.position.x,0));
        return distanceTravelled;
    }
    
}
