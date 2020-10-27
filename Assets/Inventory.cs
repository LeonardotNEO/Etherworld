using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    //public List<GameObject> inventory = new List<GameObject>();
    private int inventoryCapacity = 12;
    private GameManager gameManager;
    public GameObject inventorySlotPrefab;
    private GameObject inventorySlot;
    public int inventoryID;
    string itemSaved;
    
    void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // Instantiating inventory slots according to inventorycapacity
        for(int i = 0; i < inventoryCapacity; i++){
            GameObject inventorySlot = Instantiate(inventorySlotPrefab, transform);
            inventorySlot.GetComponent<InventorySlot>().setSlotNumber(i);
        }
    }

    void Start()
    {
        // Gets the inventory catalog and add this inventory to it
        gameManager.getInventoryCatalog().addInventoryToCatalog(this);
        inventoryID = gameManager.getInventoryCatalog().getAmountOfInventoriesInCatalog();
    }

    
    public Inventory getInventory(){
        return this;
    }

    public int getInventoryCapacity(){
        return inventoryCapacity;
    }

    public int addItemToInventory(Dictionary<string, int> itemsToAdd){
        foreach(var item in itemsToAdd){
            int amountLeftToAdd = item.Value;


            // This for loop is for checking if the item already exists, if it does, we want to start with that item and not inventortslot0
            bool firstiteration = true;
            InventorySlot itemExistsSlotSaved = null;

            for(int i = 0; i < transform.childCount; i++){
                InventorySlot itemExistsSlot = transform.GetChild(i).GetComponent<InventorySlot>();
                if(item.Key == itemExistsSlot.getItemInSlot()){
                    itemExistsSlotSaved = transform.GetChild(i).GetComponent<InventorySlot>();
                }
            }

            for(int i = 0; i < transform.childCount; i++){

                InventorySlot thisInventorySlot = transform.GetChild(i).GetComponent<InventorySlot>();

                // We include this so that if an item exists, we want to check that inventorySlot FIRST, then after that we check the rest.
                if(itemExistsSlotSaved != null && firstiteration){
                    thisInventorySlot = itemExistsSlotSaved;
                    firstiteration = false;
                    i--;
                }


                if(thisInventorySlot.getItemInSlot() == item.Key){

                    if(amountLeftToAdd + thisInventorySlot.getCurrentAmountInSlot() <= thisInventorySlot.getslotCapacity()){

                        int amountToAddToThisSlot = amountLeftToAdd;
                        thisInventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;
                        

                    } else {

                        int amountToAddToThisSlot = thisInventorySlot.getslotCapacity() - thisInventorySlot.getCurrentAmountInSlot();
                        thisInventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;

                        }

                } else {
                            
                    if(thisInventorySlot.getCurrentAmountInSlot() == 0){

                        if(amountLeftToAdd + thisInventorySlot.getCurrentAmountInSlot() <= thisInventorySlot.getslotCapacity()){

                            int amountToAddToThisSlot = amountLeftToAdd;
                            thisInventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                            thisInventorySlot.setItemInSlot(item.Key);
                            amountLeftToAdd -= amountToAddToThisSlot;

                        } else {

                            int amountToAddToThisSlot = thisInventorySlot.getslotCapacity() - thisInventorySlot.getCurrentAmountInSlot();
                            thisInventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                            amountLeftToAdd -= amountToAddToThisSlot;

                        }

                    }
                }                   
                if(amountLeftToAdd == 0){
                    break;
                }
            }
            if(amountLeftToAdd != 0){
                gameManager.getMessageLogText().addMessageToLog("Inventory is full! " + amountLeftToAdd + " " + item.Key + " could not be added to the inventory.");
                return amountLeftToAdd;
            }
             
        }
        return 0;
    }

    public void removeItemFromInventory(Dictionary<string, int> itemsToRemove){
        foreach(var itemRemove in itemsToRemove){
            int amountToRemove = itemRemove.Value;

            for(int i = inventoryCapacity - 1; i > -1; i--){

                InventorySlot thisInventorySlot = transform.GetChild(i).GetComponent<InventorySlot>();
                
                if(thisInventorySlot.getItemInSlot() == itemRemove.Key){
                    int amountToDecrease;
                    if(amountToRemove >= thisInventorySlot.getCurrentAmountInSlot()){
                        amountToDecrease = thisInventorySlot.getCurrentAmountInSlot();
                    } else {
                        amountToDecrease = amountToRemove;
                    }
                    thisInventorySlot.decreaseCurrentAmountInSlot(amountToDecrease);
                    amountToRemove -= amountToDecrease;
                }
                if(amountToRemove == 0){
                    break;
                }
            }
        }
    }

    public bool checkIfListOfItemsAreInInventory(Dictionary<string, int> checkList){
        string trueOrFalse = "";
        foreach(var item in checkList){
            int itemAmountFound = 0;

            for(int i = 0; i < inventoryCapacity; i++){
                InventorySlot inventorySlot = transform.GetChild(i).GetComponent<InventorySlot>();

                if(item.Key == inventorySlot.getItemInSlot()){
                    itemAmountFound += inventorySlot.getCurrentAmountInSlot();
                }
            }
            if(itemAmountFound >= item.Value){
                trueOrFalse += "true";
            } else {
                trueOrFalse += "false";
            }
        }
        if(trueOrFalse.Contains("false")){
            return false;
        } else {
            return true;
        }
    }
}
