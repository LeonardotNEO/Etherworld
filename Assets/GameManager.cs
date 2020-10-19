using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // MAIN CAMERA //
    GameObject mainCamera;

    // PLAYER BEHAVIOR //
    PlayerBehavior playerBehavior;

    // CRAFTING SYSTEM //
    CraftingSystem craftingSystem;
    public bool isCrafting;
    public int amountOfBuildingsInGame;
    public int craftingButtonsID;
    public Dictionary<string, int> itemsToRemoveFromInventory;
    public GameObject currentlyCraftedBuilding;
    public bool collidingWithOtherObject;
    
 
    // INVENTORY SYSTEM //
    InventorySystem inventorySystem;

    // UI //
    UI UI;
    public bool inventoryOpen;
    public bool craftingOpen;

    // CATALOGS //
    private BuildingsCatalog buildingCatalog;
    private ItemCatalog itemCatalog;
    private InventoryCatalog inventoryCatalog;

    // MAININVENTORY //
    Inventory mainInventory;
    

    void Awake()
    {
        // CATALOGS //
        buildingCatalog = GetComponent<BuildingsCatalog>();
        itemCatalog = GetComponent<ItemCatalog>();
        inventoryCatalog = GetComponent<InventoryCatalog>();

        // MAIN CAMERA //
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera2");

        // CRAFTING SYSTEM
        craftingSystem = GetComponent<CraftingSystem>();
    }


    void Start()
    {
        
    }

    void Update()
    {

        // PLAYER BEHAVIOR //
        playerBehavior = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerBehavior>();

        // CRAFTING SYSTEM //
        craftingSystem = GetComponent<CraftingSystem>();
        isCrafting = craftingSystem.getIsCrafting();
        currentlyCraftedBuilding = craftingSystem.getCraftedBuilding();
        itemsToRemoveFromInventory = craftingSystem.getItemsToRemoveFromInventory();
        if(currentlyCraftedBuilding){
            collidingWithOtherObject = currentlyCraftedBuilding.GetComponent<BuildingAttributes>().getCollidingWithOtherObject();
        }

        // INVENTORY SYSTEM //
        inventorySystem = GetComponent<InventorySystem>();

        // UI //
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        inventoryOpen = UI.getInventoryOpen();
        craftingOpen = UI.getCraftingOpen();

        //MAININVENTORY //
        mainInventory = inventoryCatalog.getMainInventory();

        // INPUTS //
        if(Input.GetKeyDown("1")){
            Debug.Log(buildingCatalog.getBuildingsCatalogToString());
        }
        if(Input.GetKeyDown("2")){
            Debug.Log(inventoryCatalog.inventoryCatalogToString());
        }
        if(Input.GetKeyDown("3")){
            Debug.Log(itemCatalog.itemCatalogToString());
        }
        if(Input.GetKeyDown("4")){
            mainInventory.removeItemFromInventory(new Dictionary<string, int>{{"WoodPile", 5}});
        }
        if(Input.GetKeyDown("5")){
            mainInventory.addItemToInventory(new Dictionary<string, int>{{"WoodPile", 1000}, {"StonePile", 1000}});
        }
        if(Input.GetKeyDown("i") && inventoryOpen == false){
            UI.setInventory(true);
            UI.setCrafting(false);
        } else if(Input.GetKeyDown("i") && inventoryOpen == true){
            UI.setInventory(false);
        }
        if(Input.GetKeyDown("tab") && craftingOpen == false){
            UI.setCrafting(true);
            UI.setInventory(false);
        } else if(Input.GetKeyDown("tab") && craftingOpen == true) {
            UI.setCrafting(false);
        }
        if(isCrafting){
            if(Input.GetKey("q")){
                craftingSystem.rotateBuildingLeft();
            }
            if(Input.GetKey("e")){
                craftingSystem.rotateBuildingRight();
            }
        }
        if(Input.GetKeyDown("g")){
            inventorySystem.spawnNPC();
        }
        if(Input.GetKeyDown("h")){
            RaycastHit mouseButtonPressed;
            Ray movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movementRay, out mouseButtonPressed, Mathf.Infinity, LayerMask.GetMask("Ground"))){
                float y = 0;
                for(int i = 0; i < 10; i++){
                    Instantiate(itemCatalog.getItemByIndex(0).getItemPrefab(), new Vector3(mouseButtonPressed.point.x, mouseButtonPressed.point.y, mouseButtonPressed.point.z + y), mouseButtonPressed.transform.rotation);
                    y += 1.5f;
                }
            }
        }
    }

    // CATALOGS //
    public BuildingsCatalog getBuildingCatalog(){
        return buildingCatalog;
    }
    public ItemCatalog getItemCatalog(){
        return itemCatalog;
    }
    public InventoryCatalog getInventoryCatalog(){
        return inventoryCatalog;
    }
    public PlayerBehavior getPlayerBehavior(){
        return playerBehavior;
    }
    public InventorySystem getInventorySystem(){
        return inventorySystem;
    }
    public CraftingSystem getCraftingSystem(){
        return craftingSystem;
    }

    // CRAFTING SYSTEM //
    public int getAmountOfBuildingsInGame(){
        return amountOfBuildingsInGame;
    }
    public bool getPlacingBuilding(){
        return isCrafting;
    }
    public void increaseAmountOfBuildingsInGame(int amount){
        amountOfBuildingsInGame += amount;
    }
    public void decreaseAmountOfBuildingsInGame(int amount){
        amountOfBuildingsInGame -= amount;
    }
    public int getCraftingButtonID(){
        return craftingButtonsID;
    }
    public void newCraftingButtonID(){
        craftingButtonsID++;
    }
    public bool getcollidingWithOtherObject(){
        return collidingWithOtherObject;
    }
    public GameObject getMainCamera(){
        return mainCamera;
    }
}
