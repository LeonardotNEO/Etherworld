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

        if(transform.tag == "MainInvSlot"){
            transform.GetComponent<Button>().onClick.AddListener(delegate {gameManager.getInventoryCatalog().getMainInventory().clickInventoryItem(inventorySlotID, gameManager.getBuildingCatalog().getBuildingLastClickedInventory()); });
        }
        if(transform.tag == "BuildingInvSlot"){
            transform.GetComponent<Button>().onClick.AddListener(delegate {gameManager.getBuildingCatalog().getBuildingLastClickedInventory().clickInventoryItem(inventorySlotID, gameManager.getInventoryCatalog().getMainInventory()); });
        }
    }

    public void setInventorySlotID(int number){
        inventorySlotID = number;
    }
}
