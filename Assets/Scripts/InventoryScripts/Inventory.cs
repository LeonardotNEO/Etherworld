using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int inventoryCapacity;
    public string inventoryName;
    private GameManager gameManager;
    public int inventoryID;
    
    void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        inventoryName = transform.name;
    }

    void Start()
    {
        StartCoroutine(instatiateInventory());
    }

    public IEnumerator instatiateInventory(){
        yield return new WaitForSeconds(0.5f);

        inventoryCapacity = 3;

        if(transform.gameObject.layer == LayerMask.NameToLayer("Buildings")){
            inventoryCapacity = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getStorageCapacity();
        }
        if(transform.gameObject.layer == LayerMask.NameToLayer("Citizens")){
            inventoryCapacity = 5;
        }
        if(transform.tag == "player"){
            inventoryCapacity = 30;
        }

        // INSTATIATING AN INVENTORY ON UNFINISHED BUILDING
        if(GetComponent<UnfinishedBuilding>()){
            int amountInventorySlots = 0;

            foreach(var item in GetComponent<UnfinishedBuilding>().getItemsNeededToCraft()){
                int amountToAdd = item.Value;
                int amountOfInventorySlots = amountToAdd/99;
                float rest = 0;

                if(amountToAdd/99.0f <= 1){
                    amountOfInventorySlots = 1;
                } else {
                    rest = (float)(amountToAdd/99f) % (float)amountOfInventorySlots;
                    if(rest != 0){
                        amountOfInventorySlots++;
                    }
                }

                for(int i = 0; i < amountOfInventorySlots; i++){
                    if(i == amountOfInventorySlots - 1){
                        //Debug.Log();
                        inventorySlots.Add(new InventorySlot(amountToAdd - (99*i), new List<string>(){item.Key}));
                    } else {
                        inventorySlots.Add(new InventorySlot(99, new List<string>(){item.Key}));
                    }
                    amountInventorySlots++;
                }
            }
            inventoryCapacity = amountInventorySlots;
        }

        // ADDING THE INVENTORY SLOTS
        if(!GetComponent<UnfinishedBuilding>()){
            for(int i = 0; i < inventoryCapacity; i++){
                inventorySlots.Add(new InventorySlot(99, null));
            }
        }
        


        // Gets the inventory catalog and add this inventory to it
        if(transform.tag == "player"){
            gameManager.getInventoryCatalog().addInventoryToCatalogAtIndex(0, this);
            inventoryID = 0;
        } else {
            gameManager.getInventoryCatalog().addInventoryToCatalog(this);
            inventoryID = gameManager.getInventoryCatalog().getAmountOfInventoriesInCatalog();
        } 

        if(transform.tag != "player" && transform.GetComponent<EnemyAttributes>()){
            foreach(var item in transform.GetComponent<EnemyAttributes>().getDroptable()){
                string itemName = null;
                int itemAmount = 0;
                float droprate = item.Value;
                foreach(var itemNameAmount in item.Key){
                    itemName = itemNameAmount.Key;
                    itemAmount = itemNameAmount.Value;
                }
                if(Random.Range(0f, 1f) <= droprate){
                    this.addItemToInventory(itemName, itemAmount);
                }
            }
        }
    }
    
    public Inventory getInventory(){
        return this;
    }

    public int getInventoryCapacity(){
        return inventoryCapacity;
    }

    public string getInventoryName(){
        return inventoryName;
    }


    public void clickInventoryItem(int slotnumber, Inventory toInventory){
        
        // TRANSFER TO BUILDING INVENTORY
        if(gameManager.getBuildingCatalog().getBuildingLastClicked() != null){
            if(gameManager.GetUI().getBuildingInventoryOpen() && this.getInventorySlot(slotnumber).currentAmountInSlot != 0){
                if(gameManager.getBuildingCatalog().getBuildingLastClickedAttributes().getPlayerEnteredBuilding()){
                    int amountToRemove = toInventory.addItemToInventory(this.getInventorySlot(slotnumber).getItemInSlot() , this.getInventorySlot(slotnumber).getCurrentAmountInSlot());
                    removeAmountFromSpecificSlot(this.getInventorySlot(slotnumber), this.getInventorySlot(slotnumber).getCurrentAmountInSlot() - amountToRemove);
                    updateInventoryInterface();

                } else {
                    gameManager.getMessageLogText().addMessageToLog("The player needs to be inside the building in order to transfer items to building inventory");
                }
            }
        }

        // TRANSFER TO UNFINISHED BUILDING
        if(gameManager.getBuildingCatalog().getUnfinishedBuildingSelected() != null){
            if(gameManager.GetUI().getUnfinishedBuildingOpen() && this.getInventorySlot(slotnumber).currentAmountInSlot != 0){

                    // TODO: if statement that prohbit player from transfering if to far away
                    int amountToRemove = toInventory.addItemToInventory(this.getInventorySlot(slotnumber).getItemInSlot() , this.getInventorySlot(slotnumber).getCurrentAmountInSlot());
                    removeAmountFromSpecificSlot(this.getInventorySlot(slotnumber), this.getInventorySlot(slotnumber).getCurrentAmountInSlot() - amountToRemove);
                    updateInventoryInterface();
            }
        }

        // TRANSFER TO CITIZEN
        if(gameManager.getCitizenCatalog().getSelectedCitizen() != null){
            if(gameManager.GetUI().getCitizenInventoryOpen() && this.getInventorySlot(slotnumber).currentAmountInSlot != 0){

                    // TODO: if statement that prohbit player from transfering if to far away
                    int amountToRemove = toInventory.addItemToInventory(this.getInventorySlot(slotnumber).getItemInSlot() , this.getInventorySlot(slotnumber).getCurrentAmountInSlot());
                    removeAmountFromSpecificSlot(this.getInventorySlot(slotnumber), this.getInventorySlot(slotnumber).getCurrentAmountInSlot() - amountToRemove);
                    updateInventoryInterface();
            }
        }

        // TRANSFER TO TOOLBAR
        if(!gameManager.GetUI().getCitizenInventoryOpen() && !gameManager.GetUI().getUnfinishedBuildingOpen() && !gameManager.GetUI().getBuildingInventoryOpen() && this.getInventorySlot(slotnumber).currentAmountInSlot != 0){
            //if(gameManager.getPlayerBehavior().getPerkattributes().getPerksByName(this.getInventorySlot(slotnumber).getItemInSlot()) != null){
                gameManager.getPlayerBehavior().getToolbelt().addToSlot(this.getInventorySlot(slotnumber));
                updateInventoryInterface();
            //} else {
                //Debug.Log("Dont have the perk associated with that item");
            //}
        }
        if(gameManager.GetUI().getInventoryOpen() && gameManager.GetUI().getEquipmentOpen() && this.getInventorySlot(slotnumber).currentAmountInSlot != 0){
            gameManager.getPlayerBehavior().getToolbelt().addToSlot(this.getInventorySlot(slotnumber));
            updateInventoryInterface();
        }

        
    }

    public void removeAmountFromSpecificSlot(InventorySlot inventorySlotSpecific, int amount){
        inventorySlotSpecific.decreaseCurrentAmountInSlot(amount);
        updateInventoryInterface();
    }

    public int addItemToInventory(string item, int amount){
        int amountLeftToAdd = amount;
        List<InventorySlot> invSlotsWithItemAlready = new List<InventorySlot>();

        if(item.Equals(null)){
            Debug.Log("Can add item thats null");
            return 0;
        }

        foreach(InventorySlot inventorySlot in inventorySlots){
            if(inventorySlot.getItemInSlot() == item && inventorySlot.getCurrentAmountInSlot() < 99){
                invSlotsWithItemAlready.Add(inventorySlot);
            }
        }
        
        foreach(InventorySlot inventorySlot in invSlotsWithItemAlready){
            if(amountLeftToAdd == 0){
                break;
            }
            if(amountLeftToAdd + inventorySlot.getCurrentAmountInSlot() <= inventorySlot.getslotCapacity()){
                int amountToAddToThisSlot = amountLeftToAdd;
                inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                amountLeftToAdd -= amountToAddToThisSlot;
            } else {
                int amountToAddToThisSlot = inventorySlot.getslotCapacity() - inventorySlot.getCurrentAmountInSlot();
                inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                amountLeftToAdd -= amountToAddToThisSlot;
            }
        }


        foreach(InventorySlot inventorySlot in inventorySlots){
            if(amountLeftToAdd == 0){
                break;
            }
            if(inventorySlot.getItemInSlot() == item){
                if(amountLeftToAdd + inventorySlot.getCurrentAmountInSlot() <= inventorySlot.getslotCapacity()){
                    int amountToAddToThisSlot = amountLeftToAdd;
                    inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                    amountLeftToAdd -= amountToAddToThisSlot;
                } else {
                    int amountToAddToThisSlot = inventorySlot.getslotCapacity() - inventorySlot.getCurrentAmountInSlot();
                    inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                    amountLeftToAdd -= amountToAddToThisSlot;
                }
            } else {
                if(inventorySlot.getCurrentAmountInSlot() == 0 && (inventorySlot.getInventorySlotType() == null || inventorySlot.getInventorySlotType().Contains(item))){
                    if(amountLeftToAdd + inventorySlot.getCurrentAmountInSlot() <= inventorySlot.getslotCapacity()){
                        int amountToAddToThisSlot = amountLeftToAdd;
                        inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        inventorySlot.setItemInSlot(item);
                        amountLeftToAdd -= amountToAddToThisSlot;
                    } else {
                        int amountToAddToThisSlot = inventorySlot.getslotCapacity() - inventorySlot.getCurrentAmountInSlot();
                        inventorySlot.increaseCurrentAmountInSlot(amountToAddToThisSlot);
                        amountLeftToAdd -= amountToAddToThisSlot;
                        inventorySlot.setItemInSlot(item);
                    }
                }
            }
        }
        if(amountLeftToAdd != 0){
            gameManager.getMessageLogText().addMessageToLog("Inventory is full! " + amountLeftToAdd + " " + item + " could not be added to the inventory.");
        }
        updateInventoryInterface();
        
        return amountLeftToAdd;
    }

    
    public void removeItemFromInventory(string item, int amount){
        int amountToRemove = amount;

        for(int i = inventoryCapacity - 1; i > -1; i--){
            
            if(inventorySlots[i].getItemInSlot() == item){
                int amountToDecrease;
                if(amountToRemove >= inventorySlots[i].getCurrentAmountInSlot()){
                    amountToDecrease = inventorySlots[i].getCurrentAmountInSlot();
                } else {
                    amountToDecrease = amountToRemove;
                }
                inventorySlots[i].decreaseCurrentAmountInSlot(amountToDecrease);
                amountToRemove -= amountToDecrease;
                if(inventorySlots[i].getCurrentAmountInSlot() == 0){
                    inventorySlots[i].setItemInSlot(null);
                }
            }
            if(amountToRemove == 0){
                break;
            }
        }
        updateInventoryInterface();
    }

    public int getAmountOfSpecificItem(string itemName){
        int totalAmount = 0;
        foreach(InventorySlot inventorySlot in inventorySlots){
            if(inventorySlot.getItemInSlot() != null){
                if(inventorySlot.getItemInSlot().Equals(itemName)){
                    totalAmount += inventorySlot.getCurrentAmountInSlot();
                }
            }
        }
        return totalAmount; 
    }

    public bool checkIfListOfItemsAreInInventory(Dictionary<string, int> checkList){
        string trueOrFalse = "";
        if(checkList != null){
            foreach(var item in checkList){
                int itemAmountFound = 0;

                for(int i = 0; i < inventoryCapacity; i++){
                    if(item.Key == inventorySlots[i].getItemInSlot()){
                        itemAmountFound += inventorySlots[i].getCurrentAmountInSlot();
                    }
                }
                if(itemAmountFound >= item.Value){
                    trueOrFalse += "true";
                } else {
                    trueOrFalse += "false";
                }
            }
        } else {
            //Debug.Log("the method needs a list of more than 0 items");
            return false;
        }
        if(trueOrFalse.Contains("false")){
            return false;
        } else {
            return true;
        }
    }


    public List<InventorySlot> getFreeInventorySpaceForItem(string itemName, int amount){
        List<InventorySlot> freeInventorySlots = new List<InventorySlot>();

        foreach(InventorySlot inventorySlot in inventorySlots){
            if(inventorySlot.getCurrentAmountInSlot() == 0 || ((inventorySlot.getCurrentAmountInSlot() + amount) <= 99 && inventorySlot.getItemInSlot().Equals(itemName))){
                freeInventorySlots.Add(inventorySlot);
            }
        }

        return freeInventorySlots;
    }

    public int getFreeInventorySpaceForSpecificItem(string itemName){
        int freeSpace = 0;
        foreach(InventorySlot inventorySlot in inventorySlots){
            if(inventorySlot.getCurrentAmountInSlot() == 0){
                freeSpace += 99;
            }
            if(inventorySlot.getItemInSlot() != null)
                if(inventorySlot.getItemInSlot().Equals(itemName)){
                    freeSpace += 99 - inventorySlot.getCurrentAmountInSlot();
                }
        }
        return freeSpace;
    }

    public bool checkIfInventoryHasSpaceForItem(string itemName, int amount){
        int totalFreeSpace = 0;

        foreach(InventorySlot inventorySlot in inventorySlots){
            if(inventorySlot.getCurrentAmountInSlot() == 0){
                totalFreeSpace += 99;
                continue;
            }

            if(inventorySlot.getItemInSlot() == itemName && inventorySlot.getCurrentAmountInSlot() + amount <= 99){
                totalFreeSpace += 99 - inventorySlot.getCurrentAmountInSlot();
            }
        }

        if(totalFreeSpace >= amount){
            return true;
        } else {
            return false;
        }
    }

    public bool sendItemFromThisToOther(Inventory inv2, string itemName, int amount){
        if(inv2.checkIfInventoryHasSpaceForItem(itemName, amount)){
            if(this.getAmountOfSpecificItem(itemName) != 0){
                if(this.getAmountOfSpecificItem(itemName) >= amount){
                    inv2.addItemToInventory(itemName, amount);
                    this.removeItemFromInventory(itemName, amount);
                    return true;
                } else {
                    inv2.addItemToInventory(itemName, this.getAmountOfSpecificItem(itemName));
                    this.removeItemFromInventory(itemName, this.getAmountOfSpecificItem(itemName));
                    Debug.Log(itemName + " " + this.getAmountOfSpecificItem(itemName) + " " + this.transform.name + " " + inv2.transform.name);
                    Debug.Log("Didnt have the full amount to transfer, so sent what the inventory had of that item");
                }
            } else {
                Debug.Log("Inventory didnt have " + itemName + ". No transfer was made.");
            }
        } else {
            gameManager.getMessageLogText().addMessageToLog("Could not transfer item to inventory, since its not enough space");
        }
        return false;
    }

    public List<InventorySlot> getInventorySlots(){
        return inventorySlots;
    }

    public string getInventoryToString(){
        string inventoryString = "";
        foreach(InventorySlot inventorySlot in inventorySlots){
            inventoryString += inventorySlot.getCurrentAmountInSlot() + " " + inventorySlot.getItemInSlot();
        }
        return inventoryString;
    }

    public InventorySlot getInventorySlot(int index){
        return inventorySlots[index];
    }
    public void setInventoryCapacity(int number){
        inventoryCapacity = number;
        updateInventorySlots(number);
    }

    public void updateInventorySlots(int newCapacity){
        if(newCapacity != inventorySlots.Count){
            inventorySlots.Clear();
            for(int i = 0; i < inventoryCapacity; i++){
                inventorySlots.Add(new InventorySlot(99, null));
            }
        }
    }

    public void updateInventoryInterface(){
        if(this.transform.tag == "player" && GameObject.FindGameObjectWithTag("InventoryMenuUI").transform.Find("Background").gameObject.activeSelf){
            gameManager.GetUI().updateMainInventory();
            //Debug.Log("updating main inventory");
        }
        if(GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Inventory").gameObject.activeSelf){
            gameManager.GetUI().updateBuildingInventory();
            //Debug.Log("updating building inventory");
        }
        if(GameObject.FindGameObjectWithTag("UnfinishedBuildingMenuUI").transform.Find("Background").gameObject.activeSelf){
            gameManager.GetUI().openUnfinishedBuilding();
            //Debug.Log("updating unfinished building inventory");
        }
        if(GameObject.FindGameObjectWithTag("TownMenuUI").transform.Find("Background/Inventory").gameObject.activeSelf){
            gameManager.GetUI().updateTownInventory();
            //Debug.Log("updating town inventory");
        }
        if(gameManager.getCitizenCatalog().getSelectedCitizen()){
            if(gameManager.getCitizenCatalog().getSelectedCitizen().Equals(this.transform.GetComponent<Citizen>()) && GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background").gameObject.activeSelf){
                gameManager.GetUI().updateCitizenInventory();
                //Debug.Log("updating citizen inventory");
            }
        }

        // UPDATING UNFINISHED BUILDING IF IT EXISTS
        if(this.transform.GetComponent<UnfinishedBuilding>()){
            this.transform.GetComponent<UnfinishedBuilding>().updateMaterialProgress();
        }
    }
}
