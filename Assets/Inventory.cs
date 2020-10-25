using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    private InventoryCatalog inventoryCatalog;
    public int inventoryID = 0;
    string itemSaved;
    
    void Awake()
    {
        // Gets the inventory catalog and add this inventory to it
        inventoryCatalog = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().getInventoryCatalog();
        inventoryCatalog.addInventoryToCatalog(this);
        inventoryID = inventoryCatalog.getAmountOfInventoriesInCatalog();
    }



    public void addItemToInventory(Dictionary<string, int> itemsToAdd){
        foreach(var itemAdd in itemsToAdd){
            bool sameKey = false;

            foreach(var itemInv in inventory){
                if(itemInv.Key == itemAdd.Key){
                    sameKey = true;
                    itemSaved = itemInv.Key;
                }
            }
            if(sameKey){
                inventory[itemSaved] += itemAdd.Value;
            } else {
                inventory.Add(itemAdd.Key, itemAdd.Value);
            }
        }
    }
    public void removeItemFromInventory(Dictionary<string, int> itemsToRemove){
        foreach(var itemRemove in itemsToRemove){
            bool sameKey = false;

            foreach(var itemInv in inventory){
                if(itemInv.Key == itemRemove.Key){
                    sameKey = true;
                    itemSaved = itemInv.Key;
                }
            }
            if(sameKey){
                inventory[itemSaved] -= itemRemove.Value;
                if(inventory[itemSaved] == 0){
                    inventory.Remove(itemRemove.Key);
                }
            }
        }
    }

    public bool checkIfListOfItemsAreInInventory(Dictionary<string, int> list){
        string enoughInInventory = "";
        bool lastValue = false;

        foreach(var itemC in list){
            foreach(var itemI in inventory){
                if(itemC.Key == itemI.Key){
                    if(itemC.Value <= itemI.Value){
                        enoughInInventory += "true";
                        lastValue = true;
                        break;
                    }
                }
            }
            if(!lastValue){
                enoughInInventory += "false";
            }
            lastValue = false;
        }
        if(!enoughInInventory.Contains("false")){
            return true;
        } else {
            return false;
        }
    }

    public string getNameOfResourcesInInventoryToString(){
        string inventoryName = "Resource: \n";
        foreach(var item in inventory){
            inventoryName += item.Key + "\n";
        }
        return inventoryName;
    }
    public string getAmountOfResourcesInInventoryToString(){
        string inventoryAmount = "Amount: \n";
        foreach(var item in inventory){
            inventoryAmount += item.Value + "\n";
        }
        return inventoryAmount;
    }

    public string getListOfItemsToString(Dictionary<string, int> list){
        string listOfItems = "";
        foreach(var item in list){
            listOfItems += "Item: " + item.Key + " Amount: " + item.Value + "\n";
        }
        return listOfItems;
    }
    public int getInventorySize(){
        return inventory.Count;
    }
    public Dictionary<string, int> getInventory(){
        return inventory;
    }
}
