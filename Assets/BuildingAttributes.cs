using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttributes : MonoBehaviour
{
    GameManager gameManager;
    BuildingsCatalog buildingsCatalog;
    public bool isOwnedByPlayer;
    //public List<NPC> npcsAssignedHere;
    public Dictionary<string, int> itemsStoredInBuilding;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForBuildingProduction;
    private int houseValue;
    public int buildingID;
    public float positionX;
    public float positionY;
    public float positionZ;
    public bool collidingWithOtherObject;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buildingsCatalog = gameManager.getBuildingCatalog();
        buildingID = gameManager.getAmountOfBuildingsInGame();
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
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

    void OnMouseOver()
    {
        GetComponentInChildren<Outline>().eraseRenderer = false;
    }

    void OnMouseExit()
    {
        GetComponentInChildren<Outline>().eraseRenderer = true;
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
}
