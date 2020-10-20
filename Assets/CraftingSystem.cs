using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSystem : MonoBehaviour
{
    
    Inventory mainInventory;
    GameManager gameManager;

    Dictionary<string, int> itemsToRemoveFromInventory;
    public bool isCrafting;
    public GameObject currentlyCraftedBuilding;
    
    public Ray movementRay;
    public RaycastHit hit;

    void Awake()
    {
        isCrafting = false;
    }

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mainInventory = gameManager.getInventoryCatalog().getMainInventory(); //Find main inventory in inventorycatalog, nr 0
    }


    void Update()
    {
        // updating placingBuilding
        if(isCrafting){
            currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = true;
            movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(movementRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            currentlyCraftedBuilding.transform.position = hit.point;

            if(isCrafting && Input.GetMouseButtonDown(0) && !gameManager.getcollidingWithOtherObject()){
                currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = false;
                mainInventory.removeItemFromInventory(itemsToRemoveFromInventory);
                gameManager.increaseAmountOfBuildingsInGame(1);
                currentlyCraftedBuilding = null;
                setIsCrafting(false);
            }
        } 
        if(isCrafting && Input.GetKeyDown(KeyCode.Escape) || isCrafting && Input.GetMouseButtonDown(1)){
            setIsCrafting(false);
            Destroy(currentlyCraftedBuilding);
        }
        if(gameManager.getcollidingWithOtherObject() && isCrafting){
            //gameManager.getMainCamera().GetComponentInChildren<OutlineEffect>().setLineColor0(Color.red);
        } else {
            //gameManager.getMainCamera().GetComponentInChildren<OutlineEffect>().setLineColor0(Color.green);
        }
    }

    public void craftBuilding(){
        if(!GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().getIsCrafting()){
            int thisButtonID = EventSystem.current.currentSelectedGameObject.GetComponent<CraftingButton>().getButtonId();
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            BuildingsCatalog buildingsCatalog = gameManager.getBuildingCatalog();
            Inventory mainInventory = gameManager.getInventoryCatalog().getMainInventory();
    
            Building buildingSelected = buildingsCatalog.getBuilding(thisButtonID);
            Dictionary<string, int> itemsNeededToCraft = buildingSelected.getCostToCraftBuilding();
            Dictionary<string, int> itemsInInventory = mainInventory.getInventory();
            
            string enoughResources = "";
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
                
                GameObject craftedBuilding = Instantiate(buildingSelected.getBuildingPrefab(), new Vector3(0,0,0), transform.rotation);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().setItemsToRemoveFromInventory(itemsNeededToCraft);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().setCraftedBuilding(craftedBuilding);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().setIsCrafting(true);
            } 
        }
    }

    public void rotateBuildingLeft(){
        if(currentlyCraftedBuilding){
            currentlyCraftedBuilding.transform.Rotate(0,-100 * Time.deltaTime ,0, Space.Self);
        }
    }
    public void rotateBuildingRight(){
        if(currentlyCraftedBuilding){
            currentlyCraftedBuilding.transform.Rotate(0,100 * Time.deltaTime ,0, Space.Self);
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
}
