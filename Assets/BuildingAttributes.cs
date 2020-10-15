using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttributes : MonoBehaviour
{
    bool isOwnedByPlayer;

    //public List<NPC> npcsAssignedHere;
    public Dictionary<string, int> itemsStoredInBuilding;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForBuildingProduction;
    public int houseValue;
    public int buildingID;
    public float positionX;
    public float positionY;
    public float positionZ;
    void Start()
    {
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
