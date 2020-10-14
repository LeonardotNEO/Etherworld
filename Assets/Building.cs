using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    private List<Item> costToCraftBuilding;
    private GameObject buildingPrefab;
    public Sprite buildingSprite;
    private string nameOfBuilding;
    private string buildingDescription;

    //CONSTRUCTOR
    public Building(List<Item> costToCraftBuilding, GameObject  buildingPrefab, Sprite buildingSprite, string nameOfBuilding, string buildingDescription){
        this.costToCraftBuilding = costToCraftBuilding;
        this.buildingPrefab = buildingPrefab;
        this.buildingSprite = buildingSprite;
        this.nameOfBuilding = nameOfBuilding;
        this.buildingDescription = buildingDescription;
    }

    //GETTERS
    public List<Item> getCostToCraftBuilding()
    {
        return costToCraftBuilding;
    }
    public string getCostToCraftBuildingToString()
    {
        string costToCraftBuildingToString = "";
        foreach(var item in costToCraftBuilding){
            costToCraftBuildingToString += "ItemName: " + item.getName() + "\nItemAmount: " + item.getItemAmount();
        }
        if(costToCraftBuildingToString == ""){
            costToCraftBuildingToString = "None";
        }
        return costToCraftBuildingToString;
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
    public void setCostToCraftBuilding(List<Item> newCostToCraftBuilding)
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
