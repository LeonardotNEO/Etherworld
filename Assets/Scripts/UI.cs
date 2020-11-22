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

    // MENU BOOLS
    public bool inventoryOpen;
    public bool craftingOpen;
    public bool messageLogOpen;
    public bool skillsOpen;
    public bool buildingMenuOpen;
    public bool buildingOpenOpen;
    public bool buildingInventoryOpenBool;
    public bool townOpen;    

    // UI ELEMENT PREFABS
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
        if(Input.GetKeyDown("|")){
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown("1")){
            Time.timeScale = 1;
        }
        if(Input.GetKeyDown("2")){
            Time.timeScale = 2f;
        }
        if(Input.GetKeyDown("3")){
            Time.timeScale = 4;
        }
        if(Input.GetKeyDown("4")){
            Time.timeScale = 8;
        }
        if(Input.GetKeyDown("5")){
            gameManager.getPlayerBehavior().getSkills().getSkillByName("Woodcutting").increaseExperience(100000);
        }
        if(Input.GetKeyDown("6")){

        }
        if(Input.GetKeyDown("7")){
            Dictionary<string,int> listOfItems = new Dictionary<string, int>{
                {"Wood planks", 3}, 
                {"Stone", 3}};

            foreach(var item in listOfItems){
                gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(item.Key, item.Value);
            }
        }
        if(Input.GetKeyDown("8")){
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
        if(Input.GetKeyDown("9")){
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

            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background").gameObject.SetActive(true);

            //HEADER
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Headline/HeadlineText").GetComponent<Text>().text = building.getBuildingName();
        }
    }
    public void buildingOpenClose(){
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
        setBuildingInventoryOpen(true);
        gameManager.GetUI().openInventory();

        //OPEN
        closeAllBuildingOptions();
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(true);
    }
    public void closeBuildingInventory(){
        setBuildingInventoryOpen(false);
        GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.SetActive(false);
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
        closeMessageLog();
        closeSkills();
        closeInventory();
        closeCrafting();
        closeTown();
    }

    //-------------//
    // MESSAGE LOG //
    //-------------//
    public void openMessageLog(){
        if(GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background").gameObject.activeSelf == true){
            closeMessageLog();
            return;
        }

        messageLogOpen = true;
        closeAllMainMenus();
        GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeMessageLog(){
        messageLogOpen = false;
        GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background").gameObject.SetActive(false); 
    }

    //------------------//
    // PLAYER INVENTORY //
    //------------------//
    public void openInventory(){
        if(GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeInventory();
            return;
        }

        inventoryOpen = true;
        closeAllMainMenus();
        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(true);
    }
    public void closeInventory(){
        inventoryOpen = false;
        GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }

    //--------------//
    // CRAFTINGMENU //
    //--------------//
    public void openCrafting(){
        if(GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeCrafting();
            return;
        }

        craftingOpen = true;
        closeAllMainMenus();
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeCrafting(){
        craftingOpen = false;
        GameObject.FindGameObjectWithTag("CraftingMenuUI").transform.Find("Background").gameObject.SetActive(false); 
    }

    //--------//
    // SKILLS //
    //--------//
    public void openSkills(){
        if(GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.activeSelf == true){
            closeSkills();
            return;
        }

        skillsOpen = true;
        closeAllMainMenus();
        GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.SetActive(true); 
    }
    public void closeSkills(){
        skillsOpen = false;
        GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.SetActive(false); 
    }

    //------//
    // TOWN //
    //------//
    public void openTown(){
        if(GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background").gameObject.activeSelf == true){
            closeTown();
            return;
        }

        // DROPDOWN OF TOWNS
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach(Town town in gameManager.getPlayerBehavior().getTownsOwned()){
            options.Add(new Dropdown.OptionData(town.getTownName()));
        }
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().AddOptions(options);


        townOpen = true;
        closeAllMainMenus();
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background").gameObject.SetActive(true);
    }  
    public void closeTown(){
        GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background").gameObject.SetActive(false);
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
        int townSelectedValue = GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/TownList").GetComponent<Dropdown>().value;
        return gameManager.getPlayerBehavior().getTownOwnedByIndex(townSelectedValue);
    }
}