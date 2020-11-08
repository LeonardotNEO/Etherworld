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
    public BuildingAttributes buildingInsideOf;

    public bool isMovingToDestination;
    public bool reachedDestination;

    public bool touchingObstacle;
    public bool mouseOnItemResource;
    public bool movementDisabled;

    public bool isMovingToBuilding;
    public bool movedToBuilding;

    public bool playerInsideBuilding;

    public GameObject npc; //for testing

    void Start()
    {
        stopPlayer();
    }
    
    void Update()
    {
        if(Input.GetKeyDown("r")){
            Instantiate(npc, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }

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
        checkMovement();
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

    public void goToBuilding(BuildingAttributes building){
        StartCoroutine(goToBuildingCourentine(building));
    }
    public IEnumerator goToBuildingCourentine(BuildingAttributes building){
        if(building != null){
            isMovingToBuilding = true;
            movedToBuilding = false;
            Vector3 buildingPosition = new Vector3(building.getPositionX(), building.getPositionY(), building.getPositionZ());

            if(playerInsideBuilding == true){
                leaveBuilding();
            }

            agent.SetDestination(buildingPosition);

            while(isMovingToBuilding){
                if(agent.hasPath){
                    if(agent.remainingDistance <= 0.6){
                        movedToBuilding = true;
                        agent.ResetPath();
                        goInsideBuilding(building);
                        break;
                    }
                }
                yield return null;
            }
            isMovingToBuilding = false;
        }
    }

    public void goInsideBuilding(BuildingAttributes building){
        setBuildingIsInsideOf(building);
        building.setPlayerEnteredBuilding(true);
        gameManager.GetUI().buildingOpen();
        hideBody(true);

        setMovementDisabled(true);
        playerInsideBuilding = true;
    }
    public void leaveBuilding(){
        gameManager.GetUI().buildingOpenClose();
        buildingInsideOf.setPlayerEnteredBuilding(false);
        setBuildingIsInsideOf(null);
        hideBody(false);

        setMovementDisabled(false);
        playerInsideBuilding = false;
    }
    public void setBuildingIsInsideOf(BuildingAttributes building){
        buildingInsideOf = building;
    }

    public void checkMovement(){
        if(GetComponent<NavMeshAgent>().enabled == true){
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
    }

    public bool getPlayerInsideBuilding(){
        return playerInsideBuilding;
    }
}
