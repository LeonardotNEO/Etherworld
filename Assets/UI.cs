using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // GAMEMANAGER

    GameManager gameManager;

    // INVENTORY
    private bool inventoryOpen;
    private bool craftingOpen;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if(inventoryOpen){
            GameObject.FindGameObjectWithTag("InventoryMenuUI").GetComponent<Canvas>().enabled = true;
        } else {
            GameObject.FindGameObjectWithTag("InventoryMenuUI").GetComponent<Canvas>().enabled = false;
        }

        if(craftingOpen){
            GameObject.FindGameObjectWithTag("CraftingMenuUI").GetComponent<Canvas>().enabled = true;
        } else {
            GameObject.FindGameObjectWithTag("CraftingMenuUI").GetComponent<Canvas>().enabled = false;
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

    public void visitBuildingOnClick(){
        GameObject buildingSelected = gameManager.getBuildingLastClicked();
        //Debug.Log(buildingSelected.GetComponent<BuildingAttributes>().getBuildingID());
        gameManager.getPlayerBehavior().moveToPosition(buildingSelected.transform.Find("Entrance").GetComponent<Collider>().transform.position);
        
    }
}
