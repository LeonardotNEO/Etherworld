using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public float playerSpeed = 2;
    public bool isMoving = false;
    public bool playerInBoundsResource = false;
    public bool gatheringsResourcesRunning = false;
    public Collider colliderInfo;
    RaycastHit hit;
    RectTransform progressBar;
    void Start()
    {
        // Makes the position of the ray the same as the player (so the player doesent automaticaly move at the start)
        hit.point = transform.position; 
    }
    
    void Update()
    {
        // Setting up progressbar, and that it will update each frame
        progressBar = GameObject.Find("/UI Panel/LoadingBar/LoadingBarProgress").GetComponent<RectTransform>();

        // Move player to new position when pressing mouse click
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ground"){
                isMoving = true;
            } else {
                isMoving = false;
            }
            
        }

        // When in bounds of resource and pressing e, do action (Pick up item, start gathering resources)
        if(Input.GetKeyDown("e") && playerInBoundsResource){
            playerInBoundsResource = false;

            //Gathering resources from tree or depot
            if((colliderInfo.tag == "Tree" || colliderInfo.tag == "StoneDepot" || colliderInfo.tag == "IronDepot" || colliderInfo.tag == "CoalDepot") && !gatheringsResourcesRunning){
                StartCoroutine(gatheringResources(colliderInfo.GetComponent<ResourceAttributes>().getResourceMined()));
            }
            //Picking up items from the ground
            if(colliderInfo.tag == "WoodPile"){
                pickUpItem(colliderInfo.tag, 20);
            }
            if(colliderInfo.tag == "StonePile"){
                pickUpItem(colliderInfo.tag, 10);
            }
            if(colliderInfo.tag == "IronPile"){
                pickUpItem(colliderInfo.tag, 5);
            }
            if(colliderInfo.tag == "CoalPile"){
                pickUpItem(colliderInfo.tag, 5);
            }
        }




        // Play animation when gathering resources
        if(gatheringsResourcesRunning == true){
            GetComponent<Animator>().SetBool("isGatheringResources" , true);
        } else {
            GetComponent<Animator>().SetBool("isGatheringResources" , false);
        }

        // check if player has reached new destination(HIT.POINT), if it has, set walking bool to false
        if(transform.position == hit.point){
            isMoving = false;
        }
        // moves the player to new postition
        if(isMoving == true){
            transform.position = Vector3.MoveTowards(transform.position, hit.point /*+ offset*/, playerSpeed * Time.deltaTime);
            GetComponent<Animator>().SetBool("isMoving" , true);
            transform.LookAt(hit.point);
        } else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

    // Triggers when player collides with other objects; sets collider bool to true if it collides with these objects
    void OnTriggerStay(Collider collider){
        colliderInfo = collider;
        if(
            colliderInfo.tag == "Tree"              ||
            colliderInfo.tag == "StoneDepot"        ||
            colliderInfo.tag == "IronDepot"         ||
            colliderInfo.tag == "CoalDepot"         ||
            colliderInfo.tag == "WoodPile"          ||
            colliderInfo.tag == "IronPile"          ||
            colliderInfo.tag == "CoalPile"          ||
            colliderInfo.tag == "StonePile" 
        ){playerInBoundsResource = true;}
    }

    // no collider detected if player has moved out of collider
    void OnTriggerExit(Collider collider){
        colliderInfo = null;
        playerInBoundsResource = false;
    }

    public IEnumerator gatheringResources(GameObject resource){
        GameObject x = colliderInfo.gameObject;
        gatheringsResourcesRunning = true;

        // TREES
        if(x.tag == "Tree"){
            float progressNumber = 0;
            float progressSpeed = 50;

            while(progressNumber <= 360){
                bool isMoving = GameObject.Find("/player").GetComponent<PlayerBehavior>().isMoving; 
                progressNumber += Time.deltaTime * progressSpeed;
                progressBar.sizeDelta = new Vector2(progressNumber, 26.4F);

                if(isMoving){
                    resetProgressBar();
                    break;
                } else if(progressBar.sizeDelta.x >= 360){
                    resetProgressBar();
                    Instantiate(resource, new Vector3(x.transform.position.x + 1f, x.transform.position.y + 1f, x.transform.position.z), Quaternion.identity);
                    Destroy(x.gameObject);
                    break;
                }
                yield return null;
            }
        }

        // STONEDEPOT; IRONDEPOT; COALDEPOT
        if(colliderInfo.tag == "StoneDepot" || colliderInfo.tag == "IronDepot" || colliderInfo.tag == "CoalDepot"){
            float progressNumber = x.GetComponent<ResourceAttributes>().progress; 
            float progressSpeed = 30;

            while(progressNumber <= 360){
                bool isMoving = GameObject.Find("/player").GetComponent<PlayerBehavior>().isMoving;
                progressNumber += Time.deltaTime * progressSpeed;
                progressBar.sizeDelta = new Vector2(progressNumber, 26.4F);

                bool firstTriggered = x.GetComponent<ResourceAttributes>().firstTriggered;
                bool secondTriggered = x.GetComponent<ResourceAttributes>().secondTriggered;
                bool thirdTriggered = x.GetComponent<ResourceAttributes>().thirdTriggered;
                bool fourthTriggered = x.GetComponent<ResourceAttributes>().fourthTriggered;
                bool fifthTriggered = x.GetComponent<ResourceAttributes>().fifthTriggered;
                bool sixtTriggered = x.GetComponent<ResourceAttributes>().sixtTriggered;


                if(isMoving){
                    resetProgressBar();
                    break;
                } 
                if(progressBar.sizeDelta.x >= 60 && firstTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("first");
                    x.GetComponent<ResourceAttributes>().progress = 60;
                    x.GetComponent<ResourceAttributes>().firstTriggered = true;
                    x.GetComponent<ResourceAttributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 120 && secondTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("second");
                    x.GetComponent<ResourceAttributes>().progress = 120;
                    x.GetComponent<ResourceAttributes>().secondTriggered = true;
                    x.GetComponent<ResourceAttributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 180 && thirdTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("third");
                    x.GetComponent<ResourceAttributes>().progress = 180;
                    x.GetComponent<ResourceAttributes>().thirdTriggered = true;
                    x.GetComponent<ResourceAttributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 240 && fourthTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("fourth");
                    x.GetComponent<ResourceAttributes>().progress = 240;
                    x.GetComponent<ResourceAttributes>().fourthTriggered = true;
                    x.GetComponent<ResourceAttributes>().amountLeft--; 
                } 
                if(progressBar.sizeDelta.x >= 300 && fifthTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("fifth");
                    x.GetComponent<ResourceAttributes>().progress = 300;
                    x.GetComponent<ResourceAttributes>().fifthTriggered = true;
                    x.GetComponent<ResourceAttributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 360 && sixtTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<ResourceAttributes>().sixtTriggered = true;
                    x.GetComponent<Animator>().SetTrigger("final");
                    resetProgressBar();
                    yield return new WaitForSeconds(2);
                    Destroy(x.gameObject);
                    break;
                }
                yield return null;
            }
        }
        gatheringsResourcesRunning = false;
    }

    public void pickUpItem(string itemName, int amountToAdd){
        GetComponent<Inventory>().addItemToInventory(new Item(colliderInfo.tag, amountToAdd));
        GetComponent<Animator>().SetTrigger("pickingUpItem");
        Destroy(colliderInfo.gameObject);
    }
    public void resetProgressBar(){
        progressBar.sizeDelta = new Vector2(0, 26F);
    }
}
