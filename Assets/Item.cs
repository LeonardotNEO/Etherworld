using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int itemID;
    private string itemName;
    private int itemAmount;
    public Item(string itemName, int itemAmount){
        this.itemName = itemName;
        this.itemAmount = itemAmount;
    }

    public void setItemNameAmount(string newItem, int newAmount){
        itemName = newItem;
        itemAmount = newAmount;
    }



    public string getName(){
        return itemName;
    }

    public int getAmount(){
        return itemAmount;
    }
}
