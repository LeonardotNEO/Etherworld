using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class UnfinishedBuilding : MonoBehaviour
{
    GameManager gameManager;
    public Inventory inventory;
    public BuildingAttributes building;
    public int currentBuilderProgress;
    public int maxBuilderProgress;
    public int currentMaterialProgress;
    public int maxMaterialProgress;
    public Dictionary<string, int> itemsNeededToCraft;
    public bool allMaterialsInBuilding;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        inventory = GetComponent<Inventory>();
    }

    void Start()
    {
        instantiateUnfinishedBuilding();
    }

    void Update()
    {
        if(Input.GetKeyDown(".")){
            increaseBuildingProgress(5);
            gameManager.GetUI().openUnfinishedBuilding();
        }
    }

    public void instantiateUnfinishedBuilding(){
        //yield return new WaitForSeconds(0.5f);

        // ITEMSNEEDEDTOCRAFT, MAXBUILDERPROGRESS, MAXMATERIALPROGRESS
        itemsNeededToCraft = gameManager.getBuildingCatalog().getBuildingByName(building.getBuildingName()).getCostToCraftBuilding();
        foreach(var item in itemsNeededToCraft){
            maxBuilderProgress += item.Value;
            maxMaterialProgress += item.Value;
        }

        // INDICATORS
        if(transform.Find("Object Indicator")){
            if(building.transform.GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>()){
                transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size = new Vector2(building.transform.GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.x, building.transform.GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.z);
                transform.Find("Object Indicator/Red indicator").GetComponent<SpriteRenderer>().size = new Vector2(building.transform.GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.x, building.transform.GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.z);
            } else {
                transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size = new Vector2(building.transform.GetComponentInChildren<NavMeshObstacle>().size.x * building.transform.GetComponentInChildren<NavMeshObstacle>().transform.localScale.x, building.transform.GetComponentInChildren<NavMeshObstacle>().size.z  * building.transform.GetComponentInChildren<NavMeshObstacle>().transform.localScale.z);
                transform.Find("Object Indicator/Red indicator").GetComponent<SpriteRenderer>().size = new Vector2(building.transform.GetComponentInChildren<NavMeshObstacle>().size.x * building.transform.GetComponentInChildren<NavMeshObstacle>().transform.localScale.x, building.transform.GetComponentInChildren<NavMeshObstacle>().size.z  * building.transform.GetComponentInChildren<NavMeshObstacle>().transform.localScale.z);
            }
            
            buildingIndicator(false);
        }

        // GROUND SPRITE
        transform.Find("Ground").GetComponent<SpriteRenderer>().size = new Vector2(transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size.x, transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size.y);


        // BOX COLLIDER
        GetComponent<BoxCollider>().size = new Vector3(transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size.x, this.transform.position.y, transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size.y);
    }

    void OnMouseOver()
    {
        buildingIndicator(true);
        buildingIndicatorMode(true);

        if(!gameManager.GetUI().getIsMouseOverUI() && Input.GetMouseButtonDown(1)){
            gameManager.getBuildingCatalog().setUnfinishedBuildingSelected(this);
            gameManager.GetUI().openUnfinishedBuilding();
        }
    }

    void OnMouseExit()
    {
        buildingIndicator(false);
        buildingIndicatorMode(true);
    }

    void OnDestroy()
    {
        if(building){
            building.transform.gameObject.SetActive(true);
        }
        gameManager.GetUI().closeUnfinishedBuilding();
    }

    public void buildingIndicator(bool val){
        if(transform.Find("Object Indicator")){
            transform.Find("Object Indicator").transform.gameObject.SetActive(val);
        }
    }
    public void buildingIndicatorMode(bool val){
        if(transform.Find("Object Indicator")){
            if(val == false){
                transform.Find("Object Indicator/Red indicator").transform.gameObject.SetActive(true);
                transform.Find("Object Indicator/Green indicator").transform.gameObject.SetActive(false);
            } else {
                transform.Find("Object Indicator/Red indicator").transform.gameObject.SetActive(false);
                transform.Find("Object Indicator/Green indicator").transform.gameObject.SetActive(true);
            }
        }
    }

    public void setBuilding(BuildingAttributes building){
        this.building = building;
    }
    public BuildingAttributes getBuilding(){
        return building;
    }

    public void updateMaterialProgress(){
        currentMaterialProgress = 0;

        foreach(var item in itemsNeededToCraft){
            foreach(InventorySlot inventorySlot in inventory.getInventorySlots()){
                if(item.Key.Equals(inventorySlot.getItemInSlot())){
                    currentMaterialProgress += inventorySlot.getCurrentAmountInSlot();
                }
            }
        }   
        checkMaterialProgress();
    }

    public void checkMaterialProgress(){
        if(currentMaterialProgress >= maxMaterialProgress){
            allMaterialsInBuilding = true;
        }
    }

    public void increaseBuildingProgress(int amount){
        currentBuilderProgress += amount;
        checkBuilderProgress();
    }

    public void checkBuilderProgress(){
        if(currentBuilderProgress >= maxBuilderProgress){
            Destroy(this.transform.gameObject);
        }
    }

    public Inventory getInventory(){
        return inventory;
    }

    public Dictionary<string, int> getItemsNeededToCraft(){
        return itemsNeededToCraft;
    }

    public int getBuilderProgress(){
        return currentBuilderProgress;
    }
    public int getBuilderMaxProgress(){
        return maxBuilderProgress;
    }
    public int getMaterialProgress(){
        return currentMaterialProgress;
    }
    public int getMaterialMaxProgress(){
        return maxMaterialProgress;
    }
}
