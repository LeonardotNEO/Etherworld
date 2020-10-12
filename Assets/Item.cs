using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemName;
    private int itemID;
    private int itemAmount;

    public Item(string itemName, int itemAmount){
        this.itemName = itemName;
        this.itemAmount = itemAmount;
        itemID++;
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

    public Item itemObject(){
        return this;
    }
}
