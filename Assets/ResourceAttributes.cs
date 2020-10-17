using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAttributes : MonoBehaviour
{
    PlayerBehavior playerBehavior;
    Inventory mainInventory;
    Collider player;
    RectTransform progressBar;
    public GameObject resourceMined;
    
    public bool playerInBounds;
    public int amountLeft = 6;
    public float progress = 0;
    public string resourceTag;
    public bool gatheringsResourcesRunning;

    public bool firstTriggered = false;
    public bool secondTriggered = false;
    public bool thirdTriggered = false;
    public bool fourthTriggered = false;
    public bool fifthTriggered = false;
    public bool sixtTriggered = false; 

    void Start()
    {
        resourceTag = this.tag;
    }

    void Update()
    {
        playerBehavior = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getPlayerBehavior();
        mainInventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getInventoryCatalog().getMainInventory();

        // Gathering resources
        if(Input.GetKeyDown("e") && playerInBounds && !gatheringsResourcesRunning){
            StartCoroutine(gatheringResources());
        }

        // Setting up progressbar, and that it will update each frame
        progressBar = GameObject.Find("/UI Panel/LoadingBar/LoadingBarProgress").GetComponent<RectTransform>();
    }

    void OnMouseDown()
    {
        if(!gatheringsResourcesRunning){
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

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "player"){
            player = other;
            playerInBounds = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "player"){
            playerInBounds = false;
        }
    }

    public IEnumerator walkingToResource(){
        bool runLoop = true;
        while(runLoop){
            if(playerInBounds){
                playerBehavior.setPlayerRaycastHitVector(playerBehavior.getPlayerPosition());
                playerBehavior.setReachedDestination(true);
                StartCoroutine(gatheringResources());
                runLoop = false;
            }
            yield return null;
        }
    }
    public IEnumerator gatheringResources(){
        gatheringsResourcesRunning = true;
        playerBehavior.setIsMovingToDestination(false);
        player.GetComponent<Animator>().SetBool("isGatheringResources" , true);

        // TREES
        if(resourceTag == "Tree"){
            float progressNumber = 0;
            float progressSpeed = 50;

            while(progressNumber <= 360){
                progressNumber += Time.deltaTime * progressSpeed;
                progressBar.sizeDelta = new Vector2(progressNumber, 26.4F);

                if(playerBehavior.getIsMovingToDestination()){
                    player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                    resetProgressBar();
                    break;
                } 
                if(progressNumber >= 360){
                    Instantiate(resourceMined, new Vector3(transform.position.x + 1.5F, transform.position.y + 1, transform.position.z + 1f), Quaternion.identity);
                    player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                    resetProgressBar();
                    Destroy(gameObject);
                }
                yield return null;
            }
        }
        // STONEDEPOT; IRONDEPOT; COALDEPOT
        if(resourceTag == "StoneDepot" || resourceTag == "IronDepot" || resourceTag == "CoalDepot"){
            float progressSpeed = 30;

            while(progress <= 360){
                progress += Time.deltaTime * progressSpeed;
                progressBar.sizeDelta = new Vector2(progress, 26.4F);

                if(playerBehavior.getIsMovingToDestination()){
                    player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                    resetProgressBar();
                    break;
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
        gatheringsResourcesRunning = false;
        playerBehavior.setTouchingObstacle(false);
    }
    public void resetProgressBar(){
        progressBar.sizeDelta = new Vector2(0, 26F);
    }
    public void gatheringResourceSteps(string animationTrigger){
        Instantiate(resourceMined, new Vector3(transform.position.x + 1.5F, transform.position.y + 1, transform.position.z + 1f), Quaternion.identity);
        //GetComponent<Animator>().SetTrigger(animationTrigger);
        amountLeft--;
        if(amountLeft == 0){
            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
            gatheringsResourcesRunning = false;
            resetProgressBar();
            Destroy(gameObject);
        }       
    }
}
