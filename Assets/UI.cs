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
            gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(new Dictionary<string, int>{{"Wood planks", 3}, {"Stone", 3}});
        }
        if(Input.GetKeyDown("5")){
            gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(new Dictionary<string, int>{
                {"Wood planks", 35}, 
                {"Stone", 35}, 
                {"Wood", 10}, 
                {"Bucket", 1},
                {"Iron ore", 10},
                {"Coal ore", 10},
                {"Gold ore", 10},
                {"Silver ore", 10},
                {"Copper ore", 10},
                {"Tin ore", 10}
                });
        }
        if(Input.GetKeyDown("6")){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
                gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>().addItemToInventory(new Dictionary<string, int>{
                    {"Wood planks", 35}, 
                    {"Stone", 35}, 
                    {"Wood", 10}, 
                    {"Bucket", 1},
                    {"Iron ore", 10},
                    {"Coal ore", 10},
                    {"Gold ore", 10},
                    {"Silver ore", 10},
                    {"Copper ore", 10},
                    {"Tin ore", 10}
                    });
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
            background.Find("Enter").gameObject.SetActive(false);                       // ENTER
            background.Find("Exit").gameObject.SetActive(false);                        // EXIT
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
                    if(gameManager.getPlayerBehavior().getPlayerEnteredBuilding()){
                        background.Find("Enter").gameObject.SetActive(false);           // ENTER
                        background.Find("Exit").gameObject.SetActive(true);             // EXIT
                    } else {
                        background.Find("Enter").gameObject.SetActive(true);            // ENTER
                        background.Find("Exit").gameObject.SetActive(false);            // EXIT
                    }
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                }

                if(buildingAttributes.getBuildingTag().Equals("Residential")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT

                    if(gameManager.getPlayerBehavior().getPlayerEnteredBuilding()){
                        background.Find("Enter").gameObject.SetActive(false);           // ENTER
                        background.Find("Exit").gameObject.SetActive(true);             // EXIT
                    } else {
                        background.Find("Enter").gameObject.SetActive(true);            // ENTER
                        background.Find("Exit").gameObject.SetActive(false);            // EXIT
                    }

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
    //---------------//
    // BUILDING MENU //
    //---------------//
    public void closeBuildingUI(){
        buildingUIOpen = false;
        buildingMenu.transform.Find("Background").gameObject.SetActive(false);
    }
    public void visitBuilding(){
        closeBuildingUI();
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        gameManager.getPlayerBehavior().moveToPosition(buildingSelected.GetComponent<Collider>().bounds.center);
    }

    public void enterBuilding(){
        closeBuildingUI();
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        BuildingAttributes buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();

        if(buildingAttributes.getPlayerInBoundsBuilding()){
            gameManager.getPlayerBehavior().setPlayerEnteredBuilding(true);
            gameManager.getPlayerBehavior().hideBody(true);
            gameManager.getPlayerBehavior().setMovementDisabled(true);
        } else {
            gameManager.getMessageLogText().addMessageToLog("You need to be at the entrance in order to enter the building");
        }
    }

    public void exitBuilding(){
        closeBuildingUI();
        gameManager.getPlayerBehavior().setPlayerEnteredBuilding(false);
        gameManager.getPlayerBehavior().hideBody(false);
        gameManager.getPlayerBehavior().setMovementDisabled(false);
    }

    //------------------//
    // BUILDING OPEN UI //
    //------------------//
    public void buildingOpen(){
        closeBuildingUI();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("BuildingMenuUI").transform.Find("Background").gameObject.SetActive(false);
    }
    public void buildingOpenClose(){
        resetSelectedItem();
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

        BuildingAttributes building = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats/InfoLeft/InfoLeftText").GetComponent<Text>().text =
        "Name:\n" + building.getBuildingName() + "\n\nDescription:\n" + building.getBuildingDescription() + "\n\nStorage Capacity:\n" + building.getStorageCapacity() +
        "\n\nValue:\n" + building.getBuildingValue() + "\n\nRented:\n" + building.getIsRented();

        InventoryCatalog inventoryCatalog = gameManager.getInventoryCatalog();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats/InfoRight/Viewport/Content/Text").GetComponent<Text>().text =
        "Building upkeep:\n" + inventoryCatalog.getListOfItemsToString(building.getBuildingUpKeep()) + "\nItems Produced Here:\n" + inventoryCatalog.getListOfItemsToString(building.getItemsProducedInBuilding()) +
        "\nItems needed for production:\n" + inventoryCatalog.getListOfItemsToString(building.getItemsNeededForBuildingProduction());
    }
    public void buildingDisassemble(){

    }

    //---------------//
    // PRODUCTION UI //
    //---------------//
    public void selectItemButton(string item){
        gameManager.getItemCatalog().setSelectedItem(gameManager.getItemCatalog().getItemByName(item));
        Item selectedItem = gameManager.getItemCatalog().getSelectedItem();
        GameObject rightPanel = GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production/RightPanel").gameObject;
        rightPanel.transform.Find("Selected Item Field").GetComponent<Text>().text = item;
        rightPanel.transform.Find("Items Needed Field").GetComponent<Text>().text = gameManager.getInventoryCatalog().getListOfItemsToString(selectedItem.getCostToCraftItem());
    }

    public void resetSelectedItem(){
        GameObject rightPanel = GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production/RightPanel").gameObject;
        rightPanel.transform.Find("Selected Item Field").GetComponent<Text>().text = "";
        rightPanel.transform.Find("Items Needed Field").GetComponent<Text>().text = "";
        gameManager.getItemCatalog().setSelectedItem(null);
    }

    public void produceItemButton(){
        StartCoroutine(produceItem());
    }

    public IEnumerator produceItem(){
        Item selectedItem = gameManager.getItemCatalog().getSelectedItem();
        Inventory buildingInventory = gameManager.getBuildingCatalog().getBuildingLastClickedInventory();
        BuildingAttributes buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>(); 
        BuildingProgressBar progressbar = GameObject.FindGameObjectWithTag("ProductionProgressBar").GetComponent<BuildingProgressBar>();

        float progressSpeed = 100;

        if(selectedItem != null){
            if(!buildingAttributes.getBuildingIsProducing()){
                Debug.Log(gameManager.getInventoryCatalog().getListOfItemsToString(selectedItem.getCostToCraftItem()));

                if(buildingInventory.checkIfListOfItemsAreInInventory(selectedItem.getCostToCraftItem())){
                    buildingAttributes.setBuildingIsProducing(true);
                    buildingAttributes.setItemCurrentlyProduced(selectedItem);

                    while(buildingInventory.checkIfListOfItemsAreInInventory(selectedItem.getCostToCraftItem())){

                        while(buildingAttributes.getProductionProgress() <= 360){
                            buildingAttributes.setIncreaseProductionProgress(Time.deltaTime * progressSpeed);
                            //progressbar.updateProgressBar(buildingAttributes.getProductionProgress());

                            if(buildingAttributes.getProductionProgress() >= 360){
                                buildingInventory.addItemToInventory(new Dictionary<string, int>{{selectedItem.getName(), 1}});
                                buildingInventory.removeItemFromInventory(selectedItem.getCostToCraftItem());
                                buildingAttributes.setResetProductionProgress();
                                break;
                            }
                            yield return null;
                        }
                    }
                    buildingAttributes.setBuildingIsProducing(false);
                    buildingAttributes.setItemCurrentlyProduced(null);

                } else {
                    gameManager.getMessageLogText().addMessageToLog("You dont have enough items in the building inventory to produce that item");
                }
            }
        } else {
            gameManager.getMessageLogText().addMessageToLog("You need to select an item to produce");
        }
        
        yield return null;
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
