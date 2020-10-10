using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // INVENTORY
    public bool inventoryOpen = false;






    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown("i") && inventoryOpen == false){
            openInventory();
        } else if(Input.GetKeyDown("i") && inventoryOpen == true){
            closeInventory();
        }
    }

    public void openInventory(){
        GameObject.Find("/UI Panel/Inventory").GetComponent<Canvas>().enabled = true;
        inventoryOpen = true;
    }

    public void closeInventory(){
        GameObject.Find("/UI Panel/Inventory").GetComponent<Canvas>().enabled = false;
        inventoryOpen = false;
    }
}
