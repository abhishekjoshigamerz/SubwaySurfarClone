using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] TextMeshProUGUI DistanceTravel;
     [SerializeField] TextMeshProUGUI TimeTakenText;
    private Vector3 lastPosition ;
    private int TimeTaken;
    private float totalDistance = 0 ;
    [SerializeField] float forwardSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity = -20;
     private static int hitCounter=0;
    [SerializeField] int MaxHealth = 3;
    Animator playerAnimation;

    [SerializeField] GameObject _blinkingtextMessage;
    
    [SerializeField] float _maxSpeed=40;

    private int laneNumber=1;
    public float laneDistance = 4; //Distance between 2 lanes
    private void Awake(){
        controller = GetComponent<CharacterController>();
        playerAnimation = GetComponent<Animator>();
        controller.enabled = false;
    }
    private void Start(){
         lastPosition = transform.position ;
    }
    void Update(){
        direction.z = forwardSpeed;
        
        CalculateDistance();
        CalculateTime();
         

        direction.y += Gravity * Time.deltaTime;

        if(controller.isGrounded){
            if(Input.GetKeyDown(KeyCode.W)){
            Jump();
            }
        }
       

        if(Input.GetKeyDown(KeyCode.Space)){
            if(playerAnimation.GetBool("isAlive")==false){
                playerAnimation.SetBool("isAlive",true);
                controller.enabled = true;


                //I can disable the text message as well destroy it as well. I prefer to destroy here because it saves memory here.
               // _blinkingtextMessage.SetActive(false);

               Destroy(_blinkingtextMessage);
            }
        }

     
        
       LaneChanger();

        CheckDeath();

       CalculatePositionPlayer();

        
    }
    void LaneChanger(){
           //Key Code COntrol the lane
        if(Input.GetKeyDown(KeyCode.D)){
            laneNumber++;
            if(laneNumber==3){
                playerAnimation.SetTrigger("shiftRight");
                laneNumber = 2;
                
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            playerAnimation.SetTrigger("Sliding");
        }

        if(Input.GetKeyDown(KeyCode.A)){
             laneNumber--;
            if(laneNumber==-1){
                playerAnimation.SetTrigger("shiftLeft");
                laneNumber = 0;
                
            }
        }
    }
    void CheckDeath(){
          if(hitCounter==MaxHealth){
            GameManager.gameOver = true;

        }
    }
    private void CalculatePositionPlayer(){
         //Calucate where the player position will be
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(laneNumber==0){
            targetPosition += Vector3.left * laneDistance;
        }else if(laneNumber==2){
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position,targetPosition,240 * Time.deltaTime);
        //A trick to resolve the bug which makes it difficult to detect collision bcz we are changing player lane in different methods.
        controller.center = controller.center;
    }

    private void FixedUpdate() {
         
       
         controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump(){
        playerAnimation.SetTrigger("IsJumping");
        direction.y = JumpForce;
        
    }

    //to detect Collision here

    private void OnControllerColliderHit(ControllerColliderHit hit) {
      
        if(hit.transform.tag == "Obstacle"){
            hitCounter++;

                
                playerAnimation.SetTrigger("gotHurted");
                playerAnimation.SetBool("SitIdle",true);
                playerAnimation.SetBool("isAlive",false);
                controller.enabled =false;
                Debug.Log(hitCounter);
                StartCoroutine(WaitForFewSecondsAfterHurt());
        }

        if(hit.transform.tag == "Coin"){
            GameManager.numberOfCoins += 1;
            Debug.Log("Touched the coin!");
            Destroy(gameObject);
        }
    }



    IEnumerator WaitForFewSecondsAfterHurt(){
        yield return new WaitForSeconds(1);
        Application.LoadLevel(1);

    }

    void CalculateDistance(){
     float distance = Vector3.Distance( lastPosition, transform.position ) ;
     totalDistance += distance ;
     lastPosition = transform.position ;
     DistanceTravel.text = "Distance Travel " + totalDistance;
    }

    void CalculateTime(){
        TimeTaken = (int)totalDistance/(int)forwardSpeed;
        TimeTakenText.text = "Time Taken: " + TimeTaken + " seconds";
    }
}
