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

    // CONFIRMATION MENU
    public string situation;

    // MENU BOOLS
    public bool inventoryOpen;
    public bool craftingOpen;
    public bool messageLogOpen;
    public bool buildingMenuOpen;
    public bool buildingOpenOpen;
    public bool buildingInventoryOpenBool;                                                                                                          

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buildingMenu = GameObject.FindGameObjectWithTag("BuildingMenuUI");
    }
    void Update()
    {
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
            Dictionary<string,int> listOfItems = new Dictionary<string, int>{
                {"Wood planks", 3}, 
                {"Stone", 3}};

            foreach(var item in listOfItems){
                gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(item.Key, item.Value);
            }
        }
        if(Input.GetKeyDown("5")){
            Dictionary<string,int> listOfItems = new Dictionary<string, int>{
            {"Wood plank", 35}, 
            {"Stone", 35}, 
            {"Wood log", 10}, 
            {"Bucket", 1},
            {"Iron ore", 10},
            {"Coal ore", 10},
            {"Gold ore", 10},
            {"Silver ore", 10},
            {"Copper ore", 10},
            {"Tin ore", 10}
            };

            foreach(var item in listOfItems){
                gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(item.Key, item.Value);
            }
        }
        if(Input.GetKeyDown("6")){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
                Dictionary<string,int> listOfItems = new Dictionary<string, int>{
                {"Wood plank", 35}, 
                {"Stone", 35}, 
                {"Wood log", 10}, 
                {"Bucket", 1},
                {"Iron ore", 10},
                {"Coal ore", 10},
                {"Gold ore", 10},
                {"Silver ore", 10},
                {"Copper ore", 10},
                {"Tin ore", 10}
                };
                
                foreach(var item in listOfItems){
                    gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>().addItemToInventory(item.Key, item.Value);
                }
            }
        }
        if(Input.GetKeyDown("x")){
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown("c")){
            Time.timeScale = 1;
        }
        if(Input.GetKeyDown("v")){
            Time.timeScale = 2f;
        }
        if(Input.GetKeyDown("b")){
            Time.timeScale = 4;
        }
        if(Input.GetKeyDown("n")){
            Time.timeScale = 8;
        }
        if(Input.GetKeyDown("i")){
            openInventory();
        }
        if(Input.GetKeyDown("m")){
            openMessageLog();
        } 
        if(Input.GetKeyDown("tab")){
            openCrafting();
        } 
        if(gameManager.getCraftingSystem().getIsCrafting()){
            if(Input.GetKey("q")){
                gameManager.getCraftingSystem().rotateBuildingLeft();
            }
            if(Input.GetKey("e")){
                gameManager.getCraftingSystem().rotateBuildingRight();
            }
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

    //---------//
    // GETTERS //
    //---------//

    public bool getInventoryOpen(){
        return inventoryOpen;
    }
    public bool getCraftingOpen(){
        return craftingOpen;
    }
    public bool getMessageLogOpen(){
        return messageLogOpen;
    }
    public bool geBuildingMenuOpen(){
        return buildingMenuOpen;
    }
    public bool getBuildingOpenOpen(){
        return buildingOpenOpen;
    }
    public bool getBuildingInventoryOpen(){
        return buildingInventoryOpenBool;
    }

    //---------//
    // SETTERS //
    //---------//
    public void setInventory(bool openClosed){
        inventoryOpen = openClosed;
    }
    public void setCrafting(bool openClosed){
        craftingOpen = openClosed;
    }
    public void setMessageLog(bool openClosed){
        messageLogOpen = openClosed;
    }
    public void setBuildingMenu(bool openClosed){
        buildingMenuOpen = openClosed;
    }
    public void setBuildingOpen(bool openClosed){
        buildingOpenOpen = openClosed;
    }
    public void setBuildingInventoryOpen(bool openClosed){
        buildingInventoryOpenBool = openClosed;
    }

    public bool getIsMouseOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }

    //-----------//
    // WATERWELL //
    //-----------//
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
                    gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory("Bucket", 1);
                    gameManager.getInventoryCatalog().getMainInventory().addItemToInventory("Bucket Of Water", 1);
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

    //---------------//
    // BUILDING MENU //
    //---------------//
    public void openBuildingUI(GameObject thisBuilding){
        buildingMenuOpen = true;
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
                //------------------//
                // BUILDINGS BY TAG //
                //------------------//
                if(buildingAttributes.getBuildingTag().Equals("Industrial")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    if(buildingAttributes.getPlayerEnteredBuilding()){
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

                    if(buildingAttributes.getPlayerEnteredBuilding()){
                        background.Find("Enter").gameObject.SetActive(false);           // ENTER
                        background.Find("Exit").gameObject.SetActive(true);             // EXIT
                    } else {
                        background.Find("Enter").gameObject.SetActive(true);            // ENTER
                        background.Find("Exit").gameObject.SetActive(false);            // EXIT
                    }

                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                }
                //-------------------//
                // BUILDINGS BY NAME //
                //-------------------//
                if(buildingAttributes.getBuildingName().Equals("Waterwell")){
                    background.Find("Visit").gameObject.SetActive(true);                // VISIT
                    background.Find("Open").gameObject.SetActive(true);                 // OPEN
                    background.Find("Fill Bucket").gameObject.SetActive(true);          // FILL BUCKET
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
        buildingMenuOpen = false;
        buildingMenu.transform.Find("Background").gameObject.SetActive(false);
    }
    public void visitBuilding(){
        closeBuildingUI();
        GameObject buildingSelected = gameManager.getBuildingCatalog().getBuildingLastClicked();
        gameManager.getPlayerBehavior().moveToPosition(buildingSelected.GetComponent<Collider>().bounds.center);
    }

    public void enterBuilding(){
        closeBuildingUI();
        gameManager.getPlayerBehavior().goToBuilding(gameManager.getBuildingCatalog().getBuildingLastClickedAttributes());
    }

    public void exitBuilding(){
        closeBuildingUI();
        buildingOpenClose();
        gameManager.getPlayerBehavior().leaveBuilding();
    }

    //------------------//
    // BUILDING OPEN UI //
    //------------------//
    public void buildingOpen(){
        buildingOpenOpen = true;
        BuildingAttributes building = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        closeBuildingUI();

        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("BuildingMenuUI").transform.Find("Background").gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Headline/HeadlineText").GetComponent<Text>().text = building.getBuildingName();
    }
    public void buildingOpenClose(){
        buildingOpenOpen = false;
        buildingInventoryOpenBool = false;
        resetSelectedItem();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
    }


    public void buildingProductionOpen(){
        //OPEN
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(true);

        //CLOSE
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(false);
        buildingInventoryOpenBool = false;
    }
    public void buildingWorkersOpen(){
        //OPEN
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(true);

        //CLOSE
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
    }
    public void buildingInventoryOpen(){
        // OPEN PLAYER INVENTORY
        gameManager.GetUI().openInventory();

        //OPEN
        buildingInventoryOpenBool = true;
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(true);

        //CLOSE
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(false);
    }

    public void buildingStatsOpen(){
        //OPEN
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(true);

        //CLOSE
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(false);
        buildingInventoryOpenBool = false;

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
        confirmationUIOpen("disassemble");
    }

    //------------------//
    // CONFIRMATIONMENU //
    //------------------//

    public void confirmationUIOpen(string currentSituation){
        GameObject.FindGameObjectWithTag("UI").transform.Find("ConfirmationMenu/Background").gameObject.SetActive(true);
        situation = currentSituation;
    }

    public void confirmationUIClose(){
        GameObject.FindGameObjectWithTag("UI").transform.Find("ConfirmationMenu/Background").gameObject.SetActive(false);
    }

    public void yesButton(){
        if(situation == "disassemble"){
            Destroy(gameManager.getBuildingCatalog().getBuildingLastClicked());
            buildingOpenClose();
            confirmationUIClose();
        }
    }
    public void noButton(){
        confirmationUIClose();
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
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production/Options/Viewport/Content").GetComponent<ItemsThatCanBeProduced>().updateItemsThatCanBeProducedList();
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

        bool inventoryIsFull = false;

        if(selectedItem != null){
            if(!buildingAttributes.getBuildingIsProducing()){

                if(buildingInventory.checkIfListOfItemsAreInInventory(selectedItem.getCostToCraftItem())){
                    buildingAttributes.setBuildingIsProducing(true);
                    buildingAttributes.setItemCurrentlyProduced(selectedItem);

                    while(buildingInventory.checkIfListOfItemsAreInInventory(selectedItem.getCostToCraftItem()) && !inventoryIsFull){
                        if(buildingAttributes.getWorkersInBuilding().Count == 0){
                            gameManager.getMessageLogText().addMessageToLog("Production could not start since theres no workers assigned in the building!");
                            break;
                        }

                        while(buildingAttributes.getProductionProgress() <= 360 && !inventoryIsFull){
                            float progressSpeed = buildingAttributes.getCitizensInsideBuilding().Count * 10;
                            buildingAttributes.setIncreaseProductionProgress(Time.deltaTime * progressSpeed);
                            //progressbar.updateProgressBar(buildingAttributes.getProductionProgress());

                            if(buildingAttributes.getProductionProgress() >= 360){
                                int itemsThatCouldNotBeAdded = buildingInventory.addItemToInventory(selectedItem.getName(), 1);

                                if(itemsThatCouldNotBeAdded == 0){
                                    foreach(var item in selectedItem.getCostToCraftItem()){
                                        buildingInventory.removeItemFromInventory(item.Key, item.Value);
                                    }
                                    buildingAttributes.setResetProductionProgress();
                                    break;
                                } else {
                                    gameManager.getMessageLogText().addMessageToLog("Buildinginventory is full. Production is cancelled");
                                    buildingAttributes.setResetProductionProgress();
                                    inventoryIsFull = true;
                                    break;
                                }
                                
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
    //------------//
    // WORKERS UI //
    //------------//
    public void MoveAvailableToWorker(){
        Citizen selectedCitizen = gameManager.getCitizenCatalog().getSelectedCitizen();
        BuildingAttributes selectedBuilding = gameManager.getBuildingCatalog().getBuildingLastClickedAttributes();
        if(selectedBuilding.getWorkersInBuilding().Count < selectedBuilding.getWorkerLimit()){
            if(!selectedBuilding.getWorkersInBuilding().Contains(selectedCitizen)){
                selectedCitizen.getTownAlliegence().removeAvailableWorkerFromTown(selectedCitizen);
                selectedBuilding.addWorkerToBuilding(selectedCitizen);
                GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Workers Here/Scroll View/Viewport/Content").GetComponent<ShowWorkersInBuilding>().updateAvailableWorkersList();
                GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Available workers/Scroll View/Viewport/Content").GetComponent<ShowAvailableWorkers>().updateAvailableWorkersList();
            }
        } else {
            gameManager.getMessageLogText().addMessageToLog("You cant add more workers since the limit is reached");
        }
    }
    public void MoveWorkerToAvailable(){
        Citizen selectedCitizen = gameManager.getCitizenCatalog().getSelectedCitizen();
        BuildingAttributes selectedBuilding = gameManager.getBuildingCatalog().getBuildingLastClickedAttributes();
        if(!selectedCitizen.getTownAlliegence().getAvailableWorkersInTown().Contains(selectedCitizen)){
            selectedCitizen.getTownAlliegence().addAvailableWorkerToTown(selectedCitizen);
            selectedBuilding.removeWorkerFromBuilding(selectedCitizen);
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Workers Here/Scroll View/Viewport/Content").GetComponent<ShowWorkersInBuilding>().updateAvailableWorkersList();
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Available workers/Scroll View/Viewport/Content").GetComponent<ShowAvailableWorkers>().updateAvailableWorkersList();
        }
    }   
    public void selectWorkerButton(int citizenID){
        gameManager.getCitizenCatalog().setSelectedCitizen(gameManager.getCitizenCatalog().getCitizenByID(citizenID));
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Workers Here/Scroll View/Viewport/Content").GetComponent<ShowWorkersInBuilding>().updateAvailableWorkersList();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Available workers/Scroll View/Viewport/Content").GetComponent<ShowAvailableWorkers>().updateAvailableWorkersList();
    }

    //---------//
    // GATE UI //
    //---------//
    public void OpenGate(){
        gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Gate>().setGateOpen(true);
        // PLAY ANIMATION
    }
    public void closeGate(){
        gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Gate>().setGateOpen(false);
        // PLAY ANIMATION
    }

    //-------------//
    // MESSAGE LOG //
    //-------------//
    public void openMessageLog(){
        messageLogOpen = true;
        GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponent<Canvas>().enabled = true;

        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(false); 
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }
    public void closeMessageLog(){
        messageLogOpen = false;
        GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponent<Canvas>().enabled = false;
    }

    //------------------//
    // PLAYER INVENTORY //
    //------------------//
    public void openInventory(){
        inventoryOpen = true;
        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(false); 
        GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponent<Canvas>().enabled = false;
    }
    public void closeInventory(){
        inventoryOpen = false;
        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }

    //--------------//
    // CRAFTINGMENU //
    //--------------//
    public void openCrafting(){
        craftingOpen = true;
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(true); 

        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(false); 
        GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponent<Canvas>().enabled = false;
    }
    public void closeCrafting(){
        craftingOpen = false;
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }
}