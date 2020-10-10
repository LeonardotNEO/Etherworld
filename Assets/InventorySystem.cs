using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    Inventory mainInventory = new Inventory();
    public bool playerInBoundsResource = false;

    public bool gatheringsResourcesRunning = false;
    Text inventoryTextName;
    Text inventoryTextAmount;
    public Collider colliderInfo;

    // ITEMS CURRENTLY INGAME
    Item Item0 = new Item("Wood", 0, 0);
    Item Item1 = new Item("Stone", 0, 1);
    Item Item2 = new Item("Iron", 0, 2);
    Item Item3 = new Item("Coal", 0, 3);
    public GameObject woodPile;
    public GameObject stonePile;
    public GameObject ironPile;
    public GameObject coalPile;
    
    void Start()
    {
        // ADDING ITEMS TO INVENTORY
        mainInventory.addItemToInventory(Item0);
        mainInventory.addItemToInventory(Item1);
        mainInventory.addItemToInventory(Item2);
        mainInventory.addItemToInventory(Item3);
    }

    void Update()
    {

        // COLLECTING ITEMS, EACH IF IS A RESOURCE
        if(Input.GetKeyDown("e") && playerInBoundsResource){
            playerInBoundsResource = false;

            //SPAWNING PILES FROM TREES AND DEPOTES
            if(colliderInfo.tag == "Tree" && !gatheringsResourcesRunning){
                StartCoroutine(gatheringResources(woodPile));
            }
            if(colliderInfo.tag == "StoneDepot" && !gatheringsResourcesRunning){
                StartCoroutine(gatheringResources(stonePile));
            }
            if(colliderInfo.tag == "IronDepot" && !gatheringsResourcesRunning){
                StartCoroutine(gatheringResources(ironPile));
            }
            if(colliderInfo.tag == "CoalDepot" && !gatheringsResourcesRunning){
                StartCoroutine(gatheringResources(coalPile));
            }
            //PICIKING UP PILES OF RESOURCES
            if(colliderInfo.tag == "WoodPile"){
                Item0.setIncreaseAmount(10);
                Destroy(colliderInfo.gameObject);
            }
            if(colliderInfo.tag == "StonePile"){
                Item1.setIncreaseAmount(5);
                Destroy(colliderInfo.gameObject);
            }
            if(colliderInfo.tag == "IronPile"){
                Item2.setIncreaseAmount(5);
                Destroy(colliderInfo.gameObject);
            }
            if(colliderInfo.tag == "CoalPile"){
                Item3.setIncreaseAmount(5);
                Destroy(colliderInfo.gameObject);
            }
        }

        // SHOWING INVENTORY TEXT, AND UPDATING IT
        inventoryTextName = GameObject.Find("/UI Panel/Inventory/Panel/InventoryItemsName").GetComponent<Text>();
        inventoryTextAmount = GameObject.Find("/UI Panel/Inventory/Panel/InventoryItemsAmount").GetComponent<Text>();
        inventoryTextName.text = mainInventory.getInventoryName();
        inventoryTextAmount.text = mainInventory.getInventoryAmount();
        
    }

    // TRIGGERS

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
    void OnTriggerExit(Collider collider){
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
        ){playerInBoundsResource = false;}
    }

    public IEnumerator gatheringResources(GameObject resource){
        RectTransform progressBar = GameObject.Find("/UI Panel/LoadingBar/LoadingBarProgress").GetComponent<RectTransform>();
        gatheringsResourcesRunning = true;

        // TREES
        if(colliderInfo.tag == "Tree"){
            float progressNumber = 0;
            Debug.Log("WE GET HERE");
            while(progressNumber <= 10){
                bool isMoving = GameObject.Find("/player").GetComponent<PlayerBehavior>().isMoving; 
                progressNumber += 1 * Time.deltaTime;
                progressBar.sizeDelta = new Vector2(100*progressNumber, 26.4F);

                if(isMoving){
                    progressBar.sizeDelta = new Vector2(0, 26F);
                    break;
                } else if(progressBar.sizeDelta.x > 360){
                    progressBar.sizeDelta = new Vector2(0, 26F);
                    Instantiate(woodPile, new Vector3(colliderInfo.transform.position.x + 2.5F, colliderInfo.transform.position.y - 2, colliderInfo.transform.position.z + 3f), Quaternion.identity);
                    Destroy(colliderInfo.gameObject);
                    break;
                }
                yield return null;
            }
        }

        // STONEDEPOT; IRONDEPOT; COALDEPOT
        if(colliderInfo.tag == "StoneDepot" || colliderInfo.tag == "IronDepot" || colliderInfo.tag == "CoalDepot"){
            float progressNumber = colliderInfo.GetComponent<ItemAtributes>().progress;
            float progressSpeed = 1; 

            while(progressNumber <= 360){
                bool isMoving = GameObject.Find("/player").GetComponent<PlayerBehavior>().isMoving;
                progressNumber += 10 * Time.deltaTime * progressSpeed;
                progressBar.sizeDelta = new Vector2(progressNumber, 26.4F);
                bool firstTriggered = colliderInfo.GetComponent<ItemAtributes>().firstTriggered;
                bool secondTriggered = colliderInfo.GetComponent<ItemAtributes>().secondTriggered;
                bool thirdTriggered = colliderInfo.GetComponent<ItemAtributes>().thirdTriggered;
                bool fourthTriggered = colliderInfo.GetComponent<ItemAtributes>().fourthTriggered;
                bool fifthTriggered = colliderInfo.GetComponent<ItemAtributes>().fifthTriggered;
                bool sixtTriggered = colliderInfo.GetComponent<ItemAtributes>().sixtTriggered; 

                if(isMoving){
                    progressBar.sizeDelta = new Vector2(0, 26F);
                    break;
                } 
                if(progressBar.sizeDelta.x >= 60 && firstTriggered == false){
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    colliderInfo.GetComponent<ItemAtributes>().progress = 60;
                    colliderInfo.GetComponent<ItemAtributes>().firstTriggered = true;
                    colliderInfo.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 120 && secondTriggered == false){
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    colliderInfo.GetComponent<ItemAtributes>().progress = 120;
                    colliderInfo.GetComponent<ItemAtributes>().secondTriggered = true;
                    colliderInfo.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 180 && thirdTriggered == false){
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    colliderInfo.GetComponent<ItemAtributes>().progress = 180;
                    colliderInfo.GetComponent<ItemAtributes>().thirdTriggered = true;
                    colliderInfo.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 240 && fourthTriggered == false){
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    colliderInfo.GetComponent<ItemAtributes>().progress = 240;  
                    colliderInfo.GetComponent<ItemAtributes>().fourthTriggered = true;
                    colliderInfo.GetComponent<ItemAtributes>().amountLeft--;  
                } 
                if(progressBar.sizeDelta.x >= 300 && fifthTriggered == false){
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    colliderInfo.GetComponent<ItemAtributes>().progress = 300; 
                    colliderInfo.GetComponent<ItemAtributes>().fifthTriggered = true;
                    colliderInfo.GetComponent<ItemAtributes>().amountLeft--; 
                } 
                if(progressBar.sizeDelta.x >= 360 && sixtTriggered == false){
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    Instantiate(resource, new Vector3(colliderInfo.transform.position.x + 1F, colliderInfo.transform.position.y + 3, colliderInfo.transform.position.z + 4f), Quaternion.identity);
                    colliderInfo.GetComponent<ItemAtributes>().sixtTriggered = true;
                    progressBar.sizeDelta = new Vector2(0, 26F);
                    Destroy(colliderInfo.gameObject);
                    break;
                }
                yield return null;
            }
        }
        gatheringsResourcesRunning = false;
    }
}

