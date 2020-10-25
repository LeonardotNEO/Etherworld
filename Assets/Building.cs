using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    private Dictionary<string, int> costToCraftBuilding;
    private Dictionary<string, int> buildingUpkeep;
    private Dictionary<string, int> buildingProduction;
    private Dictionary<string, int> neededForProduction;
    private GameObject buildingPrefab;
    public Sprite buildingSprite;
    private int storageCapacity;
    private int buildingID;
    private string nameOfBuilding;
    private string buildingDescription;

    //CONSTRUCTOR
    public Building(Dictionary<string, int> costToCraftBuilding, Dictionary<string, int> buildingUpkeep, Dictionary<string, int> buildingProduction, Dictionary<string, int> neededForProduction, GameObject  buildingPrefab, Sprite buildingSprite, int storageCapacity, string nameOfBuilding, string buildingDescription){
        this.costToCraftBuilding = costToCraftBuilding;
        this.buildingUpkeep = buildingUpkeep;
        this.buildingProduction = buildingProduction;
        this.neededForProduction = neededForProduction;
        this.storageCapacity = storageCapacity;
        this.buildingPrefab = buildingPrefab;
        this.buildingSprite = buildingSprite;
        this.nameOfBuilding = nameOfBuilding;
        this.buildingDescription = buildingDescription;
        this.buildingID++;
    }

    //GETTERS
    public Dictionary<string, int> getCostToCraftBuilding()
    {
        return costToCraftBuilding;
    }
    public Dictionary<string, int> getBuildingUpKeep()
    {
        return buildingUpkeep;
    }
    public Dictionary<string, int> getBuildingProduction()
    {
        return buildingProduction;
    }
    public Dictionary<string, int> getNeededForProduction()
    {
        return neededForProduction;
    }
    public string getCostToCraftBuildingToString()
    {
        string costToCraftBuildingToString = "";
        foreach(var item in costToCraftBuilding){
            costToCraftBuildingToString += "ItemName: " + item.Key + "\nItemAmount: " + item.Value + "\n";
        }
        if(costToCraftBuildingToString == ""){
            costToCraftBuildingToString = "None";
        }
        return costToCraftBuildingToString;
    }  
    public GameObject getBuildingPrefab(){
        return buildingPrefab;
    }
    public Sprite getBuildingSprite(){
        return buildingSprite;
    }
    public string getNameOfBuilding()
    {
        return nameOfBuilding;
    }
    public string getDescriptionOfBulding(){
        return buildingDescription;
    }
    public int getBuildingID(){
        return buildingID;
    }
    public int getStorageCapacity(){
        return storageCapacity;
    }


    //SETTERS
    public void setCostToCraftBuilding(Dictionary<string, int> newCostToCraftBuilding)
    {
        costToCraftBuilding = newCostToCraftBuilding;
    }
    public void setBuildingPrefab(GameObject newBuildingPrefab){
        buildingPrefab = newBuildingPrefab;
    }
    public void setNameOfBuilding(string newNameOfBuilding){
        nameOfBuilding = newNameOfBuilding;
    }
    public void setBuildingDescription(string newBuildingDescription){
        buildingDescription = newBuildingDescription;
    }
}
