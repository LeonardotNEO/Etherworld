using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSystem : MonoBehaviour
{
    GameManager gameManager;
    private Dictionary<string, int> itemsToRemoveFromInventory;
    private bool isCrafting;
    private GameObject currentlyCraftedBuilding;
    private bool collidingWithOtherObject;
    public int craftingButtonsIDCounter;
    private Ray movementRay;
    private RaycastHit hit;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {

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

            if(isCrafting && Input.GetMouseButtonDown(0) && !currentlyCraftedBuilding.GetComponent<BuildingAttributes>().getCollidingWithOtherObject()){
                if(currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                    currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>().isTrigger = false;
                } else if(!currentlyCraftedBuilding.GetComponentInChildren<MeshCollider>()){
                    currentlyCraftedBuilding.GetComponentInChildren<BoxCollider>().isTrigger = false;
                }
                currentlyCraftedBuilding.GetComponent<BuildingAttributes>().setIsOwnedByPlayer(true);
                gameManager.getInventoryCatalog().getMainInventory().removeItemFromInventory(itemsToRemoveFromInventory);
                currentlyCraftedBuilding = null;
                setIsCrafting(false);
            }
        } 
        
        if(isCrafting && Input.GetKeyDown(KeyCode.Escape) || isCrafting && Input.GetMouseButtonDown(1)){
            setIsCrafting(false);
            Destroy(currentlyCraftedBuilding);
        }

        if(currentlyCraftedBuilding){
            if(currentlyCraftedBuilding.GetComponent<BuildingAttributes>().getCollidingWithOtherObject() && isCrafting){
                //gameManager.getMainCamera().GetComponentInChildren<OutlineEffect>().setLineColor0(Color.red);
            } else {
                //gameManager.getMainCamera().GetComponentInChildren<OutlineEffect>().setLineColor0(Color.green);
            }
        }

        if(currentlyCraftedBuilding){
            collidingWithOtherObject = currentlyCraftedBuilding.GetComponent<BuildingAttributes>().getCollidingWithOtherObject();
        }
    }
    
    public void craftBuildingOnClick(){
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        int thisButtonID = EventSystem.current.currentSelectedGameObject.GetComponent<CraftingButton>().getButtonId();
        Building buildingSelected = gameManager.getBuildingCatalog().getBuilding(thisButtonID);
        Inventory mainInventory = gameManager.getInventoryCatalog().getMainInventory();
        Dictionary<string, int> itemsNeededToCraft = buildingSelected.getCostToCraftBuilding();

        if(!gameManager.getCraftingSystem().getIsCrafting() && mainInventory.checkIfListOfItemsAreInInventory(itemsNeededToCraft)){
            GameObject craftedBuilding = Instantiate(buildingSelected.getBuildingPrefab(), new Vector3(0,0,0), transform.rotation);
            craftedBuilding.name = buildingSelected.getNameOfBuilding();
            gameManager.GetComponent<CraftingSystem>().setItemsToRemoveFromInventory(itemsNeededToCraft);
            gameManager.GetComponent<CraftingSystem>().setCraftedBuilding(craftedBuilding);
            gameManager.GetComponent<CraftingSystem>().setIsCrafting(true);
        } else {
            gameManager.getMessageLogText().addMessageToLog("You don't have enough items to craft that building");
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
    public bool getcollidingWithOtherObject(){
        return collidingWithOtherObject;
    }
    public int getCraftingButtonIDCounter(){
        return craftingButtonsIDCounter;
    }
    public void updateCraftingButtonIDCounter(){
        craftingButtonsIDCounter++;
    }
}
