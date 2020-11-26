using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    private GameManager gameManager;
    private Collider player;
    public int itemAmount;
    private string itemName;

    private bool playerInBounds;
    private bool citizenInBounds;
    public bool walkingToItemBool;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(this.transform.name.Contains("Wood log")){
            itemName = "Wood log";
            itemAmount = 1;
        } else if(this.transform.name.Contains("Stone")){
            itemName = "Stone";
            itemAmount = 1;
        } else {
            itemAmount = 1;
        }

        this.transform.name = itemName;
    }

    public int getItemAmount(){
        return itemAmount;
    }
    public string getItemName(){
        return itemName;
    }
    
    public void OnMouseDown()
    {
        if(!gameManager.GetUI().getIsMouseOverUI()){
            StartCoroutine(walkingToItem()); 
        }
    }

    void OnMouseOver()
    {
        //GetComponentInChildren<Outline>().eraseRenderer = false;
    }

    void OnMouseExit()
    {
        //GetComponentInChildren<Outline>().eraseRenderer = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
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
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "player"){
            playerInBounds = false;
        }
        if(other is BoxCollider){
            if(other.tag == "Citizen"){
                citizenInBounds = false;
            }
        }
    }

    public void pickUpItem(PlayerBehavior player, Citizen citizen){
        bool itemPickedUp = false;
        if(!itemPickedUp){
            itemPickedUp = true;

            if(player){
                if(getItemAmount() != 0){
                    itemAmount = gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(getItemName(), getItemAmount());
                }
                if(itemAmount == 0){
                    player.GetComponent<Animator>().SetTrigger("pickingUpItem");
                    Destroy(this.gameObject);
                }
            }
            if(citizen){
                if(getItemAmount() != 0){
                    itemAmount = citizen.getInventory().addItemToInventory(getItemName(), getItemAmount());
                }

                if(itemAmount == 0){
                    citizen.transform.GetComponent<Citizen>().removeItemFromItemsToPickUp(this);
                    //Debug.Log("item picked up");
                    citizen.transform.GetComponent<Animator>().SetTrigger("pickingUpItem");
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public IEnumerator walkingToItem(){
        bool runLoop = true;
        gameManager.getPlayerBehavior().moveToPosition(this.transform.position);
        while(runLoop){
            if(playerInBounds){
                gameManager.getPlayerBehavior().stopPlayer();
                gameManager.getPlayerBehavior().playerLookAt(this.gameObject);
                pickUpItem(gameManager.getPlayerBehavior(), null);
                break;
            }
            yield return null;
        }
    }

    public IEnumerator walkingToItem(Citizen citizen){
        if(!walkingToItemBool){
            //Debug.Log("walking to item started");
            walkingToItemBool = true;
            citizen.goToDestination(this.transform.position);
            bool reachedResource = false;

            while(walkingToItemBool){
                if(this != null){
                    if(Vector3.Distance(this.transform.position, citizen.transform.position) <= 1.5f && !reachedResource){
                        reachedResource = true;
                        citizen.lookAt(this.gameObject);
                        citizen.stopMovement();
                        pickUpItem(null, citizen);
                        break;
                    }
                }
                yield return null;
            }
            walkingToItemBool = false;
        }
    
    }
}


