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

    RectTransform progressBar;
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
        progressBar = GameObject.Find("/UI Panel/LoadingBar/LoadingBarProgress").GetComponent<RectTransform>();

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
                GetComponent<Animator>().SetTrigger("pickingUpItem");
            }
            if(colliderInfo.tag == "StonePile"){
                Item1.setIncreaseAmount(5);
                Destroy(colliderInfo.gameObject);
                GetComponent<Animator>().SetTrigger("pickingUpItem");
            }
            if(colliderInfo.tag == "IronPile"){
                Item2.setIncreaseAmount(5);
                Destroy(colliderInfo.gameObject);
                GetComponent<Animator>().SetTrigger("pickingUpItem");
            }
            if(colliderInfo.tag == "CoalPile"){
                Item3.setIncreaseAmount(5);
                Destroy(colliderInfo.gameObject);
                GetComponent<Animator>().SetTrigger("pickingUpItem");
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
                    Instantiate(woodPile, new Vector3(x.transform.position.x + 1f, x.transform.position.y + 1f, x.transform.position.z), Quaternion.identity);
                    Destroy(x.gameObject);
                    break;
                }
                yield return null;
            }
        }

        // STONEDEPOT; IRONDEPOT; COALDEPOT
        if(colliderInfo.tag == "StoneDepot" || colliderInfo.tag == "IronDepot" || colliderInfo.tag == "CoalDepot"){
            float progressNumber = x.GetComponent<ItemAtributes>().progress; 
            float progressSpeed = 30;

            while(progressNumber <= 360){
                bool isMoving = GameObject.Find("/player").GetComponent<PlayerBehavior>().isMoving;
                progressNumber += Time.deltaTime * progressSpeed;
                progressBar.sizeDelta = new Vector2(progressNumber, 26.4F);

                bool firstTriggered = x.GetComponent<ItemAtributes>().firstTriggered;
                bool secondTriggered = x.GetComponent<ItemAtributes>().secondTriggered;
                bool thirdTriggered = x.GetComponent<ItemAtributes>().thirdTriggered;
                bool fourthTriggered = x.GetComponent<ItemAtributes>().fourthTriggered;
                bool fifthTriggered = x.GetComponent<ItemAtributes>().fifthTriggered;
                bool sixtTriggered = x.GetComponent<ItemAtributes>().sixtTriggered;


                if(isMoving){
                    resetProgressBar();
                    break;
                } 
                if(progressBar.sizeDelta.x >= 60 && firstTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("first");
                    x.GetComponent<ItemAtributes>().progress = 60;
                    x.GetComponent<ItemAtributes>().firstTriggered = true;
                    x.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 120 && secondTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("second");
                    x.GetComponent<ItemAtributes>().progress = 120;
                    x.GetComponent<ItemAtributes>().secondTriggered = true;
                    x.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 180 && thirdTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("third");
                    x.GetComponent<ItemAtributes>().progress = 180;
                    x.GetComponent<ItemAtributes>().thirdTriggered = true;
                    x.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 240 && fourthTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("fourth");
                    x.GetComponent<ItemAtributes>().progress = 240;
                    x.GetComponent<ItemAtributes>().fourthTriggered = true;
                    x.GetComponent<ItemAtributes>().amountLeft--; 
                } 
                if(progressBar.sizeDelta.x >= 300 && fifthTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<Animator>().SetTrigger("fifth");
                    x.GetComponent<ItemAtributes>().progress = 300;
                    x.GetComponent<ItemAtributes>().fifthTriggered = true;
                    x.GetComponent<ItemAtributes>().amountLeft--;
                } 
                if(progressBar.sizeDelta.x >= 360 && sixtTriggered == false){
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    Instantiate(resource, new Vector3(x.transform.position.x + 1.5F, x.transform.position.y + 1, x.transform.position.z + 1f), Quaternion.identity);
                    x.GetComponent<ItemAtributes>().sixtTriggered = true;
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

    public void resetProgressBar(){
        progressBar.sizeDelta = new Vector2(0, 26F);
    }
}

