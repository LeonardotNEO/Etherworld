using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttributes : MonoBehaviour
{
    bool isOwnedByPlayer;

    //public List<NPC> npcsAssignedHere;
    public List<Item> itemsStoredInBuilding;
    public List<Item> itemsProducedInBuilding;
    public List<Item> itemsNeededForBuildingProduction;
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
    public List<Item> getItemsStoredInBuilding()
    {
        return itemsStoredInBuilding;
    }
    public string getItemsStoredInBuildingToString()
    {
        string itemsStoredInBuildingToString = "";
        foreach(var item in itemsStoredInBuilding){
            itemsStoredInBuildingToString += "ItemName: " + item.getName() + "\nItemAmount: " + item.getItemAmount();
        }
        if(itemsStoredInBuildingToString == ""){
            itemsStoredInBuildingToString = "None";
        }
        return itemsStoredInBuildingToString;
    }
    public List<Item> getItemsProducedInBuilding()
    {
        return itemsProducedInBuilding;
    }
    public string getItemsProducedInBuildingToString()
    {
        string itemsProducedInBuildingToString = "";
        foreach(var item in itemsProducedInBuilding){
            itemsProducedInBuildingToString += "ItemName: " + item.getName() + "\nItemAmount: " + item.getItemAmount();
        }
        if(itemsProducedInBuildingToString == ""){
            itemsProducedInBuildingToString = "None";
        }
        return itemsProducedInBuildingToString;
    }
    public List<Item> getItemsNeededForBuildingProduction()
    {
        return itemsNeededForBuildingProduction;
    }
    public string getItemsNeededForBuildingProductionToString()
    {
        string itemsNeededForBuildingProductionToString = "";
        foreach(var item in itemsNeededForBuildingProduction){
            itemsNeededForBuildingProductionToString += "ItemName: " + item.getName() + "\nItemAmount: " + item.getItemAmount();
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
    public void setItemsStoredInBuilding(List<Item> newItemsStoredInBuilding)
    {
        itemsStoredInBuilding = newItemsStoredInBuilding;
    }
    public void setItemsProducedInBuilding(List<Item> newItemsProduced)
    {
        itemsProducedInBuilding = newItemsProduced;
    }
    public void setItemsNeededForBuildingProduction(List<Item> newItemsNeededForProduction)
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
