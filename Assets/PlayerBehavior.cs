using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    public float playerSpeed = 2;
    public bool isMoving = false;
    public bool playerInBoundsResource = false;
    public bool playerInBoundsItems = false;
    public bool gatheringsResourcesRunning = false;
    public bool touchingObstacle = false;
    public bool mouseOnItemResource = false;
    bool pingPong;
    public Collider colliderInfo;
    public Collider resourceColliderInfo;
    RaycastHit mouseButtonPressed;
    RaycastHit hitItemResource;
    RaycastHit hitItemResourceSaved;
    RectTransform progressBar;
    void Start()
    {
        // Makes the position of the ray the same as the player (so the player doesent automaticaly move at the start)
        mouseButtonPressed.point = transform.position; 
    }
    
    void Update()
    {
        // Setting up progressbar, and that it will update each frame
        progressBar = GameObject.Find("/UI Panel/LoadingBar/LoadingBarProgress").GetComponent<RectTransform>();

        // PLAYER MOVEMENT
        // Move player to new position when pressing mouse click
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI()) {
            Ray movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(movementRay, out mouseButtonPressed, Mathf.Infinity, LayerMask.GetMask("Ground"))){ //specify to hit layer 8
                isMoving = true;
            } else {
                isMoving = false;
            }
        }  
        // Player moves slower is there is a obstacle (So that the player doesnt accidentaly clip through)
        if(touchingObstacle == true){
            playerSpeed = 0.3f;
        } else {
            playerSpeed = 4;
        }
        // check if player has reached new destination(HIT.POINT), if it has, set walking bool to false
        if(transform.position == mouseButtonPressed.point){
            isMoving = false;
        }
        // moves the player to new postition and play animation accordingly
        if(isMoving == true){
            transform.position = Vector3.MoveTowards(transform.position, mouseButtonPressed.point /*+ offset*/, playerSpeed * Time.deltaTime);
            GetComponent<Animator>().SetBool("isMoving" , true);
            transform.LookAt(mouseButtonPressed.point);
        } else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }

        // PLAYER HOVER OVER ITEMS
        Ray hoverRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(hoverRay, out hitItemResource, Mathf.Infinity, LayerMask.GetMask("ResourcesMesh", "ItemsMesh"))){
            hitItemResourceSaved = hitItemResource;
            mouseOnItemResource = true;
        } else {
            mouseOnItemResource = false;
        }

        if(mouseOnItemResource){
            hitItemResource.collider.GetComponentInChildren<Outline>().eraseRenderer = false;

            Color c = GameObject.FindWithTag("MainCamera2").GetComponent<OutlineEffect>().lineColor0;
            if(pingPong)
            {
                c.a += Time.deltaTime;
                if(c.a >= 1)
                    pingPong = false;
            } else {
                c.a -= Time.deltaTime;
                if(c.a <= 0.4)
                    pingPong = true;
            }
            c.a = Mathf.Clamp01(c.a);
            GameObject.FindWithTag("MainCamera2").GetComponent<OutlineEffect>().lineColor0 = c;
            GameObject.FindWithTag("MainCamera2").GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        } else {
            if(hitItemResourceSaved.collider){
                hitItemResourceSaved.collider.GetComponentInChildren<Outline>().eraseRenderer = true;
            }
        }
        



        // Gathering resources
        if(playerInBoundsResource && touchingObstacle == false){ //We do this so that the mesh collider doesnt get choosen
            resourceColliderInfo = colliderInfo;
        }
        if(Input.GetKeyDown("e") && playerInBoundsResource && !gatheringsResourcesRunning || (mouseOnItemResource && playerInBoundsResource && Input.GetMouseButtonDown(0) && !gatheringsResourcesRunning)){
            playerInBoundsResource = false;
            isMoving = false;
            StartCoroutine(gatheringResources(resourceColliderInfo.GetComponent<ResourceAttributes>().getResourceMined()));
        }

        //Picking up items from the ground
        if(Input.GetKeyDown("e") && playerInBoundsItems){
            playerInBoundsItems = false;

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
    }

    // Triggers when player collides with other objects; sets collider bool to true if it collides with these objects
    void OnTriggerStay(Collider collider){
        colliderInfo = collider;
        if(colliderInfo.gameObject.layer == 9 /*Layer 9 is RESOURCES*/){
            playerInBoundsResource = true;
        }
        if(colliderInfo.gameObject.layer == 10 /*Layer 10 is ITEMS*/){
            playerInBoundsItems = true;
        }
        if(
            colliderInfo.gameObject.layer == 12  || /*Layer 12 is BUILDINGS*/
            colliderInfo.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            colliderInfo.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
            ){touchingObstacle = true;
        }
    }

    // no collider detected if player has moved out of collider
    void OnTriggerExit(Collider collider){
        colliderInfo = null;
        playerInBoundsResource = false;
        playerInBoundsItems = false;
        touchingObstacle = false;
    }

    public IEnumerator gatheringResources(GameObject resource){
        GameObject x = colliderInfo.gameObject;
        gatheringsResourcesRunning = true;

        // TREES
        if(x.tag == "Tree"){
            float progressNumber = 0;
            float progressSpeed = 50;

            while(progressNumber <= 360){
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
        playerInBoundsResource = false;
        touchingObstacle = false;
    }

    public void pickUpItem(string itemName, int amountToAdd){
        GetComponent<Inventory>().addItemToInventory(new Item(colliderInfo.tag, amountToAdd));
        GetComponent<Animator>().SetTrigger("pickingUpItem");
        Destroy(colliderInfo.gameObject);
    }
    public void resetProgressBar(){
        progressBar.sizeDelta = new Vector2(0, 26F);
    }

    public bool isMouseOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }
}
