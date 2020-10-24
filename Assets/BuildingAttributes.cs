using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingAttributes : MonoBehaviour
{
    GameManager gameManager;
    BuildingsCatalog buildingsCatalog;
    GameObject buildingMenuOwned;
    GameObject buildingMenuNotOwned;
    GameObject thisBuilding;
    public bool isOwnedByPlayer;
    //public List<NPC> npcsAssignedHere;
    public string buildingTag;
    public Dictionary<string, int> itemsStoredInBuilding;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForBuildingProduction;
    private int houseValue;
    public int buildingID;
    public string buildingName;
    public float positionX;
    public float positionY;
    public float positionZ;
    public bool collidingWithOtherObject;
    public bool buildingUIOpen;
    void Start()
    {
        buildingMenuOwned = GameObject.FindGameObjectWithTag("BuildingMenuOwnedUI");
        buildingMenuNotOwned = GameObject.FindGameObjectWithTag("BuildingMenuNotOwnedUI");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buildingsCatalog = gameManager.getBuildingCatalog();

        gameManager.increaseAmountOfBuildingsInGame(1);
        buildingID = gameManager.getAmountOfBuildingsInGame();

        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;

        buildingTag = gameObject.tag;
        // ALLE OBJEKTVARIABLER MÅ VÆRE FERDIG STADFESTET PÅ START!!!!!!!!!!!!
        buildingName = buildingsCatalog.getBuildingByName(transform.name).getNameOfBuilding();

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
    }
    void OnTriggerExit(Collider other)
    {
        setCollidingWithOtherObject(false);
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
        closeBuildingUI();
    }

    //GETTERS
    public Dictionary<string, int> getItemsStoredInBuilding()
    {
        return itemsStoredInBuilding;
    }
    public string getItemsStoredInBuildingToString()
    {
        string itemsStoredInBuildingToString = "";
        foreach(var item in itemsStoredInBuilding){
            itemsStoredInBuildingToString += "ItemName: " + item.Key + "\nItemAmount: " + item.Value;
        }
        if(itemsStoredInBuildingToString == ""){
            itemsStoredInBuildingToString = "None";
        }
        return itemsStoredInBuildingToString;
    }
    public Dictionary<string, int> getItemsProducedInBuilding()
    {
        return itemsProducedInBuilding;
    }
    public string getItemsProducedInBuildingToString()
    {
        string itemsProducedInBuildingToString = "";
        foreach(var item in itemsProducedInBuilding){
            itemsProducedInBuildingToString += "ItemName: " + item.Key + "\nItemAmount: " + item.Value;
        }
        if(itemsProducedInBuildingToString == ""){
            itemsProducedInBuildingToString = "None";
        }
        return itemsProducedInBuildingToString;
    }
    public Dictionary<string, int> getItemsNeededForBuildingProduction()
    {
        return itemsNeededForBuildingProduction;
    }
    public string getItemsNeededForBuildingProductionToString()
    {
        string itemsNeededForBuildingProductionToString = "";
        foreach(var item in itemsNeededForBuildingProduction){
            itemsNeededForBuildingProductionToString += "ItemName: " + item.Key + "\nItemAmount: " + item.Value;
        }
        if(itemsNeededForBuildingProductionToString == ""){
            itemsNeededForBuildingProductionToString = "None";
        }
        return itemsNeededForBuildingProductionToString;
    }
    public int getBuildingID()
    {
        return buildingID;
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
        itemsNeededForBuildingProduction = newItemsNeededForProduction;
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
    public bool getIsOwnedByPlayer(){
        return isOwnedByPlayer;
    }
    public void setIsOwnedByPlayer(bool value){
        isOwnedByPlayer = value;
    }
    public void setBuildingName(string name){
        buildingName = name;
    }
    public GameObject getThisBuilding(){
        return thisBuilding;
    }

    public void openBuildingUI(){
        buildingUIOpen = true;
        gameManager.setBuildingLastClicked(thisBuilding);
        buildingMenuOwned.transform.Find("Background").Find("Headline").GetComponent<Text>().text = buildingName;
        buildingMenuNotOwned.transform.Find("Background").Find("Headline").GetComponent<Text>().text = buildingName;

        if(!gameManager.getIsMouseOverUI() && !gameManager.getIsCrafting()){
            if(isOwnedByPlayer){
                buildingMenuOwned.GetComponent<Canvas>().enabled = true;
                buildingMenuNotOwned.GetComponent<Canvas>().enabled = false;


                buildingMenuOwned.transform.Find("Background").position = new Vector3(Input.mousePosition.x + buildingMenuOwned.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.x/2, Input.mousePosition.y + buildingMenuOwned.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.y/2, 0);
                buildingMenuOwned.transform.Find("Background").Find("VisitButton").Find("VisitText").GetComponentInChildren<Text>().text = "Visit " + buildingName;
                buildingMenuOwned.transform.Find("Background").Find("StatsButton").Find("StatsText").GetComponentInChildren<Text>().text = buildingName + " Stats";
            } else {
                buildingMenuOwned.GetComponent<Canvas>().enabled = false;
                buildingMenuNotOwned.GetComponent<Canvas>().enabled = true;

                buildingMenuNotOwned.transform.Find("Background").position = new Vector3(Input.mousePosition.x + buildingMenuNotOwned.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.x/2, Input.mousePosition.y + buildingMenuNotOwned.transform.Find("Background").GetComponent<RectTransform>().sizeDelta.y/2, 0);
                buildingMenuNotOwned.transform.Find("Background").Find("VisitButton").Find("VisitText").GetComponentInChildren<Text>().text = "Visit " + buildingName;
            }
        }
    }

    public void closeBuildingUI(){
        buildingUIOpen = false;
        GameObject buildingMenuOwned = GameObject.FindGameObjectWithTag("BuildingMenuOwnedUI");
        GameObject buildingMenuNotOwned = GameObject.FindGameObjectWithTag("BuildingMenuNotOwnedUI");

        buildingMenuOwned.GetComponent<Canvas>().enabled = false;
        buildingMenuNotOwned.GetComponent<Canvas>().enabled = false;
    }
}
