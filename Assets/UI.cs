using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // INVENTORY
    public bool inventoryOpen = false;
    public bool craftingOpen = false;






    void Start()
    {
        closeCrafting();
        closeInventory();
    }

    
    void Update()
    {

        if(Input.GetKeyDown("i") && inventoryOpen == false){
            openInventory();
            closeCrafting();
        } else if(Input.GetKeyDown("i") && inventoryOpen == true){
            closeInventory();
        }

        if(Input.GetKeyDown("tab") && craftingOpen == false){
            openCrafting();
            closeInventory();
        } else if(Input.GetKeyDown("tab") && craftingOpen == true) {
            closeCrafting();
        }
    }

    public void openInventory(){
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>().enabled = true;
        inventoryOpen = true;
    }
    public void closeInventory(){
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>().enabled = false;
        inventoryOpen = false;
    }

    public void openCrafting(){
        GameObject.FindGameObjectWithTag("Crafting").GetComponent<Canvas>().enabled = true;
        craftingOpen = true;
    }
    public void closeCrafting(){
        GameObject.FindGameObjectWithTag("Crafting").GetComponent<Canvas>().enabled = false;
        craftingOpen = false;
    }
}
