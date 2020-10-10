using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    List<Item> mainInventory = new List<Item>();

    public Inventory(){
        
    }

    public void addItemToInventory(Item item){
        mainInventory.Add(item);
    }

    public void SetNewItem(){
        
    }

    public List<Item> getMainInventory(){
        return mainInventory;
    }

    public string getInventoryName(){
        string inventoryName = "Resource: \n";
        foreach(var item in mainInventory){
            inventoryName += item.getName() + "\n";
        }
        return inventoryName;
    }

    public string getInventoryAmount(){
        string inventoryAmount = "Amount: \n";
        foreach(var item in mainInventory){
            inventoryAmount += item.getItemAmount() + "\n";
        }
        return inventoryAmount;
    }

}
