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
    private int initialValue;
    private int buildingID;
    private int residentialLimit;
    private int workerLimit;
    private string nameOfBuilding;
    private string buildingTag;
    private string buildingDescription;
    private string jobName;

    //CONSTRUCTOR
    public Building(
        Dictionary<string, int> costToCraftBuilding, 
        Dictionary<string, int> buildingUpkeep, 
        Dictionary<string, int> buildingProduction, 
        Dictionary<string, int> neededForProduction, 
        GameObject buildingPrefab, 
        Sprite buildingSprite, 
        int storageCapacity, 
        int initialValue, 
        int residentialLimit, 
        int workerLimit, 
        string nameOfBuilding, 
        string buildingTag, 
        string buildingDescription, 
        string jobName)
        {
        this.costToCraftBuilding = costToCraftBuilding;
        this.buildingUpkeep = buildingUpkeep;
        this.buildingProduction = buildingProduction;
        this.neededForProduction = neededForProduction;
        this.storageCapacity = storageCapacity;
        this.initialValue = initialValue;
        this.buildingPrefab = buildingPrefab;
        this.buildingSprite = buildingSprite;
        this.nameOfBuilding = nameOfBuilding;
        this.buildingTag = buildingTag;
        this.buildingDescription = buildingDescription;
        this.buildingID++;
        this.workerLimit = workerLimit;
        this.residentialLimit = residentialLimit;
        this.jobName = jobName;
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
    public int getInitialValue(){
        return initialValue;
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

    public int getResidentialLimit(){
        return residentialLimit;
    }
    public int getWorkerLimit(){
        return workerLimit;
    }
    public string getJobName(){
        return jobName;
    }
    public string getTag(){
        return buildingTag;
    }
}
