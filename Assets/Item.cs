using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    string itemName;
    int itemID;
    int itemAmount;

    public Item(string itemName, int itemAmount, int itemID){
        this.itemName = itemName;
        this.itemID = itemID;
        this.itemAmount = itemAmount;
    }

    public void setIncreaseAmount(int amount){
        this.itemAmount += amount;
    }

    public void setDecreaseAmount(int amount){
        this.itemAmount -= amount;
    }

    public string getName(){
        return itemName;
    }

    public int getItemAmount(){
        return itemAmount;
    }
    public int getItemID(){
        return itemID;
    }
}
