using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsCatalog : MonoBehaviour
{   
    public List<Building> buildingCatalog = new List<Building>();
    public GameObject house01Model;
    public Sprite house01Sprite;
    public GameObject house02Model;
    public Sprite house02Sprite;
    public GameObject house03Model;
    public Sprite house03Sprite;
    public GameObject house04Model;
    public Sprite house04Sprite;
    public GameObject house05Model;
    public Sprite house05Sprite;


    void Awake()
    {
        //ADDING BUILDINGS TO CATALOG
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 500}, {"StonePile", 20}},
                /*Prefab*/              house01Model,
                /*Sprite*/              house01Sprite,
                /*MaxItemsStored*/      3,
                /*Name*/                "House 1",
                /*Description*/         "This is just a simple house"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 1000}, {"StonePile", 100}},
                /*Prefab*/              house02Model,
                /*Sprite*/              house02Sprite,
                /*MaxItemsStored*/      5,
                /*Name*/                "House 2",
                /*Description*/         "This house is a litle bigger than house 1, but still almost the same"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 2000}, {"StonePile", 300}},
                /*Prefab*/              house03Model,
                /*Sprite*/              house03Sprite,
                /*MaxItemsStored*/      7,
                /*Name*/                "House 3",
                /*Descriptione*/        "House 3... Now we're talking. Look at those walls! Look at he floor! Truly a magnificent beast of a building"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 3000}, {"StonePile", 2000}},
                /*Prefab*/              house04Model,
                /*Sprite*/              house04Sprite,
                /*MaxItemsStored*/      10,
                /*Name*/                "House 4",
                /*Descriptione*/        "The house cant possible get better than this, just look at the price!"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 5000}, {"StonePile", 5000}},
                /*Prefab*/              house05Model,
                /*Sprite*/              house05Sprite,
                /*MaxItemsStored*/      15,
                /*Name*/                "House 5",
                /*Descriptione*/        "Why is it snowing with this house?"
            )
        );
  
    }

    void Update()
    {
        
    }

    public void addBuildingToCatalog(Building building){
        buildingCatalog.Add(building);
    }
    public void removeBuildingFromCatalog(Building building){
        buildingCatalog.Remove(building);
    }
    public List<Building> getBuildingsCatalog(){
        return buildingCatalog;
    }

    public string getBuildingsCatalogToString(){
        string buildingsCatalogToString = "";
        foreach(var building in buildingCatalog){
            buildingsCatalogToString += 
            "------------BUILDING------------- " 
            + "\nHOUSE NAME:\n" + building.getNameOfBuilding() + "\n"
            + "\nCOST TO CRAFT BUILDING:\n" + building.getCostToCraftBuildingToString() + "\n";
        }
        return buildingsCatalogToString;
    }

    public int getAmountOfBuildingsInCatalog(){
        return buildingCatalog.Count;
    }

    public Building getBuilding(int buildingID){
        return buildingCatalog[buildingID];
    }
}
