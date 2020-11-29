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

        // TRANSFER FROM MAIN INVENTORY
        if(transform.parent.parent.name == "Main Inventory"){

            // TO CITIZEN INVENTORY
            if(gameManager.GetUI().getCitizenInventoryOpen()){
                gameManager.getInventoryCatalog().getMainInventory().clickInventoryItem(inventorySlotID, gameManager.getCitizenCatalog().getSelectedCitizen().getInventory());
            }

            // TO BUILDING INVENTORY
            if(gameManager.GetUI().getBuildingInventoryOpen()){
                gameManager.getInventoryCatalog().getMainInventory().clickInventoryItem(inventorySlotID, gameManager.getBuildingCatalog().getBuildingLastClickedInventory());
            }

            // TO UNFINISHED BUILDING INVENTORY
            if(gameManager.GetUI().getUnfinishedBuildingOpen()){
                gameManager.getInventoryCatalog().getMainInventory().clickInventoryItem(inventorySlotID, gameManager.getBuildingCatalog().getUnfinishedBuildingSelected().getInventory());
            }

            // TO TOOLBAR
            if(!gameManager.GetUI().getUnfinishedBuildingOpen() && !gameManager.GetUI().getBuildingInventoryOpen() && !gameManager.GetUI().getCitizenInventoryOpen()){
                gameManager.getInventoryCatalog().getMainInventory().clickInventoryItem(inventorySlotID, null);
            }
        }

        // TRANSFER FROM CITIZEN INVENTORY
        if(transform.parent.parent.name == "Citizen Inventory"){
            gameManager.getCitizenCatalog().getSelectedCitizen().getInventory().clickInventoryItem(inventorySlotID, gameManager.getInventoryCatalog().getMainInventory());
        }

        // TRANSFER FROM BUILDING INVENTORY
        if(transform.parent.parent.name == "Building Inventory"){
            gameManager.getBuildingCatalog().getBuildingLastClickedInventory().clickInventoryItem(inventorySlotID, gameManager.getInventoryCatalog().getMainInventory());
        }

        // TRANSFER FROM UNFINISHED BUILDING INVENTORY
        if(transform.parent.parent.name == "Unfinished Building Inventory"){
            Debug.Log("unifnished building");
            gameManager.getBuildingCatalog().getUnfinishedBuildingSelected().getInventory().clickInventoryItem(inventorySlotID, gameManager.getInventoryCatalog().getMainInventory());
        }
    }
}
