using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System;

public class PlayerBehavior : MonoBehaviour
{
    GameManager gameManager;
    public float playerSpeed;
    public bool isMovingToDestination;
    public bool reachedDestination;
    public bool touchingObstacle;
    public bool mouseOnItemResource;
    bool move;
    public Collider colliderInfo;
    public RaycastHit hitGround;
    public Vector3 playerPosition;
    void Start()
    {
        stopPlayer();
    }
    
    void Update()
    {
        playerPosition = transform.position;

        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // PLAYER MOVEMENT
        // Move player to new position when pressing mouse click
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI() && !gameManager.getPlacingBuilding()){
            RaycastHit hit;
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            if(hit.collider.tag == "Ground"){
                hitGround.point = hit.point;
            }
        } 
    }

    void FixedUpdate()
    {   
        if(System.Math.Round(transform.position.x) == System.Math.Round(getHitGroundPosition().x) && System.Math.Round(transform.position.z) == System.Math.Round(getHitGroundPosition().z)){
            stopPlayer();
        } else {
            movePlayer();
        }
    }

    void OnTriggerStay(Collider colliderInfo){
        
    }
    // no collider detected if player has moved out of collider
    void OnTriggerExit(Collider collider){
        colliderInfo = null;
    }

    public IEnumerator moveToPosition(){
        bool runloop = true;
        while(runloop){
            if(System.Math.Round(transform.position.x) == System.Math.Round(getHitGroundPosition().x) && System.Math.Round(transform.position.z) == System.Math.Round(getHitGroundPosition().z)){
                stopPlayer();
                break;
            }
            movePlayer();
            yield return null;
        }
            
            /*
            if(transform.position.z > getHitGroundPosition().z){
                if(transform.position.z <= getHitGroundPosition().z){
                    stopPlayer();
                }
            }
            if(transform.position.x < getHitGroundPosition().x){
                if(transform.position.x >= getHitGroundPosition().x){
                    stopPlayer();
                }
            }
            if(transform.position.z < getHitGroundPosition().z){
                if(transform.position.z >= getHitGroundPosition().z){
                    stopPlayer();
                }
            }
            */ 
    }
    public bool isMouseOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }
    public float getPlayerSpeed(){
        return playerSpeed;
    }
    public void setPlayerSpeed(int newspeed){
        playerSpeed = newspeed;
    }
    public bool getIsMovingToDestination(){
        return isMovingToDestination;
    }
    public void setIsMovingToDestination(bool newBool){
        isMovingToDestination = newBool;
    }
    public bool getReachedDestination(){
        return reachedDestination;
    }
    public void setReachedDestination(bool newBool){
        reachedDestination = newBool;
    }
    public Vector3 getHitGroundPosition(){
        return hitGround.point;
    }
    public void setHitGroundPostion(Vector3 newPosition){
        hitGround.point = newPosition;
    } 
    public Vector3 getPlayerPosition(){
        return playerPosition;
    }  
    public void stopPlayer(){

        isMovingToDestination = false;
        reachedDestination = true;
        GetComponent<Animator>().SetBool("isMoving" , false);
        setHitGroundPostion(transform.position);
    }

    public void movePlayer(){
        playerLookAt(getHitGroundPosition().x, 0, getHitGroundPosition().z);
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * playerSpeed);
        isMovingToDestination = true;
        reachedDestination = false;
        GetComponent<Animator>().SetBool("isMoving" , true);
    }

    public void playerLookAt(float x, float y, float z){
        transform.LookAt(new Vector3(x, y, z));
    }
}
