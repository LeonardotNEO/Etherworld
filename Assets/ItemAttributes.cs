using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    private GameManager gameManager;
    private Collider player;
    public int itemAmount;
    private string itemTag;
    private bool playerInBounds;


    void Awake()
    {
        itemTag = this.tag;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(itemTag == "Wood"){
            itemAmount = 1;
        }
        if(itemTag == "Stone"){
            itemAmount = 1;
        }
    }

    void Update()
    {
        
    }

    void Start()
    {

    }

    public int getItemAmount(){
        return itemAmount;
    }
    public string getItemTag(){
        return itemTag;
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
        if(getItemAmount() != 0){
            itemAmount = gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(new Dictionary<string, int>{{getItemTag(), getItemAmount()}});
        }
        player.GetComponent<Animator>().SetTrigger("pickingUpItem");
        if(itemAmount == 0){
            Destroy(this.gameObject);
        }
    }

    public IEnumerator walkingToItem(){
        bool runLoop = true;
        gameManager.getPlayerBehavior().moveToPosition(this.transform.position);
        while(runLoop){
            if(playerInBounds){
                gameManager.getPlayerBehavior().stopPlayer();
                gameManager.getPlayerBehavior().playerLookAt(transform.position.x, transform.position.y, transform.position.z);
                pickUpItem();
                runLoop = false;
            }
            yield return null;
        }
    }
}


