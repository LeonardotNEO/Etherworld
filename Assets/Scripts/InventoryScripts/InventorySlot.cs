using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot
{
    public string itemInSlot = null;
    public int slotCapacity = 99;
    public int currentAmountInSlot = 0;
    public int itemSlotPlacement = 0;
    public int slotNumber = 0;
    public List<string> type = null;

    public InventorySlot(int slotCapacity, List<string> type){
        this.slotCapacity = slotCapacity;
        this.type = type;
    }


    public int getslotCapacity(){
        return slotCapacity;
    }

    public void setSlotCapacity(int val){
        slotCapacity = val;
    }

    public int getCurrentAmountInSlot(){
        return currentAmountInSlot;
    }

    public void updateCurrentAmountInSlot(int amount){
        currentAmountInSlot = amount;
        if(currentAmountInSlot == 0){
            itemInSlot = null;
        }
    }
    public void increaseCurrentAmountInSlot(int amount){
        currentAmountInSlot += amount;
        if(currentAmountInSlot == 0){
            itemInSlot = null;
        }
    }
    public void decreaseCurrentAmountInSlot(int amount){
        currentAmountInSlot -= amount;
        if(currentAmountInSlot == 0){
            itemInSlot = null;
        }
    }

    public void addInventorySlotToThis(InventorySlot inventorySlot){
        if(inventorySlot.getItemInSlot().Equals(this.getItemInSlot())){
            if(inventorySlot.getCurrentAmountInSlot() + this.getCurrentAmountInSlot() <= getslotCapacity()){
                this.updateCurrentAmountInSlot(inventorySlot.getCurrentAmountInSlot() + this.getCurrentAmountInSlot());
                inventorySlot.updateCurrentAmountInSlot(0);
            } else {
                this.increaseCurrentAmountInSlot(getslotCapacity() - getCurrentAmountInSlot());
                inventorySlot.decreaseCurrentAmountInSlot(getslotCapacity() - getCurrentAmountInSlot());
            }
        }

        if(this.itemInSlot == null){
            if(inventorySlot.getCurrentAmountInSlot() + this.getCurrentAmountInSlot() <= getslotCapacity()){
                this.setItemInSlot(inventorySlot.getItemInSlot());
                this.updateCurrentAmountInSlot(inventorySlot.getCurrentAmountInSlot() + this.getCurrentAmountInSlot());
                inventorySlot.updateCurrentAmountInSlot(0);
            } else {
                int amountToAdd = getslotCapacity() - getCurrentAmountInSlot();
                this.setItemInSlot(inventorySlot.getItemInSlot());
                this.increaseCurrentAmountInSlot(amountToAdd);
                inventorySlot.decreaseCurrentAmountInSlot(amountToAdd);
            }
        }
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
    public void setInventorySlotType(List<string> type){
        this.type = type;
    }
    public List<string> getInventorySlotType(){
        return type;
    }
}
