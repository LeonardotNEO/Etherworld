using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    // GAMEMANAGER
    GameManager gameManager;
    GameObject buildingMenu;

    // INVENTORY
    public bool inventoryOpen;
    public bool craftingOpen;
    public bool messageLogOpen;
    public bool buildingUIOpen;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buildingMenu = GameObject.FindGameObjectWithTag("BuildingMenuUI");
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
        if(Input.GetKeyDown("6")){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
                gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>().addItemToInventory(new Dictionary<string, int>{{"Wood", 35}, {"Stone", 35}, {"Bucket", 1}});
            }
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


    // WATERWELL //
    public void fillBucketOnClick(){
        StartCoroutine(collectWaterFromWell());
    }
    public IEnumerator collectWaterFromWell(){
        bool runLoop = true;
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        visitBuilding();
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

    // BUILDING MENU UI //

    public void openBuildingUI(GameObject thisBuilding){
        buildingUIOpen = true;
        gameManager.getBuildingCatalog().setBuildingLastClicked(thisBuilding);
        BuildingAttributes buildingAttributes  = thisBuilding.GetComponent<BuildingAttributes>();
        Transform background = buildingMenu.transform.Find("Background");

        
        background.Find("Headline").GetComponent<Text>().text = buildingAttributes.getBuildingName();

        if(!gameManager.GetUI().getIsMouseOverUI() && !gameManager.getCraftingSystem().getIsCrafting()){

            // BUTTONS TO SHOW BY DEFAULT
            background.Find("Visit").gameObject.SetActive(false);                       // VISIT
            background.Find("Open").gameObject.SetActive(false);                        // OPEN
            background.Find("Fill Bucket").gameObject.SetActive(false);                 // FILL BUCKET
            background.Find("Open Gate").gameObject.SetActive(false);                   // OPEN GATE
            background.Find("Close Gate").gameObject.SetActive(false);                   // CLOSE GATE

            buildingMenu.transform.Find("Background").gameObject.SetActive(true);
            background.position = new Vector3(Input.mousePosition.x + buildingMenu.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.x/2, Input.mousePosition.y + buildingMenu.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.y/2, 0);

            if(buildingAttributes.getIsOwnedByPlayer() && !buildingAttributes.getIsRented()){
                if(buildingAttributes.getBuildingName().Equals("Waterwell")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                    background.Find("Fill Bucket").gameObject.SetActive(true);          // FILL BUCKET
                }
                if(buildingAttributes.getBuildingTag().Equals("Industrial")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                }
                if(buildingAttributes.getBuildingName().Equals("Small Wood House")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                }
                if(buildingAttributes.getBuildingName().Equals("Medium Wood House")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                }
                if(buildingAttributes.getBuildingName().Equals("Boarding House")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                }
                if(buildingAttributes.getBuildingName().Equals("Medium Stone Gate")){
                    if(thisBuilding.GetComponent<Gate>().getGateOpen()){
                        background.Find("Open Gate").gameObject.SetActive(false);        // OPEN GATE
                        background.Find("Close Gate").gameObject.SetActive(true);        // CLOSE GATE
                    } else {
                        background.Find("Close Gate").gameObject.SetActive(false);       // CLOSE GATE
                        background.Find("Open Gate").gameObject.SetActive(true);         // OPEN GATE
                    }
                }
            } else {
                // THIS IS BUILDING IS NOT OWNED BY PLAYER
                if(buildingAttributes.getBuildingName().Equals("Waterwell")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Fill Bucket").gameObject.SetActive(true);          // FILL BUCKET
                }
                if(buildingAttributes.getBuildingName().Equals("Boarding House")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                }
                if(buildingAttributes.getBuildingName().Equals("Small Wood House")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                }
                if(buildingAttributes.getBuildingName().Equals("Medium Wood House")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                }
                if(buildingAttributes.getBuildingName().Equals("Sawmill")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                }
                if(buildingAttributes.getBuildingName().Equals("Furnace")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                }
            }
        }
    }
    public void closeBuildingUI(){
        buildingUIOpen = false;
        buildingMenu.transform.Find("Background").gameObject.SetActive(false);
    }
    public void visitBuilding(){
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        gameManager.getPlayerBehavior().moveToPosition(buildingSelected.GetComponent<Collider>().bounds.center);
    }


    // BUILDING OPEN UI //
    public void buildingOpen(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("BuildingMenuUI").transform.Find("Background").gameObject.SetActive(false);
    }
    public void buildingOpenClose(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
    }


    public void buildingProductionOpen(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
    }
    public void buildingWorkersOpen(){

    }
    public void buildingInventoryOpen(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
    }
    public void buildingStatsOpen(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
    }
    public void buildingDisassemble(){

    }

    // GATE UI //
    public void OpenGate(){
        gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Gate>().setGateOpen(true);
        // PLAY ANIMATION
    }
    public void closeGate(){
        gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Gate>().setGateOpen(false);
        // PLAY ANIMATION
    }
}
