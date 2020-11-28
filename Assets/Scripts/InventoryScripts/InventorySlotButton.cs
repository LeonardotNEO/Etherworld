using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotButton : MonoBehaviour
{
    public int inventorySlotID;
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void setInventorySlotID(int number){
        inventorySlotID = number;
    }

    public void buttonClick(){
        if(transform.tag == "MainInvSlot"){
            gameManager.getInventoryCatalog().getMainInventory().clickInventoryItem(inventorySlotID, gameManager.getBuildingCatalog().getBuildingLastClickedInventory());
            //Debug.Log("1");
            //Debug.Log(inventorySlotID);
        }
        if(transform.tag == "BuildingInvSlot"){
            gameManager.getBuildingCatalog().getBuildingLastClickedInventory().clickInventoryItem(inventorySlotID, gameManager.getInventoryCatalog().getMainInventory());
            //Debug.Log("2");
            //Debug.Log(inventorySlotID);

        
        }
    }
}
