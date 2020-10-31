using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int inventoryCapacity;
    public string inventoryName;
    private GameManager gameManager;
    public int inventoryID;
    
    void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        inventoryName = transform.name;

        if(transform.tag == "player"){
            inventoryCapacity = 30;
        } 
    }

    void Start()
    {
        if(transform.tag == "Residential" || transform.tag == "Industrial"){
            inventoryCapacity = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getStorageCapacity();
        }
        for(int i = 0; i < inventoryCapacity; i++){
            inventorySlots.Add(new InventorySlot());
        }

        // Gets the inventory catalog and add this inventory to it
        if(transform.tag == "player"){
            gameManager.getInventoryCatalog().addInventoryToCatalogAtIndex(0, this);
            inventoryID = 0;
        } else {
            gameManager.getInventoryCatalog().addInventoryToCatalog(this);
            inventoryID = gameManager.getInventoryCatalog().getAmountOfInventoriesInCatalog();
        }   
    }

    
    public Inventory getInventory(){
        return this;
    }

    public int getInventoryCapacity(){
        return inventoryCapacity;
    }

    public string getInventoryName(){
        return inventoryName;
    }

    public int addItemToInventory(Dictionary<string, int> itemsToAdd){
        foreach(var item in itemsToAdd){
            int amountLeftToAdd = item.Value;
            List<InventorySlot> invSlotsWithItemAlready = new List<InventorySlot>();

            foreach(InventorySlot inventorySlot in inventorySlots){
                if(inventorySlot.getItemInSlot() == item.Key && inventorySlot.getCurrentAmountInSlot() < 99){
                    invSlotsWithItemAlready.Add(inventorySlot);
                }
            }
            
            foreach(InventorySlot inventorySlot in invSlotsWithItemAlready){
                    if(amountLeftToAdd + inventorySlot.getCurrentAmountInSlot() <= inventorySlot.getslotCapacity()){
                        int amountToAddToThisSlot = amountLeftToAdd;
                        inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;
                    } else {
                        int amountToAddToThisSlot = inventorySlot.getslotCapacity() - inventorySlot.getCurrentAmountInSlot();
                        inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;
                    }
                if(amountLeftToAdd == 0){
                    break;
                }
            }


            foreach(InventorySlot inventorySlot in inventorySlots){

                if(inventorySlot.getItemInSlot() == item.Key){
                    if(amountLeftToAdd + inventorySlot.getCurrentAmountInSlot() <= inventorySlot.getslotCapacity()){
                        int amountToAddToThisSlot = amountLeftToAdd;
                        inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;
                    } else {
                        int amountToAddToThisSlot = inventorySlot.getslotCapacity() - inventorySlot.getCurrentAmountInSlot();
                        inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;
                    }
                } else {
                    if(inventorySlot.getCurrentAmountInSlot() == 0){
                        if(amountLeftToAdd + inventorySlot.getCurrentAmountInSlot() <= inventorySlot.getslotCapacity()){
                            int amountToAddToThisSlot = amountLeftToAdd;
                            inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                            inventorySlot.setItemInSlot(item.Key);
                            amountLeftToAdd -= amountToAddToThisSlot;
                        } else {
                            int amountToAddToThisSlot = inventorySlot.getslotCapacity() - inventorySlot.getCurrentAmountInSlot();
                            inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
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
                Debug.Log(amountLeftToAdd);
                return amountLeftToAdd;
            }
            Debug.Log(amountLeftToAdd);
        }
        return 0;
    }

    
    public void removeItemFromInventory(Dictionary<string, int> itemsToRemove){
        foreach(var itemRemove in itemsToRemove){
            int amountToRemove = itemRemove.Value;

            for(int i = inventoryCapacity - 1; i > -1; i--){
                
                if(inventorySlots[i].getItemInSlot() == itemRemove.Key){
                    int amountToDecrease;
                    if(amountToRemove >= inventorySlots[i].getCurrentAmountInSlot()){
                        amountToDecrease = inventorySlots[i].getCurrentAmountInSlot();
                    } else {
                        amountToDecrease = amountToRemove;
                    }
                    inventorySlots[i].decreaseCurrentAmountInSlot(amountToDecrease);
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
                if(item.Key == inventorySlots[i].getItemInSlot()){
                    itemAmountFound += inventorySlots[i].getCurrentAmountInSlot();
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

    public void sendItemsFromInventory1ToIventory2(Inventory inv1, Inventory inv2, Dictionary<string,int> items){
        inv1.addItemToInventory(items);
        inv2.removeItemFromInventory(items);
    }

    public List<InventorySlot> getInventorySlots(){
        return inventorySlots;
    }

    public string getInventoryToString(){
        string inventoryString = "";
        foreach(InventorySlot inventorySlot in inventorySlots){
            inventoryString += inventorySlot.getCurrentAmountInSlot() + " " + inventorySlot.getItemInSlot();
        }
        return inventoryString;
    }
}
