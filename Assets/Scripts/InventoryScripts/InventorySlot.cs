using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot
{
    public string itemInSlot = null;
    public int slotCapacity = 99;
    public int currentAmountInSlot = 0;
    public int itemSlotPlacement = 0;
    public int slotNumber = 0;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public int getslotCapacity(){
        return slotCapacity;
    }

    public int getCurrentAmountInSlot(){
        return currentAmountInSlot;
    }

    public void updateCurrentAmountInSlot(int amount){
        currentAmountInSlot = amount;
    }
    public void increaseCurrentAmountInSlot(int amount){
        currentAmountInSlot += amount;
    }
    public void decreaseCurrentAmountInSlot(int amount){
        currentAmountInSlot -= amount;
    }
    public void setItemInSlot(string nameOfItem){
        itemInSlot = nameOfItem;
    }
    public string getItemInSlot(){
        return itemInSlot;
    }
    public void setSlotNumber(int num){
        slotNumber = num;
    }
    public int getSlotNumber(){
        return slotNumber;
    }
}
