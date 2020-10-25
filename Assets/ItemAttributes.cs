using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    PlayerBehavior playerBehavior;
    GameManager gameManager;
    Inventory mainInventory;
    Collider player;
    public int itemAmount;
    public string itemName;
    public bool playerInBounds;


    void Awake()
    {
        itemName = this.tag;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerBehavior = gameManager.getPlayerBehavior();
    }

    void Update()
    {
        mainInventory = gameManager.getInventoryCatalog().getMainInventory();
    }

    void Start()
    {

    }

    public int getItemAmount(){
        return itemAmount;
    }
    public string getItemName(){
        return itemName;
    }
    
    public void OnMouseDown()
    {
        if(!gameManager.getIsMouseOverUI()){
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

    public void pickUpItem(){
        mainInventory.addItemToInventory(new Dictionary<string, int>{{getItemName(), getItemAmount()}});
        player.GetComponent<Animator>().SetTrigger("pickingUpItem");
        Destroy(this.gameObject);
    }

    public IEnumerator walkingToItem(){
        bool runLoop = true;
        playerBehavior.moveToPosition(this.transform.position);
        while(runLoop){
            if(playerInBounds){
                playerBehavior.stopPlayer();
                playerBehavior.playerLookAt(transform.position.x, transform.position.y, transform.position.z);
                pickUpItem();
                runLoop = false;
            }
            yield return null;
        }
    }
}


