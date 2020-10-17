using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // INVENTORY
    private bool inventoryOpen;
    private bool craftingOpen;

    void Start()
    {

    }
    void Update()
    {
        if(inventoryOpen){
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>().enabled = true;
        } else {
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>().enabled = false;
        }

        if(craftingOpen){
            GameObject.FindGameObjectWithTag("Crafting").GetComponent<Canvas>().enabled = true;
        } else {
            GameObject.FindGameObjectWithTag("Crafting").GetComponent<Canvas>().enabled = false;
        }

    }
    public bool getInventoryOpen(){
        return inventoryOpen;
    }
    public bool getCraftingOpen(){
        return craftingOpen;
    }

    public void setInventory(bool openClosed){
        inventoryOpen = openClosed;
    }
    public void setCrafting(bool openClosed){
        craftingOpen = openClosed;
    }
}
