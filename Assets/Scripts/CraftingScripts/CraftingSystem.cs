using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class CraftingSystem : MonoBehaviour
{
    GameManager gameManager;
    private Dictionary<string, int> itemsToRemoveFromInventory;
    public bool isCrafting;
    private GameObject currentlyCraftedBuilding;

    // BUILDING
    public bool collidingWithOtherObject;
    public bool craftedBuildingIsOutsideTown;

    // TOWN
    public bool townCollidingWithOtherTown;
    public bool allBuildingsInsideTownIsPlayerOwned;

    private Ray movementRay;
    private RaycastHit hit;
    private string craftingSavedTag;
    private string craftingSavedLayer;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // updating placingBuilding
        if(isCrafting){
            if(currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = true;
            } else if(!currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                currentlyCraftedBuilding.GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
            
            movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(movementRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            currentlyCraftedBuilding.transform.position = hit.point;

            if(currentlyCraftedBuilding.GetComponent<BuildingAttributes>()){
                collidingWithOtherObject = currentlyCraftedBuilding.GetComponent<BuildingAttributes>().getCollidingWithOtherObject();

                currentlyCraftedBuilding.GetComponent<BuildingAttributes>().buildingIndicator(true);
                gameManager.GetUI().getPlayerOwnedSelectedTown().townIndicator(true);

                if(gameManager.GetUI().getPlayerOwnedSelectedTown().checkIfPositionIsInsideTown(currentlyCraftedBuilding.transform.position)){
                    craftedBuildingIsOutsideTown = false;
                    gameManager.GetUI().getPlayerOwnedSelectedTown().townIndicatorMode(true);
                } else {
                    craftedBuildingIsOutsideTown = true;
                    gameManager.GetUI().getPlayerOwnedSelectedTown().townIndicatorMode(false);
                    currentlyCraftedBuilding.GetComponent<BuildingAttributes>().buildingIndicatorMode(false);
                }


                if(isCrafting && Input.GetMouseButtonDown(0) && !collidingWithOtherObject && !craftedBuildingIsOutsideTown){
                    if(currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                        currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = false;
                    } else if(!currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                        currentlyCraftedBuilding.GetComponentInChildren<BoxCollider>().isTrigger = false;
                    }

                    BuildingAttributes buildingAttributes = currentlyCraftedBuilding.GetComponent<BuildingAttributes>();

                    // STORAGE CAPACITY
                    if(buildingAttributes.gameObject.GetComponent<Inventory>()){
                        buildingAttributes.GetComponent<Inventory>().setInventoryCapacity(buildingAttributes.getStorageCapacity());
                        buildingAttributes.gameObject.GetComponent<Inventory>().instatiateInventory();
                    }

                    // PLAYER OWNED
                    buildingAttributes.setIsOwnedByPlayer(true);

                    // TOWN BUILDING IS A PART OF
                    buildingAttributes.setTownBuildingIsApartOf(gameManager.GetUI().getPlayerOwnedSelectedTown());

                    // POSITION
                    if(buildingAttributes.gameObject.GetComponent<BoxCollider>()){
                        buildingAttributes.setPositionX(currentlyCraftedBuilding.transform.GetComponent<BoxCollider>().bounds.center.x);
                        buildingAttributes.setPositionY(currentlyCraftedBuilding.transform.GetComponent<BoxCollider>().bounds.center.y);
                        buildingAttributes.setPositionZ(currentlyCraftedBuilding.transform.GetComponent<BoxCollider>().bounds.center.z);
                    } else {
                        buildingAttributes.setPositionX(buildingAttributes.transform.position.x);
                        buildingAttributes.setPositionY(buildingAttributes.transform.position.y);
                        buildingAttributes.setPositionZ(buildingAttributes.transform.position.z);
                    }
            

                    // TAGS AND LAYERS
                    currentlyCraftedBuilding.tag = craftingSavedTag;
                    buildingAttributes.setBuildingTag(craftingSavedTag);
                    currentlyCraftedBuilding.layer = LayerMask.NameToLayer(craftingSavedLayer);

                    // ADD BUILDING TO TOWN, ACTIVATE ONTRIGGER FOR TOWN
                    currentlyCraftedBuilding.SetActive(false);
                    currentlyCraftedBuilding.SetActive(true);

                    foreach(var item in itemsToRemoveFromInventory){
                        gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(item.Key, item.Value);
                    }
                    currentlyCraftedBuilding = null;
                    foreach(Town town in gameManager.getPlayerBehavior().getTownsOwned()){
                        town.townIndicator(false);
                    }
                    //gameManager.GetUI().getPlayerOwnedSelectedTown().townIndicator(false);
                    setIsCrafting(false);
                }
            }
            if(currentlyCraftedBuilding){
                if(currentlyCraftedBuilding.GetComponent<Town>()){
                    townCollidingWithOtherTown = currentlyCraftedBuilding.GetComponent<Town>().getCollidingWithOtherTown();
                    allBuildingsInsideTownIsPlayerOwned = currentlyCraftedBuilding.GetComponent<Town>().checkIfAllBuildingsInsideTownIsPlayerOwned();

                    currentlyCraftedBuilding.GetComponent<Town>().townIndicator(true);
                    if(!allBuildingsInsideTownIsPlayerOwned || townCollidingWithOtherTown){
                        currentlyCraftedBuilding.GetComponent<Town>().townIndicatorMode(false);
                    } else {
                        currentlyCraftedBuilding.GetComponent<Town>().townIndicatorMode(true);
                    }

                    if(isCrafting && Input.GetMouseButtonDown(0) && !townCollidingWithOtherTown && allBuildingsInsideTownIsPlayerOwned){
                        Town townCrafted = currentlyCraftedBuilding.GetComponent<Town>();
                        
                        // TAGS AND LAYERS
                        currentlyCraftedBuilding.tag = craftingSavedTag;
                        currentlyCraftedBuilding.layer = LayerMask.NameToLayer(craftingSavedLayer);

                        // ADD BUILDING TO TOWN, ACTIVATE ONTRIGGER FOR TOWN
                        currentlyCraftedBuilding.SetActive(false);
                        currentlyCraftedBuilding.SetActive(true);

                        // ADD TOWN TO PLAYERTOWNS
                        gameManager.getPlayerBehavior().addTownToOwnedTowns(townCrafted);

                        // NAME
                        townCrafted.setTownOwner(gameManager.getPlayerBehavior().getPlayerFirstName() + " " + gameManager.getPlayerBehavior().getPlayerLastName());

                        foreach(var item in itemsToRemoveFromInventory){
                            gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(item.Key, item.Value);
                        }
                        currentlyCraftedBuilding = null;
                        foreach(Town town in gameManager.getPlayerBehavior().getTownsOwned()){
                            town.townIndicator(false);
                        }
                        setIsCrafting(false);
                    }
                }
            }
        } 
        
        if(isCrafting && Input.GetKeyDown(KeyCode.Escape) || isCrafting && Input.GetMouseButtonDown(1)){
            setIsCrafting(false);
            foreach(Town town in gameManager.getPlayerBehavior().getTownsOwned()){
                town.townIndicator(false);
            }
            Destroy(currentlyCraftedBuilding);
        }
    }
    
    public void craftBuildingOnClick(){
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        string buildingName = EventSystem.current.currentSelectedGameObject.GetComponent<CraftingButton>().getBuildingName();
        Building buildingSelected = gameManager.getBuildingCatalog().getBuildingByName(buildingName);
        Inventory mainInventory = gameManager.getInventoryCatalog().getMainInventory();
        Dictionary<string, int> itemsNeededToCraft = buildingSelected.getCostToCraftBuilding();

        if(!gameManager.getCraftingSystem().getIsCrafting() && mainInventory.checkIfListOfItemsAreInInventory(itemsNeededToCraft)){
            GameObject craftedBuilding = Instantiate(buildingSelected.getBuildingPrefab(), new Vector3(0,0,0), transform.rotation);
            craftedBuilding.name = buildingSelected.getNameOfBuilding();
            gameManager.GetComponent<CraftingSystem>().setItemsToRemoveFromInventory(itemsNeededToCraft);
            gameManager.GetComponent<CraftingSystem>().setCraftedBuilding(craftedBuilding);
            gameManager.GetComponent<CraftingSystem>().setCraftingSavedTag(craftedBuilding.tag);
            gameManager.GetComponent<CraftingSystem>().setCraftingSavedLayer(LayerMask.LayerToName(craftedBuilding.layer));
            craftedBuilding.tag = "Placeholder";
            craftedBuilding.layer = LayerMask.NameToLayer("Placeholder");
            gameManager.GetComponent<CraftingSystem>().setIsCrafting(true);
        } else {
            gameManager.getMessageLogText().addMessageToLog("You don't have enough items to craft that building");
        }
    }

    public void rotateBuildingLeft(){
        if(currentlyCraftedBuilding){
            //currentlyCraftedBuilding.transform.Rotate(0,-100 * Time.deltaTime ,0, Space.Self);
            currentlyCraftedBuilding.transform.Rotate( 0,-22.5f, 0, Space.Self);
        }
    }
    public void rotateBuildingRight(){
        if(currentlyCraftedBuilding){
            //currentlyCraftedBuilding.transform.Rotate(0,100 * Time.deltaTime ,0, Space.Self);
            currentlyCraftedBuilding.transform.Rotate( 0, 22.5f, 0, Space.Self);
        }
    }
    public bool getIsCrafting(){
        return isCrafting;
    }
    public void setIsCrafting(bool value){
        isCrafting = value;
    }
    public GameObject getCraftedBuilding(){
        return currentlyCraftedBuilding;
    }
    public void setCraftedBuilding(GameObject set){
        currentlyCraftedBuilding = set;
    }
    public Dictionary<string, int> getItemsToRemoveFromInventory(){
        return itemsToRemoveFromInventory;
    }
    public void setItemsToRemoveFromInventory(Dictionary<string, int> items){
        itemsToRemoveFromInventory = items;
    }
    public bool getcollidingWithOtherObject(){
        return collidingWithOtherObject;
    }
    public void setCraftingSavedTag(string tag){
        craftingSavedTag = tag;
    }
    public void setCraftingSavedLayer(string layer){
        craftingSavedLayer = layer;
    }
    public GameObject getCurrentlyCraftedBuilding(){
        return currentlyCraftedBuilding;
    }
}
