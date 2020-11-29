using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    // GAMEMANAGER
    GameManager gameManager;

    // CONFIRMATION MENU
    public string situation;

    // TOWN
    int townSelectedValue;

    // MENU BOOLS
    public bool menuBarOpen;
    public bool inventoryOpen;
    public bool craftingOpen;
    public bool messageLogOpen;
    public bool skillsOpen;
    public bool buildingMenuOpen;
    public bool buildingOpenOpen;
    public bool buildingInventoryOpenBool;
    public bool townOpen;    
    public bool abilitesOpen;
    public bool citizenOpen;
    public bool citizenInventoryOpen;
    public bool unfinishedBuildingOpen;
    
    //--------------------//
    // UI ELEMENT PREFABS //
    //--------------------//

    // CRAFTING
    public GameObject craftingBuilding; // FOR CRAFTING
    public GameObject craftingBuildingHover; // FOR CRAFTING

    // INVENTORY
    public GameObject inventorySlot; // FOR INVENTORY

    // SKILLS
    public GameObject skillSlot; // FOR SKILLS

    // ABILITIES
    public GameObject abilitiesSlot; // FOR ABILITIES

    // TOWN
    public GameObject building;  // FOR TOWN BUILDINGS
    public GameObject citizen; // FOR TOWN CITIZENS 
    public GameObject item; // FOR TOWN ITEM                                                                                                  

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        // INPUTS //
        if(Input.GetKeyDown("1")){
            selectToolbarElement("Magic");
        }
        if(Input.GetKeyDown("2")){
            selectToolbarElement("Melee");
        }
        if(Input.GetKeyDown("3")){
            selectToolbarElement("Ranged");
        }
        if(Input.GetKeyDown("4")){
            selectToolbarElement("Shield");
        }
        if(Input.GetKeyDown("5")){
            selectToolbarElement("Pickaxe");
        }
        if(Input.GetKeyDown("6")){
            selectToolbarElement("Axe");
        }
        if(Input.GetKeyDown("7")){
            selectToolbarElement("Hammer");
        }
        if(Input.GetKeyDown("8")){
            selectToolbarElement("Food");
        }
        if(Input.GetKeyDown("9")){

        }
        if(Input.GetKeyDown("t")){
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown("y")){
            Time.timeScale = 1;
        }
        if(Input.GetKeyDown("u")){
            Time.timeScale = 2f;
        }
        if(Input.GetKeyDown("i")){
            Time.timeScale = 4;
        }
        if(Input.GetKeyDown("o")){
            gameManager.getPlayerBehavior().getSkills().increaseExperience("Woodcutting", 100000);
        }
        if(Input.GetKeyDown("p")){

        }
        if(Input.GetKeyDown("g")){
            Dictionary<string,int> listOfItems = new Dictionary<string, int>{
                {"Wood planks", 3}, 
                {"Stone", 3}};

            foreach(var item in listOfItems){
                gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(item.Key, item.Value);
            }
        }
        if(Input.GetKeyDown("h")){
            Dictionary<string,int> listOfItems = new Dictionary<string, int>{
            {"Wand", 5}, 
            {"Helmet", 5}, 
            {"Sword", 5}, 
            {"Battleaxe", 5},
            {"Greatsword", 5},
            {"Ether pickaxe", 5},
            {"Ether axe", 5},
            {"Steel axe", 5},
            {"Stone hammer", 5},
            {"Wood bow", 5},
            {"Beef", 5},
            {"Staff", 5}
            };

            foreach(var item in listOfItems){
                gameManager.getInventoryCatalog().getMainInventory().addItemToInventory(item.Key, item.Value);
            }
        }
        if(Input.GetKeyDown("j")){
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
        if(Input.GetKeyDown("k")){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
                Dictionary<string,int> listOfItems = new Dictionary<string, int>{
                {"Wood log", 10}, 
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
        //if(Input.GetKeyDown("space")){
        //    GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<MoveCameraScript>().moveCameraToPosition(gameManager.getPlayerBehavior().getPlayerPosition());
        //}
        if(Input.GetKeyDown("tab")){
            openInventory();
        } 
        if(Input.GetKeyDown("z")){
            openMessageLog();
        } 
        if(Input.GetKeyDown("x")){
            openSkills();
        } 
        if(Input.GetKeyDown("c")){
            openCrafting();
        } 
        if(Input.GetKeyDown("v")){
            openTown();
        }
        if(Input.GetKeyDown("b")){
            openAbilities();
        }
        if(gameManager.getCraftingSystem().getIsCrafting()){
            if(Input.GetKeyDown("q")){
                gameManager.getCraftingSystem().rotateBuildingLeft();
            }
            if(Input.GetKeyDown("e")){
                gameManager.getCraftingSystem().rotateBuildingRight();
            }
        }
        if(Input.GetKeyDown("g")){
            //inventorySystem.spawnNPC(); Doesnt exist anymore, add new if u want
            // MOVED TO PLAYERBEHAVIOR
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
    public bool getCitizenOpen(){
        return citizenOpen;
    }
    public bool getCitizenInventoryOpen(){
        return citizenInventoryOpen;
    }
    public bool getUnfinishedBuildingOpen(){
        return unfinishedBuildingOpen;
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
        Transform background = GameObject.FindGameObjectWithTag("BuildingMenuUI").transform.Find("Background");

        
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

            background.gameObject.SetActive(true);
            background.position = new Vector3(Input.mousePosition.x + background.GetComponent<RectTransform>().sizeDelta.x/2, Input.mousePosition.y + background.GetComponent<RectTransform>().sizeDelta.y/2, 0);

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

                if(buildingAttributes.getBuildingTag().Equals("Storage")){
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
        GameObject.FindGameObjectWithTag("BuildingMenuUI").transform.Find("Background").gameObject.SetActive(false);
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
        if(!gameManager.getCraftingSystem().getIsCrafting()){
            buildingOpenOpen = true;
            BuildingAttributes building = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
            closeBuildingUI();

            closeAllBuildingOptions();
            buildingProductionOpen();
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(true);

            //HEADER
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Headline/HeadlineText").GetComponent<Text>().text = building.getBuildingName();
        }
    }
    public void buildingOpenClose(){
        closeAllBuildingOptions();
        buildingOpenOpen = false;
        resetSelectedItem();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(false);
    }

    // PRODCTION
    public void buildingProductionOpen(){
        closeAllBuildingOptions();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(true);
    }
    public void closeBuildingProduction(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Production").gameObject.SetActive(false);
    }

    // WORKERS
    public void buildingWorkersOpen(){
        closeAllBuildingOptions();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(true);
    }
    public void closeBuildingWorkers(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers").gameObject.SetActive(false);
    }

    // INVENTORY
    public void buildingInventoryOpen(){
        // OPEN PLAYER INVENTORY
        gameManager.GetUI().openInventory();

        //OPEN
        closeAllBuildingOptions();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(true);
        setBuildingInventoryOpen(true);
        updateBuildingInventory();
    }
    public void closeBuildingInventory(){
        setBuildingInventoryOpen(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
    }
    public void updateBuildingInventory(){
        updateInventory(gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>(), GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory/Scroll View/Viewport/Building Inventory"));
    }

    // STATS
    public void buildingStatsOpen(){
        closeAllBuildingOptions();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(true);

        BuildingAttributes building = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats/InfoLeft/InfoLeftText").GetComponent<Text>().text =
        "Name:\n" + building.getBuildingName() + "\n\nDescription:\n" + building.getBuildingDescription() + "\n\nStorage Capacity:\n" + building.getStorageCapacity() +
        "\n\nValue:\n" + building.getBuildingValue() + "\n\nRented:\n" + building.getIsRented();

        InventoryCatalog inventoryCatalog = gameManager.getInventoryCatalog();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats/InfoRight/Viewport/Content/Text").GetComponent<Text>().text =
        "Building upkeep:\n" + inventoryCatalog.getListOfItemsToString(building.getBuildingUpKeep()) + "\nItems Produced Here:\n" + inventoryCatalog.getListOfItemsToString(building.getItemsProducedInBuilding()) +
        "\nItems needed for production:\n" + inventoryCatalog.getListOfItemsToString(building.getItemsNeededForBuildingProduction());
    }
    public void closeBuildingStats(){
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Stats").gameObject.SetActive(false);
    }

    public void closeAllBuildingOptions(){
        closeBuildingProduction();
        closeBuildingWorkers();
        closeBuildingInventory();
        closeBuildingStats();
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
        bool producing = true;
        bool inventoryIsFull = false;
        int playerProduction = 0;

        if(selectedItem != null){
            if(!buildingAttributes.getBuildingIsProducing()){

                buildingAttributes.setBuildingIsProducing(true);
                buildingAttributes.setItemCurrentlyProduced(selectedItem);

                while(producing){
                    if(buildingAttributes.getWorkersInBuilding().Count == 0 && !buildingAttributes.getPlayerEnteredBuilding()){
                        gameManager.getMessageLogText().addMessageToLog("Production could not start since theres no workers assigned in the building!");
                        break;
                    }

                    while(buildingAttributes.getProductionProgress() <= 360 && !inventoryIsFull && buildingInventory.checkIfListOfItemsAreInInventory(selectedItem.getCostToCraftItem())){
                        if(buildingAttributes.getPlayerEnteredBuilding()){
                            playerProduction = 1;
                        } else {
                            playerProduction = 0;
                        }
                        float progressSpeed = (buildingAttributes.getCitizensInsideBuilding().Count + playerProduction) * 100;
                        buildingAttributes.setIncreaseProductionProgress(Time.deltaTime * progressSpeed);
                        //progressbar.updateProgressBar(buildingAttributes.getProductionProgress());

                        if(buildingAttributes.getProductionProgress() >= 360){
                            int itemsThatCouldNotBeAdded = buildingInventory.addItemToInventory(selectedItem.getName(), 1);
                            updateTownInventory();

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
                    yield return null;
                }
                buildingAttributes.setBuildingIsProducing(false);
                //buildingAttributes.setItemCurrentlyProduced(null);

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
                if(selectedCitizen){
                    selectedCitizen.getTownAlliegence().removeAvailableWorkerFromTown(selectedCitizen);
                    selectedBuilding.addWorkerToBuilding(selectedCitizen);
                }
            }
        } else {
            gameManager.getMessageLogText().addMessageToLog("You cant add more workers since the limit is reached");
        }
    }
    public void MoveWorkerToAvailable(){
        Citizen selectedCitizen = gameManager.getCitizenCatalog().getSelectedCitizen();
        BuildingAttributes selectedBuilding = gameManager.getBuildingCatalog().getBuildingLastClickedAttributes();
        if(selectedCitizen){
            if(!selectedCitizen.getTownAlliegence().getAvailableWorkersInTown().Contains(selectedCitizen)){
                selectedBuilding.removeWorkerFromBuilding(selectedCitizen);
            }
        }
    }   
    public void selectWorkerButton(int citizenID){
        gameManager.getCitizenCatalog().setSelectedCitizen(gameManager.getCitizenCatalog().getCitizenByID(citizenID));
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Workers Here/Scroll View/Viewport/Content").GetComponent<ShowWorkersInBuilding>().updateWorkersInBuildingList();
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

    //------------//
    // MAIN MENUS //
    //------------//
    public void closeAllMainMenus(){
        closeMenuBar();
        closeMessageLog();
        closeSkills();
        closeInventory();
        closeCrafting();
        closeTown();
        closeAbilities();
    }
    public void closeAllMainMenusOpenMenuBar(){
        openMenuBar();
        closeMessageLog();
        closeSkills();
        closeInventory();
        closeCrafting();
        closeTown();
        closeAbilities();
    }
    public void openMenuBar(){
        closeAllMainMenus();

        menuBarOpen = true;
        GameObject.FindGameObjectWithTag("MenuBarUI").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeMenuBar(){
        menuBarOpen = false;
        GameObject.FindGameObjectWithTag("MenuBarUI").transform.Find("Background").gameObject.SetActive(false); 
    }

    

    //----------------//
    // MESSAGELOGMENU //
    //----------------//
    public void openMessageLog(){
        if(GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background").gameObject.activeSelf == true){
            closeAllMainMenusOpenMenuBar();
            return;
        }

        closeAllMainMenus();
        messageLogOpen = true;
        GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeMessageLog(){
        messageLogOpen = false;
        GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background").gameObject.SetActive(false); 
    }

    //------------------//
    // INVENTORY MENU   //
    //------------------//
    public void openInventory(){
        if(GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeAllMainMenusOpenMenuBar();
            return;
        }

        
        closeAllMainMenus();
        inventoryOpen = true;
        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(true);
        updateMainInventory();
    }
    public void closeInventory(){
        inventoryOpen = false;
        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }
    public void updateMainInventory(){
        updateInventory(gameManager.getPlayerBehavior().getInventory(), GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background/Scroll View/Viewport/Main Inventory"));
    }

    public void updateInventory(Inventory inventory, Transform parentContent){
        if(parentContent.childCount > inventory.getInventoryCapacity()){
            removeInventory(parentContent, parentContent.childCount - inventory.getInventoryCapacity());
        }
        if(parentContent.childCount < inventory.getInventoryCapacity()){
            int amountToInstiate = inventory.getInventoryCapacity() - parentContent.childCount;
            for(int i = 0; i < amountToInstiate; i++){
                Instantiate(inventorySlot, parentContent);
            }
        }

        int counter = 0;
        foreach(InventorySlot inventorySlot in inventory.getInventorySlots()){
            parentContent.GetChild(counter).Find("Button").GetComponent<InventorySlotButton>().setInventorySlotID(counter);

            if(inventorySlot.getCurrentAmountInSlot() != 0){
                parentContent.GetChild(counter).transform.Find("Button/Text").GetComponent<Text>().text = inventory.getInventorySlots()[counter].getItemInSlot();
                parentContent.GetChild(counter).transform.Find("Panel/Text").GetComponent<Text>().text = inventory.getInventorySlots()[counter].getCurrentAmountInSlot().ToString();
            } else {
                parentContent.GetChild(counter).transform.Find("Button/Text").GetComponent<Text>().text = "";
                parentContent.GetChild(counter).transform.Find("Panel/Text").GetComponent<Text>().text = "";
            }
            counter++;
        }
    }
    public void removeInventory(Transform parentContent, int amount){
        int counter = 0;
        foreach(Transform child in parentContent){
            if(counter < amount){
                Destroy(child.gameObject);
            }
            counter++;
        }
    }

    //--------------//
    // CRAFTINGMENU //
    //--------------//
    public void openCrafting(){
        if(GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeAllMainMenusOpenMenuBar();
            return;
        }

        
        closeAllMainMenus();
        getPlayerOwnedSelectedTown().townIndicator(true);
        getPlayerOwnedSelectedTown().townIndicatorMode(true);

        showAllCraftingBuildings();
        craftingOpen = true;
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeCrafting(){
        craftingOpen = false;
        getPlayerOwnedSelectedTown().townIndicator(false);
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }
    public void showAllCraftingBuildings(){
        removeAllCraftingBuildingsItems();
        showCraftingBuildings(gameManager.getBuildingCatalog().getBuildingsCatalog());
    }
    public void showCraftingBuildings(List<Building> list){
        removeAllCraftingBuildingsItems();

        List<Building> buildingCatalog = gameManager.getBuildingCatalog().getBuildingsCatalog();
        int counter = 0;

        foreach(Building building in list){
            GameObject thisBuilding = (GameObject)Instantiate(craftingBuilding, GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background/Crafting Elements/Viewport/Content"));
            thisBuilding.GetComponent<CraftingButton>().setBuildingName(building.getNameOfBuilding());
            thisBuilding.transform.name = building.getNameOfBuilding();
            thisBuilding.transform.Find("Text").GetComponent<Text>().text = building.getNameOfBuilding();
            thisBuilding.GetComponent<CraftingButton>().setBuildingName(building.getNameOfBuilding());
            thisBuilding.GetComponent<CraftingButton>().setCostToCraftBuilding(building.getCostToCraftBuilding());

            foreach(var item in building.getCostToCraftBuilding()){
                GameObject costToCraftItem = Instantiate(craftingBuildingHover, thisBuilding.transform.Find("Hover/Cost").transform);
                costToCraftItem.GetComponent<Text>().text = item.Value + " " + item.Key;
                costToCraftItem.GetComponent<CostText>().setItemName(item.Key);
                costToCraftItem.GetComponent<CostText>().setAmount(item.Value);
            }
            counter++;
        }
    }
    public void showAllItems(){

    }
    public void showCraftingBuildingTag(string tag){
        removeAllCraftingBuildingsItems();
        showCraftingBuildings(gameManager.getBuildingCatalog().getBuildingsByTag(tag));
    }

    public void removeAllCraftingBuildingsItems(){
        foreach(Transform child in GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background/Crafting Elements/Viewport/Content")){
            Destroy(child.gameObject);
        }
    }

    //------------//
    // SKILLSMENU //
    //------------//
    public void openSkills(){
        if(GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.activeSelf == true){
            closeAllMainMenusOpenMenuBar();
            return;
        }

        closeAllMainMenus();
        updateSkills();
        skillsOpen = true;
        GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeSkills(){
        skillsOpen = false;
        GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.SetActive(false); 
    }
    public void updateSkills(){
        List<Skill> skills = gameManager.getPlayerBehavior().getSkills().GetSkills();
        Transform Skillscontent = GameObject.FindGameObjectWithTag("Skills").transform.Find("Background/Skills");

        if(Skillscontent.childCount > skills.Count){
            removeSkills(Skillscontent.childCount - skills.Count);
        }
        if(Skillscontent.childCount < skills.Count){
            int amountToInstiate = skills.Count - Skillscontent.childCount;
            for(int i = 0; i < amountToInstiate; i++){
                Instantiate(skillSlot, Skillscontent);
            }
        }

        int counter = 0;
        foreach(Skill skill in skills){
            Skillscontent.GetChild(counter).transform.Find("skillName").GetComponent<Text>().text = skill.getName();
            Skillscontent.GetChild(counter).transform.Find("level").GetComponent<Text>().text = skill.getLevel().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Exp").GetComponent<Text>().text = skill.getExperience().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Exp Next Lvl").GetComponent<Text>().text = skill.getExperienceNextLevel().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Exp Til Next").GetComponent<Text>().text = skill.getExperienceLeft().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Percentage").GetComponent<Text>().text = skill.getPercentageLeft().ToString("0.##") + "%";     
            counter++;        
        }
    }
    public void removeSkills(int amount){
        int counter = 0;
        foreach(Transform child in GameObject.FindGameObjectWithTag("Skills").transform.Find("Background/Skills")){
            if(counter < amount){   
                Destroy(child.gameObject);
            }
            counter++;
        }
    }

    //-------------//
    // TOOLBARMENU //
    //-------------//
    public void clickToolbarItem(string type){
        Toolbelt toolbar = gameManager.getToolbelt();

        // TRANSFER TO MAIN INVENTORY
        if(inventoryOpen){
            toolbar.transferFromToolbarToInventory(gameManager.getPlayerBehavior().getInventory(), type);
        } else {
            //Debug.Log("inventoryopen not open");
            selectToolbarElement(type);
        }
    }
    public void updateToolbarInterface(){
        List<InventorySlot> toolbar = gameManager.getToolbelt().getToolbar();

        int counter = 0;
        foreach(Transform child in GameObject.FindGameObjectWithTag("ToolbeltUI").transform.Find("Background/Content")){
            if(toolbar[counter].getItemInSlot() != null){
                child.Find("Text").GetComponent<Text>().text = toolbar[counter].getItemInSlot();

                int counter2 = 0;
                foreach(Transform ability in child.Find("Hoverpanel/Background/Content")){
                    if(ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton().Length > counter2){
                        if(ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton()[counter2] != null){
                            ability.Find("Abilityname").GetComponent<Text>().text = ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton()[counter2].getName();
                            ability.transform.name = ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton()[counter2].getName();
                        } else {
                            ability.Find("Abilityname").GetComponent<Text>().text = "";
                            ability.transform.name = "";
                        }
                        
                    }
                    counter2++;
                }

                // FOR FOOD
                if(counter == 7){
                    child.Find("Amount").GetComponent<Text>().text = toolbar[counter].getCurrentAmountInSlot().ToString();
                }
            } else {
                child.Find("Text").GetComponent<Text>().text = toolbar[counter].getInventorySlotType();

                int counter2 = 0;
                foreach(Transform ability in child.Find("Hoverpanel/Background/Content")){
                    if(ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton().Length > counter2){
                        if(ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton()[counter2] != null){
                            ability.Find("Abilityname").GetComponent<Text>().text = ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton()[counter2].getName();
                            ability.transform.name = ability.transform.GetComponentInParent<AbilitiesButton>().getAbilityButton()[counter2].getName();
                        } else {
                            ability.Find("Abilityname").GetComponent<Text>().text = "";
                            ability.transform.name = "";
                        }
                        
                    }
                    counter2++;
                }

                // FOR FOOD
                if(counter == 7){
                    child.Find("Amount").GetComponent<Text>().text = "";
                }
            }
            counter++;
        }
    }
    public void selectToolbarElement(string type){
        List<InventorySlot> toolbar = gameManager.getToolbelt().getToolbar();

        int counter = 0;
        foreach(InventorySlot inventorySlot in toolbar){
            Transform toolbarUI = GameObject.FindGameObjectWithTag("ToolbeltUI").transform.Find("Background/Content").GetChild(counter).transform;

            if(inventorySlot.getInventorySlotType().Equals(type)){
                toolbarUI.Find("Number").GetComponent<Text>().color = Color.yellow;
                toolbarUI.Find("Hoverpanel").transform.gameObject.SetActive(true);
            } else {
                toolbarUI.Find("Number").GetComponent<Text>().color = Color.white;
                toolbarUI.Find("Hoverpanel").transform.gameObject.SetActive(false);
            }
            counter++;
        }
    }

    public void removeAbilityFromToolbar(int slotNumber){

    }

    //--------------//
    // ABILITESMENU //
    //--------------//
    public void openAbilities(){
        if(GameObject.FindGameObjectWithTag("AbilitiesMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeAllMainMenusOpenMenuBar();
            return;
        }

        closeAllMainMenus();
        showAbilitesAll();
        abilitesOpen = true;
        GameObject.FindGameObjectWithTag("AbilitiesMenuUI").transform.Find("Background").gameObject.SetActive(true);
    }
    public void closeAbilities(){
        abilitesOpen = false;
        GameObject.FindGameObjectWithTag("AbilitiesMenuUI").transform.Find("Background").gameObject.SetActive(false);
    }
    public void showAbilities(List<Ability> list){
        removeAbilities();

        foreach(Ability ability in list){
            GameObject abilitySlot = (GameObject)Instantiate(abilitiesSlot, GameObject.FindGameObjectWithTag("AbilitiesMenuUI").transform.Find("Background/Abilities/Viewport/Content"));
            abilitySlot.transform.Find("Ability Name").GetComponent<Text>().text = ability.getName();
            abilitySlot.transform.name = ability.getName() + "/" + ability.getType();
            abilitySlot.transform.Find("Level").GetComponent<Text>().text = ability.getLevel().ToString();
            abilitySlot.transform.Find("Skill").GetComponent<Text>().text = ability.getSkill();
        }
    }
    public void showAbilitesAll(){
        showAbilities(gameManager.getAbilityCatalog().getAbilityCatalog());
    }
    public void showAbilitiesUnlocked(){
        showAbilities(gameManager.getPlayerBehavior().getPerkattributes().getAbilitiesUnlocked());
    }
    public void showAbilitiesBySkill(string skill){
        showAbilities(gameManager.getAbilityCatalog().getAbilityBySkill(skill));
    }
    public void removeAbilities(){
        foreach(Transform child in GameObject.FindGameObjectWithTag("AbilitiesMenuUI").transform.Find("Background/Abilities/Viewport/Content")){
            Destroy(child.gameObject);
        }
    }

    public void clickAbilityButton(GameObject button){
        string[] abilityFull = button.transform.name.Split('/');
        string abilityName = abilityFull[0]; 
        string abilityType = abilityFull[1];
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Ability selectedAbility = gameManager.getAbilityCatalog().getAbilityByNameAndType(abilityName, abilityType);
        Transform abilitiesToolbelt = GameObject.FindGameObjectWithTag("ToolbeltUI").transform.Find("Background/Content");

        foreach(Transform child in abilitiesToolbelt){
            AbilitiesButton slot = child.GetComponent<AbilitiesButton>();

            if(slot.getButtonName().Equals(selectedAbility.getType())){
                slot.addAbilityToAbilities(selectedAbility);
            
            }
        }
        updateToolbarInterface();
        selectToolbarElement(selectedAbility.getType());
    }

    //----------//
    // TOWNMENU //
    //----------//
    public void openTown(){
        if(GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeAllMainMenusOpenMenuBar();
            return;
        }

        // DROPDOWN OF TOWNS
        instantiateDropdown();

        closeAllMainMenus();
        townOpen = true;
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background").gameObject.SetActive(true);
    }  
    public void closeTown(){
        townOpen = false;
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background").gameObject.SetActive(false);
    }

    public void instantiateDropdown(){
        if(GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().options.Count != gameManager.getPlayerBehavior().getTownsOwned().Count){
            GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().ClearOptions();
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            foreach(Town town in gameManager.getPlayerBehavior().getTownsOwned()){
                options.Add(new Dropdown.OptionData(town.getTownName()));
            }
            GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().AddOptions(options);
        }
    }

    // BUILDINGS
    public void openTownBuildings(){
        
        closeAllTownOptions();
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Buildings").gameObject.SetActive(true);  

        showAllBuildings();
    }
    public void showBuildings(List<BuildingAttributes> list){
        GameObject buildingList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Buildings/Show Buildings/Viewport/Content").gameObject; 
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Buildings/Show Number/Text").GetComponent<Text>().text = list.Count.ToString(); 

        removeAllBuildings();

        foreach(BuildingAttributes buildingInTown in list){
            GameObject instantiateBuilding = Instantiate(building, buildingList.transform);
            instantiateBuilding.transform.GetComponent<buildingBotton>().setBuilding(buildingInTown);
            instantiateBuilding.transform.Find("Name").GetComponent<Text>().text = buildingInTown.getBuildingName();
            instantiateBuilding.transform.Find("Residents").GetComponent<Text>().text = buildingInTown.getResidentsInBuilding().Count.ToString() + "/" + buildingInTown.getResidentialLimit().ToString();
            instantiateBuilding.transform.Find("Workers").GetComponent<Text>().text = buildingInTown.getWorkersInBuilding().Count.ToString() + "/" + buildingInTown.getWorkerLimit().ToString();
            instantiateBuilding.transform.Find("Value").GetComponent<Text>().text = buildingInTown.getBuildingValue().ToString();
            instantiateBuilding.transform.Find("Tax income").GetComponent<Text>().text = buildingInTown.getBuildingTotalTaxPaymentDaily().ToString();
            instantiateBuilding.transform.Find("Type").GetComponent<Text>().text = buildingInTown.getBuildingTag();

            if(instantiateBuilding.transform.Find("Residents").GetComponent<Text>().text.Equals("0/0")){
                instantiateBuilding.transform.Find("Residents").GetComponent<Text>().text = "";
            }
            if(instantiateBuilding.transform.Find("Workers").GetComponent<Text>().text.Equals("0/0")){
                instantiateBuilding.transform.Find("Workers").GetComponent<Text>().text = "";
            }
            if(buildingInTown.getBuildingTag().Equals("Fortification")){
                instantiateBuilding.transform.Find("Tax income").GetComponent<Text>().text = "";
            }
        }
    }
    public void closeTownBuildings(){
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Buildings").gameObject.SetActive(false);
    }
    public void showAllBuildings(){
        showBuildings(getPlayerOwnedSelectedTown().getBuildingsInTown());
    }
    public void showAvailableResidential(){
        showBuildings(getPlayerOwnedSelectedTown().getAvailableResidentialBuildingsInTown());
    }
    public void showAvailableWorkplace(){
        showBuildings(getPlayerOwnedSelectedTown().getAvailableWorkplacesInTown());
    }
    public void showBuildingWithTag(string tag){
        showBuildings(getPlayerOwnedSelectedTown().getBuildingsInTownWithTag(tag));
    }
    public void removeAllBuildings(){
        GameObject buildingList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Buildings/Show Buildings/Viewport/Content").gameObject; 

        foreach(Transform child in buildingList.transform){
            Destroy(child.gameObject);
        }
    }

    // CITIZENS
    public void openTownCitizens(){

        closeAllTownOptions();
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens").gameObject.SetActive(true);

        showAllCitizens();
    }
    public void closeTownCitizens(){
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens").gameObject.SetActive(false);
    }
    public void showCitizens(List<Citizen> list){
        GameObject citizenList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens/Show Citizens/Viewport/Content").gameObject; 
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens/Show Number/Text").GetComponent<Text>().text = list.Count.ToString(); 

        removeAllCitizens();

        foreach(Citizen citizenInTown in list){
            GameObject instantiateCitizen = Instantiate(citizen, citizenList.transform);
            instantiateCitizen.transform.Find("Name").GetComponent<Text>().text = citizenInTown.getFirstName() + " " + citizenInTown.getLastName();
            instantiateCitizen.transform.Find("Gender").GetComponent<Text>().text = citizenInTown.getGender();
            instantiateCitizen.transform.Find("Age").GetComponent<Text>().text = citizenInTown.getAge().ToString();
            instantiateCitizen.transform.Find("Job").GetComponent<Text>().text = citizenInTown.getJob();
            instantiateCitizen.transform.Find("Happiness").GetComponent<Text>().text = citizenInTown.getHappiness().ToString();
            instantiateCitizen.transform.Find("Wealth").GetComponent<Text>().text = citizenInTown.getWealth().ToString();
        }
    }
    /*public void updateCitizens(){
        GameObject citizenList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens/Show Citizens/Viewport/Content").gameObject; 
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens/Show Number/Text").GetComponent<Text>().text = list.Count.ToString(); 

        foreach(Transform child in citizenList.transform){
            Citizen citizen = child.GetComponent<Citizen>();
            child.transform.Find("Name").GetComponent<Text>().text = citizen.getFirstName() + " " + citizen.getLastName();
            child.transform.Find("Gender").GetComponent<Text>().text = citizen.getGender();
            child.transform.Find("Age").GetComponent<Text>().text = citizen.getAge().ToString();
            child.transform.Find("Job").GetComponent<Text>().text = citizen.getJob();
            child.transform.Find("Happiness").GetComponent<Text>().text = citizen.getHappiness().ToString();
            child.transform.Find("Wealth").GetComponent<Text>().text = citizen.getWealth().ToString();
        }
    }*/
    public void showAllCitizens(){
        showCitizens(getPlayerOwnedSelectedTown().getCitizensInTown());
    }
    public void showUnemployedCitizens(){
        showCitizens(getPlayerOwnedSelectedTown().getUnemployedInTown());
    }
    public void showHomelessCitizens(){
        showCitizens(getPlayerOwnedSelectedTown().getHomelessInTown());
    }
    public void removeAllCitizens(){
        GameObject citizenList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Citizens/Show Citizens/Viewport/Content").gameObject; 

        foreach(Transform child in citizenList.transform){
            Destroy(child.gameObject);
        }
    }

    // INVENTORY
    public void openTownInventory(){
        closeAllTownOptions();
        showAllItemsInTownInventory();
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory").gameObject.SetActive(true);
    }
    public void closeTownInventory(){
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
    }
    public void showItemsInTownInventory(List<Item> list){
        removeAllItemsTownInventory();
        GameObject inventoryList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory/Show Inventory/Viewport/Content").gameObject; 

        foreach(Item itemInList in list){
            GameObject stuff = Instantiate(item, inventoryList.transform);
            stuff.transform.name = itemInList.getName();
            stuff.transform.Find("Item").GetComponent<Text>().text = itemInList.getName();
            stuff.transform.Find("Amount").GetComponent<Text>().text = 0.ToString();
            foreach(var item in getPlayerOwnedSelectedTown().getTownInventory()){
                if(item.Key.Equals(itemInList.getName())){
                    stuff.transform.Find("Amount").GetComponent<Text>().text = item.Value.ToString();
                    break;
                } 
            } 
        }
        foreach(Transform child in inventoryList.transform){
            foreach(var item in getPlayerOwnedSelectedTown().getTownInventory()){
                if(item.Key.Equals(child.transform.name)){
                    child.transform.Find("Amount").GetComponent<Text>().text = item.Value.ToString();
                    break;
                }  
            }
        }
    }
    public void showAllItemsInTownInventory(){
        showItemsInTownInventory(gameManager.getItemCatalog().getItemCatalog());
    }
    public void showItemWithTypeTownInventory(string type){
        showItemsInTownInventory(gameManager.getItemCatalog().getItemByType(type));
    }
    public void updateTownInventory(){
        GameObject inventoryList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory/Show Inventory/Viewport/Content").gameObject; 

        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory/Show Number/Text").GetComponent<Text>().text = getPlayerOwnedSelectedTown().getTownInventory().Count.ToString();

        foreach(Transform child in inventoryList.transform){
            foreach(var item in getPlayerOwnedSelectedTown().getTownInventory()){
                if(item.Key.Equals(child.transform.name)){
                    child.transform.Find("Amount").GetComponent<Text>().text = item.Value.ToString();
                    break;
                }  
            }
        }
    }
    public void removeAllItemsTownInventory(){
        GameObject inventoryList = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory/Show Inventory/Viewport/Content").gameObject; 

        foreach(Transform child in inventoryList.transform){
            Destroy(child.gameObject);
        }
    }

    // MILITARY
    public void openTownMilitary(){

    }
    public void closeTownMilitary(){

    }

    // ECONOMY
    public void openTownEconomy(){

    }
    public void closeTownEconomy(){

    }

    // COMMANDS
    public void openTownCommands(){

    }
    public void closeTownCommands(){

    }

    // STATS
    public void openTownStats(){

    }
    public void closeTownStats(){

    }

    // COUNCIL
    public void openTownCouncil(){

    }
    public void closeTownCouncil(){

    }
    public void closeAllTownOptions(){
        closeTownBuildings();
        closeTownCitizens();
        closeTownInventory();
        closeTownMilitary();
        closeTownEconomy();
        closeTownCommands();
        closeTownStats();
        closeTownCouncil();
    }
    public Town getPlayerOwnedSelectedTown(){
        townSelectedValue = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().value;
        return gameManager.getPlayerBehavior().getTownOwnedByIndex(townSelectedValue);
    }

    //-------------//
    // CITIZENMENU //
    //-------------//
    public void openCitizenMenu(){
        closeAllCitizenOptions();

        Citizen citizen = gameManager.getCitizenCatalog().getSelectedCitizen();
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Header").GetComponent<Text>().text = citizen.getFirstName() + " " + citizen.getLastName();
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background").gameObject.SetActive(true);

        openCitizenStats();
        citizenOpen = true;
    }
    public void closeCitizenMenu(){
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background").gameObject.SetActive(false);
        citizenOpen = false;
    }

    // STATS
    public void openCitizenStats(){
        closeAllCitizenOptions();
        Citizen citizen = gameManager.getCitizenCatalog().getSelectedCitizen();
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Stats").gameObject.SetActive(true);
        Transform elementsLocation = GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Stats/");


        elementsLocation.Find("Firstname").GetComponent<Text>().text = "Firstname: " + citizen.getFirstName();
        elementsLocation.Find("Lastname").GetComponent<Text>().text = "Lastname: " + citizen.getLastName();
        elementsLocation.Find("Age").GetComponent<Text>().text = "Age: " + citizen.getAge().ToString();
        elementsLocation.Find("Gender").GetComponent<Text>().text = "Gender: " + citizen.getGender();
        elementsLocation.Find("Happiness").GetComponent<Text>().text = "Happiness: " + citizen.getHappiness().ToString();
        elementsLocation.Find("Job").GetComponent<Text>().text = "Job: " + citizen.getJob();
        elementsLocation.Find("Wealth").GetComponent<Text>().text = "Wealth: " + citizen.getWealth().ToString();
        elementsLocation.Find("Income").GetComponent<Text>().text = "Income: " + citizen.getIncome().ToString();
        elementsLocation.Find("Tax payment").GetComponent<Text>().text = "Tax payment: " + citizen.getAmountToPayInTax().ToString();
        elementsLocation.Find("Status").GetComponent<Text>().text = "Status: " + citizen.getStatus();
        elementsLocation.Find("Best Skill").GetComponent<Text>().text = "Best skill: " + citizen.getSkills().getSkillWithHighestLevel().getLevel().ToString() + " " + citizen.getSkills().getSkillWithHighestLevel().getName();
        elementsLocation.Find("Second Best Skill").GetComponent<Text>().text = "2th skill: " + citizen.getSkills().getSkillWithSecondHighestLevel().getLevel().ToString() + " " + citizen.getSkills().getSkillWithSecondHighestLevel().getName();
        elementsLocation.Find("Health").GetComponent<Text>().text = "Health: " + citizen.getHealth().ToString();
        elementsLocation.Find("Hunger").GetComponent<Text>().text = "Hunger: " + citizen.getHunger().ToString();
        elementsLocation.Find("Thirst").GetComponent<Text>().text = "Thirst: " + citizen.getThrist().ToString();
    }
    public void closeCitizenStats(){
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Stats").gameObject.SetActive(false);
    }
    public void clickCitizenHome(){
        Citizen citizen = gameManager.getCitizenCatalog().getSelectedCitizen();

        if(gameManager.getCitizenCatalog().getSelectedCitizen()){
            if(gameManager.getCitizenCatalog().getSelectedCitizen().getHome() != null){
                GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<MoveCameraScript>().moveCameraToPosition(citizen.getHome().transform.position);
            }
        }
        closeCitizenMenu();
    }
    public void clickCitizenWorkplace(){
        Citizen citizen = gameManager.getCitizenCatalog().getSelectedCitizen();

        if(gameManager.getCitizenCatalog().getSelectedCitizen().getWork() != null){
            GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<MoveCameraScript>().moveCameraToPosition(citizen.getWork().transform.position);
        }
        closeCitizenMenu();
    }
    public void clickCitizenTown(){
        Citizen citizen = gameManager.getCitizenCatalog().getSelectedCitizen();

        if(gameManager.getCitizenCatalog().getSelectedCitizen().getTownAlliegence() != null){
            GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<MoveCameraScript>().moveCameraToPosition(citizen.getTownAlliegence().transform.position);
        }
        closeCitizenMenu();
    }

    // SKILLS
    public void openCitizenSkills(){
        closeAllCitizenOptions();
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Skills").gameObject.SetActive(true);
        updateCitizenSkills();
    }
    public void updateCitizenSkills(){
        Citizen citizen = gameManager.getCitizenCatalog().getSelectedCitizen();
        List<Skill> skills = citizen.getSkills().GetSkills();
        Transform Skillscontent = GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Skills/Skills content");

        if(Skillscontent.childCount > skills.Count){
            removeCitizenSkills(Skillscontent.childCount - skills.Count);
        }
        if(Skillscontent.childCount < skills.Count){
            int amountToInstiate = skills.Count - Skillscontent.childCount;
            for(int i = 0; i < amountToInstiate; i++){
                Instantiate(skillSlot, Skillscontent);
            }
        }

        int counter = 0;
        foreach(Skill skill in skills){
            Skillscontent.GetChild(counter).transform.Find("skillName").GetComponent<Text>().text = skill.getName();
            Skillscontent.GetChild(counter).transform.Find("level").GetComponent<Text>().text = skill.getLevel().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Exp").GetComponent<Text>().text = skill.getExperience().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Exp Next Lvl").GetComponent<Text>().text = skill.getExperienceNextLevel().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Exp Til Next").GetComponent<Text>().text = skill.getExperienceLeft().ToString();
            Skillscontent.GetChild(counter).transform.Find("Experience Panel/Percentage").GetComponent<Text>().text = skill.getPercentageLeft().ToString("0.##") + "%";  
            counter++;           
        }
    }
    public void closeCitizenSkills(){
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Skills").gameObject.SetActive(false);
    }
    public void removeCitizenSkills(int amount){
        int counter = 0;
        foreach(Transform child in GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Skills/Skills content")){
            if(counter < amount){
                Destroy(child.gameObject);
            }
            counter++;
        }
    }

    // INVENTORY
    public void openCitizenInventory(){
        closeAllCitizenOptions();
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Inventory").gameObject.SetActive(true);
        updateCitizenInventory();
        citizenInventoryOpen = true;
    }
    public void closeCitizenInventory(){
        GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
        citizenInventoryOpen = false;
    }
    public void updateCitizenInventory(){
        updateInventory(gameManager.getCitizenCatalog().getSelectedCitizen().getInventory(), GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background/Inventory/Scroll View/Viewport/Citizen Inventory"));
    }

    public void closeAllCitizenOptions(){
        closeCitizenStats();
        closeCitizenSkills();
        closeCitizenInventory();
    }

    //------------------------//
    // UNFINISHEDBUILDINGMENU //
    //------------------------//
    public void openUnfinishedBuilding(){
        closeUnfinishedBuilding();
        UnfinishedBuilding selectedUnfinishedBuilding = gameManager.getBuildingCatalog().getUnfinishedBuildingSelected();
        Transform content = GameObject.FindGameObjectWithTag("UnfinishedBuildingMenuUI").transform.Find("Background");
        
        
        content.gameObject.SetActive(true);
        content.Find("Header").GetComponent<Text>().text = "Unfinished " + selectedUnfinishedBuilding.getBuilding().getBuildingName();
        content.Find("Builder Progress").GetComponent<Text>().text = "Building progress: " + selectedUnfinishedBuilding.getBuilderProgress() + "/" + selectedUnfinishedBuilding.getBuilderMaxProgress();
        content.Find("Material Progress").GetComponent<Text>().text = "Material progress: " + selectedUnfinishedBuilding.getMaterialProgress() + "/" + selectedUnfinishedBuilding.getMaterialMaxProgress();

        string materialNeeded = "";
        foreach(var item in selectedUnfinishedBuilding.getItemsNeededToCraft()){
            materialNeeded += item.Value + " " + item.Key.ToLower() + "\n";
        }
        content.Find("Material Needed").GetComponent<Text>().text = "Materials needed to craft:\n" + materialNeeded;
        

        unfinishedBuildingOpen = true;
        updateUnfinishedBuildingInventory();
    }
    public void closeUnfinishedBuilding(){
        GameObject.FindGameObjectWithTag("UnfinishedBuildingMenuUI").transform.Find("Background").gameObject.SetActive(false);
        unfinishedBuildingOpen = false;
    }
    
    public void updateUnfinishedBuildingInventory(){
        updateInventory(gameManager.getBuildingCatalog().getUnfinishedBuildingSelected().getInventory(), GameObject.FindGameObjectWithTag("UnfinishedBuildingMenuUI").transform.Find("Background/Inventory/Scroll View/Viewport/Unfinished Building Inventory"));
    }
}