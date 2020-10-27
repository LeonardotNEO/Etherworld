using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    // GAMEMANAGER
    GameManager gameManager;

    // INVENTORY
    private bool inventoryOpen;
    private bool craftingOpen;
    //private bool buildingStatsOpen;
    private bool messageLogOpen;

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

        if(messageLogOpen){
            GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponent<Canvas>().enabled = true;
        } else {
            GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponent<Canvas>().enabled = false;
        }

        // INPUTS //
        if(Input.GetKeyDown("1")){
            Debug.Log(gameManager.getBuildingCatalog().getBuildingsCatalogToString());
        }
        if(Input.GetKeyDown("2")){
            Debug.Log(gameManager.getInventoryCatalog().inventoryCatalogToString());
        }
        if(Input.GetKeyDown("3")){
            Debug.Log(gameManager.getItemCatalog().itemCatalogToString());
        }
        if(Input.GetKeyDown("4")){
            gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(new Dictionary<string, int>{{"Wood", 3}, {"Stone", 3}});
        }
        if(Input.GetKeyDown("5")){
            gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(new Dictionary<string, int>{{"Wood", 35}, {"Stone", 35}, {"Bucket", 1}});
        }
        if(Input.GetKeyDown("i") && getInventoryOpen() == false){
            setInventory(true);
            setCrafting(false);
            setMessageLog(false);
        } else if(Input.GetKeyDown("i") && getInventoryOpen() == true){
            setInventory(false);
        }
        if(Input.GetKeyDown("tab") && getCraftingOpen() == false){
            setCrafting(true);
            setInventory(false);
            setMessageLog(false);
        } else if(Input.GetKeyDown("tab") && getCraftingOpen() == true) {
            setCrafting(false);
        }
        if(gameManager.getCraftingSystem().getIsCrafting()){
            if(Input.GetKey("q")){
                gameManager.getCraftingSystem().rotateBuildingLeft();
            }
            if(Input.GetKey("e")){
                gameManager.getCraftingSystem().rotateBuildingRight();
            }
        }
        if(Input.GetKeyDown("m") && getMessageLogOpen() == false){
            setMessageLog(true);
            setInventory(false);
            setCrafting(false);
            
        } else if(Input.GetKeyDown("m") && getInventoryOpen() == true){
            setMessageLog(false);
        }
        if(Input.GetKeyDown("g")){
            //inventorySystem.spawnNPC(); Doesnt exist anymore, add new if u want
        }
        if(Input.GetKeyDown("h")){
            RaycastHit mouseButtonPressed;
            Ray movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movementRay, out mouseButtonPressed, Mathf.Infinity, LayerMask.GetMask("Ground"))){
                float y = 0;
                for(int i = 0; i < 10; i++){
                    Instantiate(gameManager.getItemCatalog().getItemByIndex(0).getItemPrefab(), new Vector3(mouseButtonPressed.point.x, mouseButtonPressed.point.y, mouseButtonPressed.point.z + y), mouseButtonPressed.transform.rotation);
                    y += 1.5f;
                }
            }
        }

    }
    public bool getInventoryOpen(){
        return inventoryOpen;
    }
    public bool getCraftingOpen(){
        return craftingOpen;
    }
    public bool getMessageLogOpen(){
        return messageLogOpen;
    }

    public void setInventory(bool openClosed){
        inventoryOpen = openClosed;
    }
    public void setCrafting(bool openClosed){
        craftingOpen = openClosed;
    }
    public void setMessageLog(bool openClosed){
        messageLogOpen = openClosed;
    }

    public bool getIsMouseOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void visitBuildingOnClick(){
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        gameManager.getPlayerBehavior().moveToPosition(buildingSelected.GetComponent<Collider>().bounds.center);
    }

    public void fillBucketOnClick(){
        StartCoroutine(collectWaterFromWell());
    }
    public IEnumerator collectWaterFromWell(){
        bool runLoop = true;
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        visitBuildingOnClick();
        while(runLoop){
            if(buildingSelected.GetComponent<BuildingAttributes>().getPlayerInBoundsBuilding()){
                if(gameManager.getInventoryCatalog().getMainInventory().checkIfListOfItemsAreInInventory(new Dictionary<string, int>{{"Bucket", 1}})){
                    gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(new Dictionary<string, int>{{"Bucket", 1}});
                    gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(new Dictionary<string, int>{{"Bucket Of Water", 1}});
                    gameManager.getMessageLogText().addMessageToLog("You filled a bucket with water");
                } else {
                    gameManager.getMessageLogText().addMessageToLog("You need a bucket to fetch water");
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

    /*public void buildingStatsOnClick(){
        //buildingStatsOpen = true;
        GameObject.FindGameObjectWithTag("BuildingMenuUI").GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("BuildingStatsUI").GetComponent<Canvas>().enabled = true;
        BuildingAttributes buildingSelectedStats = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();

        // HEADER //
        GameObject.FindGameObjectWithTag("BuildingStatsUI").transform.Find("Background").Find("Headline").GetComponentInChildren<Text>().text = buildingSelectedStats.getBuildingName();

        // LEFT SIDE //
        GameObject.FindGameObjectWithTag("BuildingStatsUI").transform.Find("Background").Find("Stats").Find("InfoLeft").GetComponentInChildren<Text>().text = 
        "Name: " + buildingSelectedStats.getBuildingName() + "\nType:\n" + buildingSelectedStats.getBuildingTag() + "\nDescription:\n" + buildingSelectedStats.getBuildingDescription() + 
        "\nOwned by player:\n" + buildingSelectedStats.getIsOwnedByPlayer() + "\nStorage Capacity:\n" + buildingSelectedStats.getStorageCapacity();

        // RIGHT SIDE //
        GameObject.FindGameObjectWithTag("BuildingStatsUI").transform.Find("Background").Find("Stats").Find("InfoRight").GetComponentInChildren<Text>().text =
        "Building Upkeep:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getBuildingUpKeep()) + 
        "\nBuilding Inventory:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getItemsStoredInBuilding()) +
        "\nItems Produced:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getItemsProducedInBuilding()) +
        "\nItems Needed For Production:\n" + gameManager.getInventoryCatalog().getListOfItemsToString(buildingSelectedStats.getItemsNeededForBuildingProduction());
    }*/
    public void closeBuildingStatsOnCLick(){
        //buildingStatsOpen = false;
        GameObject.FindGameObjectWithTag("BuildingStatsUI").GetComponent<Canvas>().enabled = false;
    }
    public void OpenGateOnClick(){
        gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Gate>().setGateOpen(true);
        // PLAY ANIMATION
    }
    public void closeGateOnClick(){
        gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Gate>().setGateOpen(false);
        // PLAY ANIMATION
    }

    public void buildingOpenOnClick(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("BuildingMenuUI").GetComponent<Canvas>().enabled = false;
    }
    public void buildingOpenCloseOnClick(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(false);
    }
}
