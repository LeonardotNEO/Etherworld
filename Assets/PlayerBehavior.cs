using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System;

public class PlayerBehavior : MonoBehaviour
{
    GameManager gameManager;
    public Vector3 hitGround;
    public Vector3 playerPosition;
    public NavMeshAgent agent;
    public bool isMovingToDestination;
    public bool reachedDestination;
    public bool touchingObstacle;
    public bool mouseOnItemResource;
    public bool movementDisabled;
    public bool playerEnteredBuilding;
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
        if (Input.GetMouseButtonDown(0) && !gameManager.GetUI().getIsMouseOverUI() && !gameManager.getCraftingSystem().getIsCrafting()){
            RaycastHit hit;
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            if(hit.collider.tag == "Ground"){
                moveToPosition(hit.point);
            }
        } 
        if(agent.remainingDistance <= 0.2){
            isMovingToDestination = false;
            reachedDestination = true;
            GetComponent<Animator>().SetBool("isMoving" , false);
        } else {
            isMovingToDestination = true;
            reachedDestination = false;
            GetComponent<Animator>().SetBool("isMoving" , true);
        }
    }
    void OnTriggerStay(Collider colliderInfo){
        
    }
    void OnTriggerExit(Collider colliderInfo){
        
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
    public Vector3 getPlayerPosition(){
        return playerPosition;
    }  
    public void stopPlayer(){
        agent.ResetPath();
    }

    public void moveToPosition(Vector3 newPosition){
        if(!movementDisabled){
            agent.SetDestination(newPosition);
        }
    }

    public void teleportToPosition(Vector3 newPosition){
        agent.Warp(newPosition);
    }

    public void playerLookAt(GameObject lookAt){
        transform.LookAt(new Vector3(lookAt.transform.position.x, 0, lookAt.transform.position.z));
    }
    public bool getMovementDisabled(){
        return movementDisabled;
    }
    public void setMovementDisabled(bool val){
        movementDisabled = val;
    }

    public void hideBody(bool val){
        if(val){
            transform.GetChild(0).gameObject.SetActive(false);
        } else {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    public bool getPlayerEnteredBuilding(){
        return playerEnteredBuilding;
    }
    public void setPlayerEnteredBuilding(bool val){
        playerEnteredBuilding = val;
    }
}
