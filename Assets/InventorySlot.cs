using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemInSlot = null;
    public int slotCapacity = 99;
    public int currentAmountInSlot = 0;
    public int itemSlotPlacement = 0;
    public int slotNumber = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmountInSlot == 0){
            GetComponent<Canvas>().enabled = false;
            itemInSlot = null;
        } else {
            GetComponent<Canvas>().enabled = true;
        }

        if(currentAmountInSlot != 0){
            transform.Find("Text").GetComponent<Text>().text = currentAmountInSlot.ToString();
        }
        if(itemInSlot != null){
            transform.Find("Text (1)").GetComponent<Text>().text = itemInSlot.ToString();
        }
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
