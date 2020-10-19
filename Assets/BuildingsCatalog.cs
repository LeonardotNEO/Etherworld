using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsCatalog : MonoBehaviour
{   
    public List<Building> buildingCatalog = new List<Building>();
    public GameObject smallWoodHouse;
    public Sprite smallWoodHouseSprite;
    public GameObject mediumWoodHouse;
    public Sprite mediumWoodHouseSprite;
    public GameObject boardingHouse;
    public Sprite boardingHouseSprite;
    public GameObject waterWell;
    public Sprite waterWellSprite;
    public GameObject woodFence;
    public Sprite woodFenceSprite;


    void Awake()
    {
        //ADDING BUILDINGS TO CATALOG
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"WoodPile", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              smallWoodHouse,
                /*Sprite*/              smallWoodHouseSprite,
                /*StorageCapcity*/      3,
                /*Name*/                "Small Wood House",
                /*Description*/         "A house that can be used to store items and used as shelter"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"WoodPile", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumWoodHouse,
                /*Sprite*/              mediumWoodHouseSprite,
                /*StorageCapcity*/      4,
                /*Name*/                "Medium Wood House",
                /*Description*/         "A house that can be used to store items and used as shelter"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 100}, {"StonePile", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"WoodPile", 5}, {"StonePile", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              boardingHouse,
                /*Sprite*/              boardingHouseSprite,
                /*StorageCapcity*/      2,
                /*Name*/                "Boarding House",
                /*Descriptione*/        "Housing for those that dont have their own house"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 20}, {"StonePile", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"WoodPile", 1}, {"StonePile", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              waterWell,
                /*Sprite*/              waterWellSprite,
                /*StorageCapcity*/      1,
                /*Name*/                "Waterwell",
                /*Descriptione*/        "Can gather water here"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"WoodPile", 5}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"WoodPile", 1}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              woodFence,
                /*Sprite*/              woodFenceSprite,
                /*StorageCapcity*/      0,
                /*Name*/                "Wood fence",
                /*Descriptione*/        "Keeps things out"
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
