using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingAttributes : MonoBehaviour
{
    GameManager gameManager;
    BuildingsCatalog buildingsCatalog;
    GameObject buildingMenu;
    GameObject thisBuilding;

    public bool isOwnedByPlayer;
    //public List<NPC> npcsAssignedHere;
    public string buildingTag;
    private int buildingValue;
    public int buildingID;
    public string buildingName;
    public string buildingDescription;
    public int storageCapacity;

    public Dictionary<string, int> buildingUpKeep;
    public Dictionary<string, int> itemsStoredInBuilding;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForProduction;

    public bool playerInBoundsBuilding;
    public float positionX;
    public float positionY;
    public float positionZ;
    public bool collidingWithOtherObject;
    public bool buildingUIOpen;
    void Start()
    {
        buildingMenu = GameObject.FindGameObjectWithTag("BuildingMenuUI");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buildingsCatalog = gameManager.getBuildingCatalog();

        gameManager.increaseAmountOfBuildingsInGame(1);

        buildingTag = gameObject.tag;
        buildingID = gameManager.getAmountOfBuildingsInGame();
        buildingName = buildingsCatalog.getBuildingByName(transform.name).getNameOfBuilding();
        buildingDescription = buildingsCatalog.getBuildingByName(transform.name).getDescriptionOfBulding();
        storageCapacity = buildingsCatalog.getBuildingByName(transform.name).getStorageCapacity();

        buildingUpKeep = buildingsCatalog.getBuildingByName(transform.name).getBuildingUpKeep();
        itemsNeededForProduction = buildingsCatalog.getBuildingByName(transform.name).getNeededForProduction();
        itemsProducedInBuilding = buildingsCatalog.getBuildingByName(transform.name).getBuildingProduction();
        storageCapacity = buildingsCatalog.getBuildingByName(transform.name).getStorageCapacity();

        
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
        thisBuilding = this.gameObject;
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(
            other.gameObject.layer == 12  || /*Layer 12 is BUILDINGS*/
            other.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            other.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
        ){setCollidingWithOtherObject(true);} 
        if(
            other.gameObject.layer == 15      /*Layer 15 is PLAYER*/

        ){setPlayerInBoundsBuilding(true);} 
    }
    void OnTriggerExit(Collider other)
    {
        setCollidingWithOtherObject(false);
        setPlayerInBoundsBuilding(false);
    }

    void OnMouseDown()
    {
        openBuildingUI();
    }

    void OnMouseOver()
    {
        //GetComponentInChildren<Outline>().eraseRenderer = false;
    }

    void OnMouseExit()
    {
        if(!gameManager.getIsMouseOverUI()){
            closeBuildingUI();
        }
    }

    //GETTERS
    public Dictionary<string, int> getBuildingUpKeep(){
        return buildingUpKeep;
    }
    public Dictionary<string, int> getItemsStoredInBuilding()
    {
        return itemsStoredInBuilding;
    }
    public Dictionary<string, int> getItemsProducedInBuilding()
    {
        return itemsProducedInBuilding;
    }
    public Dictionary<string, int> getItemsNeededForBuildingProduction()
    {
        return itemsNeededForProduction;
    }
    public bool getIsOwnedByPlayer(){
        return isOwnedByPlayer;
    }
    public string getBuildingTag(){
        return buildingTag;
    }
    public int getBuildingValue(){
        return buildingValue;
    }
    public int getBuildingID()
    {
        return buildingID;
    }
    public string getBuildingName(){
        return buildingName;
    }
    public string getBuildingDescription(){
        return buildingDescription;
    }

    public int getStorageCapacity(){
        return storageCapacity;
    }
    public float getPositionX()
    {
        return positionX;
    }
    public float getPositionY()
    {
        return positionY;
    }
    public float getPositionZ()
    {
        return positionZ;
    }
    public bool getCollidingWithOtherObject(){
        return collidingWithOtherObject;
    }
    public GameObject getThisBuilding(){
        return thisBuilding;
    }
    public bool getPlayerInBoundsBuilding(){
        return playerInBoundsBuilding;
    }

    //SETTERS
    public void setItemsStoredInBuilding(Dictionary<string, int> newItemsStoredInBuilding)
    {
        itemsStoredInBuilding = newItemsStoredInBuilding;
    }
    public void setItemsProducedInBuilding(Dictionary<string, int> newItemsProduced)
    {
        itemsProducedInBuilding = newItemsProduced;
    }
    public void setItemsNeededForBuildingProduction(Dictionary<string, int> newItemsNeededForProduction)
    {
        itemsNeededForProduction = newItemsNeededForProduction;
    }
    public void setBuildingID(int newBuildingID){
        buildingID = newBuildingID;
    }
    public void setPositionX(int newPositionX){
        positionX = newPositionX;
    }
    public void setPositionY(int newPositionY){
        positionY = newPositionY;
    }
    public void setPositionZ(int newPositionZ){
        positionZ = newPositionZ;
    }
    public void setCollidingWithOtherObject(bool set){
        collidingWithOtherObject = set;
    }
    public void setIsOwnedByPlayer(bool value){
        isOwnedByPlayer = value;
    }
    public void setBuildingName(string name){
        buildingName = name;
    }
    public void setPlayerInBoundsBuilding(bool val){
        playerInBoundsBuilding = val;
    }


    public void openBuildingUI(){
        buildingUIOpen = true;
        gameManager.setBuildingLastClicked(thisBuilding);
        buildingMenu.transform.Find("Background").Find("Headline").GetComponent<Text>().text = buildingName;

        if(!gameManager.getIsMouseOverUI() && !gameManager.getIsCrafting()){

            // WHAT TO SHOW BY DEFAULT
            buildingMenu.transform.Find("Background").Find("Visit").gameObject.SetActive(true);
            buildingMenu.transform.Find("Background").Find("Stats").gameObject.SetActive(false);
            buildingMenu.transform.Find("Background").Find("Set Workers").gameObject.SetActive(false);
            buildingMenu.transform.Find("Background").Find("Open Inventory").gameObject.SetActive(false);
            buildingMenu.transform.Find("Background").Find("Fill Bucket").gameObject.SetActive(false);

            buildingMenu.GetComponent<Canvas>().enabled = true;
            buildingMenu.transform.Find("Background").position = new Vector3(Input.mousePosition.x + buildingMenu.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.x/2 - 10, Input.mousePosition.y + buildingMenu.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.y/2 - 10, 0);

            if(isOwnedByPlayer){
                if(buildingName.Equals("Waterwell")){
                    
                    buildingMenu.transform.Find("Background").Find("Stats").gameObject.SetActive(true);
                    buildingMenu.transform.Find("Background").Find("Fill Bucket").gameObject.SetActive(true);
                }
                if(buildingName.Equals("Furnace")){
                    buildingMenu.transform.Find("Background").Find("Stats").gameObject.SetActive(true);
                    buildingMenu.transform.Find("Background").Find("Set Workers").gameObject.SetActive(false);
                    buildingMenu.transform.Find("Background").Find("Open Inventory").gameObject.SetActive(false);
                }
                if(buildingName.Equals("Sawmill")){
                    buildingMenu.transform.Find("Background").Find("Stats").gameObject.SetActive(true);
                    buildingMenu.transform.Find("Background").Find("Set Workers").gameObject.SetActive(false);
                    buildingMenu.transform.Find("Background").Find("Open Inventory").gameObject.SetActive(false);
                }
                if(buildingName.Equals("Small Wood House")){
                    buildingMenu.transform.Find("Background").Find("Stats").gameObject.SetActive(true);
                }
            } else {
                // THIS IS BUILDING IS NOT OWNED BY PLAYER
                //buildingMenu.transform.Find("Background").Find("Visit").gameObject.SetActive(true);
            }
        }
    }
    public void closeBuildingUI(){
        buildingUIOpen = false;
        buildingMenu.GetComponent<Canvas>().enabled = false;
    }
}
