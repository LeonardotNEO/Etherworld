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
            if(currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = true;
            } else if(!currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                currentlyCraftedBuilding.GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
            
            movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(movementRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            currentlyCraftedBuilding.transform.position = hit.point;

            if(isCrafting && Input.GetMouseButtonDown(0) && !gameManager.getcollidingWithOtherObject()){
                if(currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                    currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = false;
                } else if(!currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                    currentlyCraftedBuilding.GetComponentInChildren<BoxCollider>().isTrigger = false;
                }
                currentlyCraftedBuilding.GetComponent<BuildingAttributes>().setIsOwnedByPlayer(true);
                mainInventory.removeItemFromInventory(itemsToRemoveFromInventory);
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

            if(mainInventory.checkIfListOfItemsAreInInventory(itemsNeededToCraft)){
                GameObject craftedBuilding = Instantiate(buildingSelected.getBuildingPrefab(), new Vector3(0,0,0), transform.rotation);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().setItemsToRemoveFromInventory(itemsNeededToCraft);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().setCraftedBuilding(craftedBuilding);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<CraftingSystem>().setIsCrafting(true);
            } else {
                gameManager.getMessageLogBar().addMessageToLog("You don't have enough items to craft that building");
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
