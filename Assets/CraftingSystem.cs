using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    
    Inventory mainInventory;
    BuildingsCatalog buildingsCatalog;
    ItemCatalog itemCatalog;
    PlayerBehavior playerBehavior;
    Building buildingSelected;
    Dictionary<string, int> itemsNeededToCraft;
    Dictionary<string, int> itemsInInventory;
    public int buttonNr;
    string enoughResources;
    bool lastValue;
    bool placingBuilding;
    GameObject craftedBuilding;
    Ray movementRay;
    RaycastHit hit;

    void Start()
    {
        mainInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryCatalog>().getInventoryCatalog()[0]; //Find main inventory in inventorycatalog, nr 0
        buildingsCatalog = GameObject.FindGameObjectWithTag("Crafting").GetComponent<BuildingsCatalog>();
        itemCatalog = GameObject.FindGameObjectWithTag("Crafting").GetComponent<ItemCatalog>();
        playerBehavior = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerBehavior>();
    }


    void Update()
    {
        if(placingBuilding && Input.GetKey("1")){
            craftedBuilding.transform.Rotate(0,-2,0, Space.Self);
        }
        if(placingBuilding && Input.GetKey("2")){
            craftedBuilding.transform.Rotate(0,2,0, Space.Self);
        }
        if(placingBuilding && !Input.GetKeyDown(KeyCode.Escape)){
            movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(movementRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            craftedBuilding.transform.position = hit.point;
            if(Input.GetMouseButtonDown(0)){
                mainInventory.removeItemFromInventory("WoodPile", 500);
                mainInventory.removeItemFromInventory("StonePile", 20);
                placingBuilding = false;
            }
        }
    }

    public void onButtonClick(){
        buildingSelected = buildingsCatalog.getBuilding(buttonNr);
        itemsNeededToCraft = buildingSelected.getCostToCraftBuilding();
        itemsInInventory = mainInventory.getInventory();
        enoughResources = "";
        bool lastValue = false;

        
        foreach(var itemC in itemsNeededToCraft){
            foreach(var itemI in itemsInInventory){
                if(itemC.Key == itemI.Key){
                    if(itemC.Value <= itemI.Value){
                        enoughResources += "true";
                        lastValue = true;
                        break;
                    }
                }
            }
            if(!lastValue){
                enoughResources += "false";
            }
            lastValue = false;
        }
        if(!enoughResources.Contains("false")){
            craftedBuilding = Instantiate(buildingSelected.getBuildingPrefab(), new Vector3(0,0,0), transform.rotation);
            placingBuilding = true;
        } 
    }
    public int getButtonNr(){
        return buttonNr;
    }

    public void setButtonNr(int number){
        this.buttonNr = number;
    }

    void craftBuildingSelected(){
        
        
    }
}
