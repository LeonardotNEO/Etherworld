using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAttributes : MonoBehaviour
{
    private GameManager gameManager;
    private Collider player;
    public GameObject resourceMined;
    public Progressbar progressbar;

    public string resourceName;
    public int amountLeft = 6;
    public float progress = 0;

    public bool playerInBounds;
    public bool citizenInBounds;
    public bool gatheringsResourcesRunning;
    public bool walkingToResourceBool;

    void Start()
    {
        if(this.transform.name.Contains("Tree")){
            resourceName = "Tree";
        }
        if(this.transform.name.Contains("StoneDepot")){
            resourceName = "StoneDepot";
        }
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        progressbar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Progressbar>(); 
    }

    void OnMouseDown()
    {
        if(!gatheringsResourcesRunning && !gameManager.GetUI().getIsMouseOverUI() && !gameManager.getCraftingSystem().getIsCrafting()){
            StartCoroutine(walkingToResource());
        }
    }
    void OnMouseEnter()
    {
        //GetComponentInChildren<Outline>().enabled = true;
        //GetComponentInChildren<Outline>().eraseRenderer = false;
    }

    void OnMouseExit()
    {
        //GetComponentInChildren<Outline>().enabled = false;
    }

    public GameObject getResourceMined(){
        return resourceMined;
    }

    public void decreaseAmountLeft(){
        amountLeft--;
    }

    public void increaseProgress(int addProgress){
        progress += addProgress;
    }

    void OnTriggerEnter(Collider other)
    {
        if(!gatheringsResourcesRunning){
            if(other.tag == "player"){
                player = other;
                playerInBounds = true;
            }

            if(other is BoxCollider){
                if(other.tag == "Citizen"){
                    player = other;
                    citizenInBounds = true;
                }
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(!gatheringsResourcesRunning){
            if(other.tag == "player"){
                playerInBounds = false;
            }

            if(other is BoxCollider){
                if(other.tag == "Citizen"){
                    citizenInBounds = false;
                }
            }
        }
        
    }

    public IEnumerator walkingToResource(){
        if(!walkingToResourceBool){
            walkingToResourceBool = true;
            gameManager.getPlayerBehavior().moveToPosition(this.transform.position);
            while(walkingToResourceBool){
                if(playerInBounds){
                    gameManager.getPlayerBehavior().stopPlayer();
                    gameManager.getPlayerBehavior().playerLookAt(this.gameObject);
                    StartCoroutine(gatheringResources());
                    break;
                }
                yield return null;
            }
            walkingToResourceBool = false;
        }
    }
    public IEnumerator walkingToResource(Citizen citizen){
        if(!walkingToResourceBool){
            walkingToResourceBool = true;
            citizen.goToDestination(this.transform.position);
            bool reachedResource = false;

            while(walkingToResourceBool){
                if(this != null){
                    if(Vector3.Distance(this.transform.position, citizen.transform.position) <= 1f && !reachedResource){
                        reachedResource = true;
                        citizen.stopMovement();
                        StartCoroutine(gatheringResources());
                        break;
                    }
                }
                yield return null;
            }
            walkingToResourceBool = false;
        }
    
    }

    public IEnumerator gatheringResources(){
        yield return new WaitForSeconds(0.3f);
        if(!gatheringsResourcesRunning){
            gatheringsResourcesRunning = true;
            player.GetComponent<Animator>().SetBool("isGatheringResources" , true);

            if(player.tag == "Citizen"){
                player.GetComponent<Citizen>().setResourceBeingMined(this);
                player.GetComponent<Citizen>().lookAt(this.gameObject);
            }

            // TREES
            if(resourceName == "Tree"){
                float progress = 0;
                float progressSpeed = player.GetComponent<Skills>().getSkillByName("Woodcutting").getSpeedMultiplier();
                bool instantiated = false;

                while(progress <= 360){
                    progress += Time.deltaTime * progressSpeed * 10;
                    if(player.tag == "player"){
                        progressbar.updateProgressBar(progress);
                    }
                    

                    if(player.tag == "player"){
                        if(gameManager.getPlayerBehavior().getIsMovingToDestination()){
                            gatheringsResourcesRunning = false;
                            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                            progressbar.resetProgressBar();
                            break;
                        } 
                    }
                    /*if(player.tag == "Citizen"){
                        if(player.GetComponent<Citizen>().getIsMovingToDestination()){
                            gatheringsResourcesRunning = false;
                            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                            break;
                        } 
                    }*/
                    if(progress >= 360 && !instantiated){
                        instantiated = true;
                        GameObject woodLog1 = Instantiate(resourceMined, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z -0.5f), Quaternion.identity);
                        GameObject woodLog2 = Instantiate(resourceMined, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 0.5f), Quaternion.identity);
                        GameObject woodLog3 = Instantiate(resourceMined, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                        woodLog1.name = resourceMined.name;
                        woodLog2.name = resourceMined.name;
                        woodLog3.name = resourceMined.name;

                        player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                        
                        if(player.tag == "player"){
                            progressbar.resetProgressBar();
                        }

                        player.GetComponent<Skills>().getSkillByName("Woodcutting").increaseExperience(400);

                        if(player.tag == "Citizen"){
                            player.GetComponent<Citizen>().setResourceBeingMined(null);
                            player.GetComponent<Citizen>().addItemToItemsToPickUp(woodLog1.GetComponent<ItemAttributes>());
                            player.GetComponent<Citizen>().addItemToItemsToPickUp(woodLog2.GetComponent<ItemAttributes>());
                            player.GetComponent<Citizen>().addItemToItemsToPickUp(woodLog3.GetComponent<ItemAttributes>());
                        }

                        
                        Destroy(gameObject);
                    }
                    yield return null;
                }
            }
            // STONEDEPOT; IRONDEPOT; COALDEPOT
            if(resourceName == "StoneDepot" || resourceName == "IronDepot" || resourceName == "CoalDepot"){
                float progressSpeed = player.GetComponent<Skills>().getSkillByName("Mining").getSpeedMultiplier();
                bool firstTriggered = false;
                bool secondTriggered = false;
                bool thirdTriggered = false;
                bool fourthTriggered = false;
                bool fifthTriggered = false;
                bool sixtTriggered = false; 

                while(progress <= 360){
                    progress += Time.deltaTime * progressSpeed;
                    if(player.tag == "player"){
                        progressbar.updateProgressBar(progress);
                    }

                    if(player.tag == "player"){
                        if(gameManager.getPlayerBehavior().getIsMovingToDestination()){
                            gatheringsResourcesRunning = false;
                            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                            progressbar.resetProgressBar();
                            break;
                        } 
                    }/*
                    if(player.tag == "Citizen"){
                        if(player.GetComponent<Citizen>().getIsMovingToDestination()){
                            gatheringsResourcesRunning = false;
                            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                            break;
                        } 
                    }*/
                    if(progress >= 60 && !firstTriggered){
                        gatheringResourceSteps("first");
                        firstTriggered = true;
                    }
                    if(progress >= 120 && !secondTriggered){
                        gatheringResourceSteps("second");
                        secondTriggered = true;
                    }
                    if(progress >= 180 && !thirdTriggered){
                        gatheringResourceSteps("third");
                        thirdTriggered = true;
                    }
                    if(progress >= 240 && !fourthTriggered){
                        gatheringResourceSteps("fourth");
                        fourthTriggered = true;
                    }
                    if(progress >= 300 && !fifthTriggered){
                        gatheringResourceSteps("fifth");
                        fifthTriggered = true;
                    }
                    if(progress >= 360 && !sixtTriggered){
                        gatheringResourceSteps("sixt");
                        sixtTriggered = true;
                        yield return new WaitForSeconds(2);
                    }
                    yield return null;
                }
            }
        }
        yield return new WaitForSeconds(1);
        gatheringsResourcesRunning = false;
    }
    public void gatheringResourceSteps(string animationTrigger){
        GameObject stone = Instantiate(resourceMined, new Vector3(player.transform.position.x - 1.5F, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity);
        stone.name = resourceMined.name;

        
        player.GetComponent<Skills>().getSkillByName("Mining").increaseExperience(400);

        //GetComponent<Animator>().SetTrigger(animationTrigger);
        amountLeft--;
        if(amountLeft == 0){
            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
            gatheringsResourcesRunning = false;
            if(player.tag == "player"){
                progressbar.resetProgressBar();
            }
            if(player.tag == "Citizen"){
                player.GetComponent<Citizen>().setResourceBeingMined(null);
            }
            Destroy(gameObject);
        }       
    }

    public bool getGatheringResourcesRunning(){
        return gatheringsResourcesRunning;
    }

    public bool getWalkingToResource(){
        return walkingToResourceBool;
    }

    public string getResourceName(){
        return resourceName;
    }
}
