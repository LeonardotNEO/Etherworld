using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> mainInventory = new List<Item>();
    

    public Inventory(){
        // Adding new items to the inventory at start, when an inventory is constructed
        addItemToInventory(new Item("WoodPile", 20));
        addItemToInventory(new Item("StonePile", 10));
        addItemToInventory(new Item("IronPile", 10));
        addItemToInventory(new Item("CoalPile", 2000));
    }

    void Awake()
    {
        
    }

    // Adds items to inventory. If item already exists, just increment itemValue in the array with itemAmount specified for item object
    public void addItemToInventory(Item item){
        var itemInInventory = mainInventory.Find(x => x.getName() == item.getName());
        if(itemInInventory != null){
            itemInInventory.setIncreaseAmount(item.getItemAmount());
        } else {
            mainInventory.Add(item); 
        }
    }

    public void removeItemFromInventory(Item item){
        mainInventory.Remove(item);
    }

    public List<Item> getMainInventory(){
        return mainInventory;
    }

    public string getNameOfResourcesInInventory(){
        string inventoryName = "Resource: \n";
        foreach(var item in mainInventory){
            inventoryName += item.getName() + "\n";
        }
        return inventoryName;
    }

    public string getAmountOfResourcesInInventory(){
        string inventoryAmount = "Amount: \n";
        foreach(var item in mainInventory){
            inventoryAmount += item.getItemAmount() + "\n";
        }
        return inventoryAmount;
    }

}
