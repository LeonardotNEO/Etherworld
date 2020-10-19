using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public PlayerBehavior playerBehavior;
    public Inventory mainInventory;
    public Collider player;
    public int itemAmount;
    public string itemName;
    public bool playerInBounds;

    void Awake()
    {
        itemName = this.tag;
    }

    void Update()
    {
        playerBehavior = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getPlayerBehavior();
        mainInventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getInventoryCatalog().getMainInventory();
    }

    public int getItemAmount(){
        return itemAmount;
    }
    public string getItemName(){
        return itemName;
    }
    
    public void OnMouseDown()
    {
        StartCoroutine(walkingToItem()); 
    }

    void OnMouseOver()
    {
        GetComponentInChildren<Outline>().eraseRenderer = false;
    }

    void OnMouseExit()
    {
        GetComponentInChildren<Outline>().eraseRenderer = true;
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
        playerBehavior.setHitGroundPostion(this.transform.position);
        while(runLoop){
            if(playerInBounds && playerBehavior.getIsMovingToDestination()){
                playerBehavior.stopPlayer();
                pickUpItem();
                runLoop = false;
            }
            yield return null;
        }
    }
}


