using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    private Dictionary<string, int> costToCraftBuilding;
    private GameObject buildingPrefab;
    public Sprite buildingSprite;
    private int maxItemsStoredInBuilding;
    private string nameOfBuilding;
    private string buildingDescription;

    //CONSTRUCTOR
    public Building(Dictionary<string, int> costToCraftBuilding, GameObject  buildingPrefab, Sprite buildingSprite, int maxItemssStoredInBuilding, string nameOfBuilding, string buildingDescription){
        this.costToCraftBuilding = costToCraftBuilding;
        this.buildingPrefab = buildingPrefab;
        this.buildingSprite = buildingSprite;
        this.maxItemsStoredInBuilding = maxItemssStoredInBuilding;
        this.nameOfBuilding = nameOfBuilding;
        this.buildingDescription = buildingDescription;
    }

    //GETTERS
    public Dictionary<string, int> getCostToCraftBuilding()
    {
        return costToCraftBuilding;
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
