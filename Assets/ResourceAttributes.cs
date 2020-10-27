using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAttributes : MonoBehaviour
{
    private GameManager gameManager;
    private Collider player;
    public GameObject resourceMined;
    public Progressbar progressbar;
    private bool playerInBounds;
    private int amountLeft = 6;
    private float progress = 0;
    private string resourceTag;
    private bool gatheringsResourcesRunning;

    private bool firstTriggered = false;
    private bool secondTriggered = false;
    private bool thirdTriggered = false;
    private bool fourthTriggered = false;
    private bool fifthTriggered = false;
    private bool sixtTriggered = false; 

    void Start()
    {
    
    }

    void Awake()
    {
        resourceTag = this.tag;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        progressbar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Progressbar>();
        
    }
    void Update()
    {
        
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
        gameManager.getPlayerBehavior().moveToPosition(this.transform.position);
        while(runLoop){
            if(playerInBounds){
                gameManager.getPlayerBehavior().stopPlayer();
                gameManager.getPlayerBehavior().playerLookAt(transform.position.x, transform.position.y, transform.position.z);
                StartCoroutine(gatheringResources());
                runLoop = false;
            }
            yield return null;
        }
    }
    public IEnumerator gatheringResources(){
        yield return new WaitForSeconds(0.3f);
        gatheringsResourcesRunning = true;
        player.GetComponent<Animator>().SetBool("isGatheringResources" , true);

        // TREES
        if(resourceTag == "Tree"){
            float progress = 0;
            float progressSpeed = 50;

            while(progress <= 360){
                progress += Time.deltaTime * progressSpeed;
                progressbar.updateProgressBar(progress);

                if(gameManager.getPlayerBehavior().getIsMovingToDestination()){
                    gatheringsResourcesRunning = false;
                    player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                    progressbar.resetProgressBar();
                    break;
                } 
                if(progress >= 360){
                    Instantiate(resourceMined, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z -0.5f), Quaternion.identity);
                    Instantiate(resourceMined, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 0.5f), Quaternion.identity);
                    Instantiate(resourceMined, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
                    player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                    progressbar.resetProgressBar();
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
                progressbar.updateProgressBar(progress);

                if(gameManager.getPlayerBehavior().getIsMovingToDestination()){
                    gatheringsResourcesRunning = false;
                    player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
                    progressbar.resetProgressBar();
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
    }
    public void gatheringResourceSteps(string animationTrigger){
        Instantiate(resourceMined, new Vector3(player.transform.position.x - 1.5F, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity);
        //GetComponent<Animator>().SetTrigger(animationTrigger);
        amountLeft--;
        if(amountLeft == 0){
            player.GetComponent<Animator>().SetBool("isGatheringResources" , false);
            gatheringsResourcesRunning = false;
            progressbar.resetProgressBar();
            Destroy(gameObject);
        }       
    }
}
