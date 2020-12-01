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
    public List<Town> ownedTowns;
    public Inventory inventory;
    public Toolbelt toolbelt;
    public Skills skills;
    public PerkAttributes perkAttributes;
    public string playerFirstName;
    public string playerLastName;
    public string gender;

    public bool isMovingToDestination;
    public bool reachedDestination;

    public bool touchingObstacle;
    public bool mouseOnItemResource;
    public bool movementDisabled;

    public bool isMovingToBuilding;
    public bool movedToBuilding;

    public bool playerInsideBuilding;

    public GameObject npc; //for testing

    void Awake()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        inventory = GetComponent<Inventory>();
        skills = GetComponent<Skills>();
        perkAttributes = GetComponent<PerkAttributes>();
        toolbelt = GetComponent<Toolbelt>();
    }

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

        // PLAYER MOVEMENT
        // Move player to new position when pressing mouse click
        if (Input.GetMouseButtonDown(0) && !gameManager.GetUI().getIsMouseOverUI() && !gameManager.getCraftingSystem().getIsCrafting()){
            RaycastHit hit;
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity /*, LayerMask.NameToLayer()*/);
            if(hit.collider.tag == "Ground"){
                hitGround = hit.point;
                moveToPosition(hit.point);
            }
        } 
        checkMovement();
    }
    void OnTriggerStay(Collider colliderInfo){
        
    }
    void OnTriggerExit(Collider colliderInfo){
        
    }

    // GETTERS
    public bool getIsMovingToDestination(){
        return isMovingToDestination;
    }
    public bool getReachedDestination(){
        return reachedDestination;
    }
    public Vector3 getPlayerPosition(){
        return playerPosition;
    } 
    public bool getMovementDisabled(){
        return movementDisabled;
    }
    public bool getPlayerInsideBuilding(){
        return playerInsideBuilding;
    }
    public Skills getSkills(){
        return skills;
    }
    public Inventory getInventory(){
        return inventory;
    }
    public string getPlayerFirstName(){
        return playerFirstName;
    }
    public string getPlayerLastName(){
        return playerLastName;
    }
    public List<Town> getTownsOwned(){
        return ownedTowns;
    }
    public Town getTownOwnedByIndex(int index){
        return ownedTowns[index];
    }
    public Town getTownOwnedByName(string name){
        // NAME IS UNIQE FOR TOWN EACH TOWN
        // TODO MAKE A SYSTEM FOR UNIQE NAMES
        foreach(Town town in ownedTowns){
            if(town.getTownName().Equals(name)){
                return town;
            }
        }
        return null;
    }
    // SETTERS
    public void setReachedDestination(bool newBool){
        reachedDestination = newBool;
    }
    public void setIsMovingToDestination(bool newBool){
        isMovingToDestination = newBool;
    }
    public void setMovementDisabled(bool val){
        movementDisabled = val;
    }
    public void setBuildingIsInsideOf(BuildingAttributes building){
        buildingInsideOf = building;
    }

    // FUNCTIONS 
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

    public void addTownToOwnedTowns(Town town){
        ownedTowns.Add(town);
    }

    public void removeTownFromOwnedTowns(Town town){
        ownedTowns.Remove(town);
    }

    public PerkAttributes getPerkattributes(){
        return perkAttributes;
    }
    public Vector3 getHitGround(){
        return hitGround;
    }

    public Toolbelt getToolbelt(){
        return toolbelt;
    }
}
