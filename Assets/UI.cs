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
        gameManager.getPlayerBehavior().moveToPosition(buildingSelected.GetComponent<Collider>().bounds.center);
    }

    public void fillBucketOnClick(){
        StartCoroutine(collectWaterFromWell());
    }
    public IEnumerator collectWaterFromWell(){
        bool runLoop = true;
        GameObject buildingSelected = gameManager.getBuildingLastClicked();
        visitBuildingOnClick();
        while(runLoop){
            if(buildingSelected.GetComponent<BuildingAttributes>().getPlayerInBoundsBuilding()){
                if(gameManager.getInventoryCatalog().getMainInventory().checkIfListOfItemsAreInInventory(new Dictionary<string, int>{{"Bucket", 1}})){
                    gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(new Dictionary<string, int>{{"Bucket Of Water", 1}});
                    gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(new Dictionary<string, int>{{"Bucket", 1}});
                    gameManager.getMessageLogBar().addMessageToLog("You filled a bucket with water");
                } else {
                    gameManager.getMessageLogBar().addMessageToLog("You need a bucket to fetch water");
                    break;
                }
                break;
            }
            yield return null;
        }
        yield return null;
    }

    public void openBuildingInventoryOnClick(){

    }


    public void buildingStatsOnClick(){
        GameObject.FindGameObjectWithTag("BuildingMenuUI").GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("BuildingStatsUI").GetComponent<Canvas>().enabled = true;
        BuildingAttributes buildingSelectedStats = gameManager.getBuildingLastClicked().GetComponent<BuildingAttributes>();

        // HEADER //
        GameObject.FindGameObjectWithTag("BuildingStatsUI").transform.Find("Background").Find("Headline").GetComponentInChildren<Text>().text = buildingSelectedStats.getBuildingName();

        // LEFT SIDE //
        GameObject.FindGameObjectWithTag("BuildingStatsUI").transform.Find("Background").Find("InfoLeft").GetComponentInChildren<Text>().text = 
        "Name: " + buildingSelectedStats.getBuildingName() + "\nType:\n" + buildingSelectedStats.getBuildingTag() + "\nDescription:\n" + buildingSelectedStats.getBuildingDescription() + 
        "\nOwned by player:\n" + buildingSelectedStats.getIsOwnedByPlayer() + "\nStorage Capacity:\n" + buildingSelectedStats.getStorageCapacity();

        // RIGHT SIDE //
        GameObject.FindGameObjectWithTag("BuildingStatsUI").transform.Find("Background").Find("InfoRight").GetComponentInChildren<Text>().text =
        "Building Upkeep:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getBuildingUpKeep()) + 
        "\nBuilding Inventory:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getItemsStoredInBuilding()) +
        "\nItems Produced:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getItemsProducedInBuilding()) +
        "\nItems Needed For Production:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getItemsNeededForBuildingProduction());
    }
    public void closeBuildingStatsOnCLick(){
        GameObject.FindGameObjectWithTag("BuildingStatsUI").GetComponent<Canvas>().enabled = false;
    }
}
