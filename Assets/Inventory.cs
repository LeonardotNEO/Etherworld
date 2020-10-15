using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    private InventoryCatalog inventories;
    public int inventoryID = 0;
    public Inventory(){
        // Adding new items to the inventory at start, when an inventory is constructed, this will be an empty inventory

        addItemToInventory("StonePile", 200);
        addItemToInventory("WoodPile", 2000);

    }

    void Awake()
    {
        // Gets the inventory catalog and ads this inventory to it
        inventories = GameObject.FindWithTag("Inventory").GetComponent<InventoryCatalog>();
        inventories.addInventoryToCatalog(this);
        inventoryID = inventories.getAmountOfInventoriesInCatalog();
    }

    // Adds items to inventory. If item already exists, just increment itemValue in the array with itemAmount specified for item object
    public void addItemToInventory(string name, int amount){
        if(!inventory.ContainsKey(name)){
            inventory.Add(name, amount);
        } else {
            inventory[name] += amount;
        }
    }
    public void removeItemFromInventory(string name, int amount){
        inventory[name] -= amount;
    }
    /*public void removeItemFromInventory(Dictionary<string, int> itemsToRemove){
        foreach(var name in itemsToRemove){
            if(name.Key = inventory.ke)
            inventory[items] -= name.Value
        }
    }*/

    public Dictionary<string, int> getMainInventory(){
        return inventory;
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
    public int getInventorySize(){
        return inventory.Count;
    }
    public Dictionary<string, int> getInventory(){
        return inventory;
    }
}
