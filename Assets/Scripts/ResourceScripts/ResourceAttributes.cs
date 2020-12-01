using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAttributes : MonoBehaviour
{
    private GameManager gameManager;
    private Transform player;
    public GameObject resourceMined;
    public Progressbar progressbar;

    public string resourceName;
    public int amountLeft = 6;
    public float progress = 0;
    public float distanceCenter;

    public bool playerInBounds;
    public bool citizenInBounds;
    public bool gatheringsResourcesRunning;
    public bool walkingToResourceBool;

    void Start()
    {
        distanceCenter = 2f;
        if(this.transform.name.Contains("Tree")){
            resourceName = "Tree";
            distanceCenter = 1;
        }
        if(this.transform.name.Contains("Stone depot")){
            resourceName = "Stone depot";
            distanceCenter = 3f;
        }
        if(this.transform.name.Contains("Coal depot")){
            resourceName = "Coal depot";
            distanceCenter = 3f;
        }
        if(this.transform.name.Contains("Marble depot")){
            resourceName = "Marble depot";
            distanceCenter = 3f;
        }
        if(this.transform.name.Contains("Tin depot")){
            resourceName = "Tin depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Copper depot")){
            resourceName = "Copper depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Iron depot")){
            resourceName = "Iron depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Silver depot")){
            resourceName = "Silver depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Gold depot")){
            resourceName = "Gold depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Kimberlite depot")){
            resourceName = "Kimberlite depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Neonium depot")){
            resourceName = "Neonium depot";
            distanceCenter = 2f;
        }
        if(this.transform.name.Contains("Ethereum depot")){
            resourceName = "Ethereum depot";
            distanceCenter = 2f;
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

    public GameObject getResourceMined(){
        return resourceMined;
    }

    public void decreaseAmountLeft(){
        amountLeft--;
    }

    public void increaseProgress(int addProgress){
        progress += addProgress;
    }

    public IEnumerator walkingToResource(){
        if(!walkingToResourceBool){
            walkingToResourceBool = true;
            bool reachedResource = false;
            gameManager.getPlayerBehavior().moveToPosition(this.transform.position);

            while(walkingToResourceBool){
                if(Vector3.Distance(this.transform.position, gameManager.getPlayerBehavior().getPlayerPosition()) <= distanceCenter && !reachedResource){
                    reachedResource = true;
                    gameManager.getPlayerBehavior().stopPlayer();
                    StartCoroutine(gatheringResources(gameManager.getPlayerBehavior(), null));
                    break;
                }
                // IF GATHERING RESOURCE STARTED BREAK??
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
                    if(Vector3.Distance(this.transform.position, citizen.transform.position) <= distanceCenter && !reachedResource){
                        reachedResource = true;
                        citizen.stopMovement();
                        StartCoroutine(gatheringResources(null, citizen));
                        break;
                    }
                }
                yield return null;
            }
            walkingToResourceBool = false;
        }
    
    }

    public IEnumerator gatheringResources(PlayerBehavior playerBehavior, Citizen citizen){
        yield return new WaitForSeconds(0.3f);
        if(!gatheringsResourcesRunning){
            gatheringsResourcesRunning = true;

            if(citizen){
                citizen.setResourceBeingMined(this);
                citizen.lookAt(this.gameObject);
                player = citizen.transform;
                player.GetComponent<Animator>().SetBool("isGatheringResources" , true);
            }
            if(playerBehavior){
                playerBehavior.playerLookAt(this.gameObject);
                player = playerBehavior.transform;
                player.GetComponent<Animator>().SetBool("isGatheringResources" , true);
            }

            // TREES
            if(resourceName == "Tree" || 
            resourceName == "Tin depot" ||
            resourceName == "Copper depot" ||
            resourceName == "Iron depot" ||
            resourceName == "Silver depot" ||
            resourceName == "Gold depot" ||
            resourceName == "Kimberlite depot" ||
            resourceName == "Neonium depot" ||
            resourceName == "Ethereum depot"){
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
                        
                        if(playerBehavior){
                            progressbar.resetProgressBar();
                        }

                        player.GetComponent<Skills>().increaseExperience("Woodcutting", 400);

                        if(citizen){
                            citizen.setResourceBeingMined(null);
                            citizen.addItemToItemsToPickUp(woodLog1.GetComponent<ItemAttributes>());
                            citizen.addItemToItemsToPickUp(woodLog2.GetComponent<ItemAttributes>());
                            citizen.addItemToItemsToPickUp(woodLog3.GetComponent<ItemAttributes>());
                        }

                        
                        Destroy(gameObject);
                    }
                    yield return null;
                }
            }
            // STONEDEPOT; IRONDEPOT; COALDEPOT
            if(resourceName == "Stone depot" || resourceName == "Coal depot" || resourceName == "Marble depot"){
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

                    if(playerBehavior){
                        if(gameManager.getPlayerBehavior().getIsMovingToDestination()){
                            gatheringsResourcesRunning = false;
                            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                            progressbar.resetProgressBar();
                            break;
                        } 
                    }
                    if(citizen){
                        if(citizen.getIsMovingToDestination()){
                            gatheringsResourcesRunning = false;
                            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                            break;
                        } 
                    }
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

        if(player.GetComponent<Citizen>()){
            player.GetComponent<Citizen>().addItemToItemsToPickUp(stone.GetComponent<ItemAttributes>());
        }

        player.GetComponent<Skills>().increaseExperience("Mining", 400);

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
