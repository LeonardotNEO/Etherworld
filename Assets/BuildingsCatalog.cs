using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsCatalog : MonoBehaviour
{   
    public List<Building> buildingCatalog = new List<Building>();
    public int amountOfBuildingsInGame;
    GameObject buildingLastClicked; // IMPORTANT
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
    public GameObject sawmill;
    public Sprite sawmillSprite;
    public GameObject smallStoneWall;
    public Sprite smallStoneWallSprite;
    public GameObject mediumStoneWall;
    public Sprite mediumStoneWallSprite;
    public GameObject mediumStoneWallGate;
    public Sprite mediumStoneWallGateSprite;
    public GameObject mediumStoneWallCorner;
    public Sprite MediumStoneWallCornerSprite;
    public GameObject furnace;
    public Sprite furnaceSprite;


    void Awake()
    {
        //ADDING BUILDINGS TO CATALOG
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              smallWoodHouse,
                /*Sprite*/              smallWoodHouseSprite,
                /*StorageCapcity*/      3,
                /*Initial Value*/       200,
                /*Name*/                "Small Wood House",
                /*Description*/         "A house that can be used to store items and used as shelter"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumWoodHouse,
                /*Sprite*/              mediumWoodHouseSprite,
                /*StorageCapcity*/      4,
                /*Initial Value*/       200,
                /*Name*/                "Medium Wood House",
                /*Description*/         "A house that can be used to store items and used as shelter"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 100}, {"Stone", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 5}, {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              boardingHouse,
                /*Sprite*/              boardingHouseSprite,
                /*StorageCapcity*/      2,
                /*Initial Value*/       200,
                /*Name*/                "Boarding House",
                /*Descriptione*/        "Housing for those that dont have their own house"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 20}, {"Stone", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 1}, {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              waterWell,
                /*Sprite*/              waterWellSprite,
                /*StorageCapcity*/      1,
                /*Initial Value*/       200,
                /*Name*/                "Waterwell",
                /*Descriptione*/        "Can gather water here"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 5}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 1}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              woodFence,
                /*Sprite*/              woodFenceSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Wood Fence",
                /*Descriptione*/        "Keeps things out"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 200}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              sawmill,
                /*Sprite*/              sawmillSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Sawmill",
                /*Descriptione*/        "Sawmill produce planks"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 15}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumStoneWall,
                /*Sprite*/              mediumStoneWallSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Medium Stone Wall",
                /*Descriptione*/        "A medium sized stone wall used for protection"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood", 5}, {"Stone", 10}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood", 2}, {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumStoneWallGate,
                /*Sprite*/              mediumStoneWallGateSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Medium Stone Gate",
                /*Descriptione*/        "A gate for getting through to the other side of the wall"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 5}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 2}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumStoneWallCorner,
                /*Sprite*/              MediumStoneWallCornerSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Medium Stone Wall Corner",
                /*Descriptione*/        "A medium sized stone wall corner"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 3}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 1}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              smallStoneWall,
                /*Sprite*/              smallStoneWallSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Small Stone Wall",
                /*Descriptione*/        "A small stone sized wall for keeping things out"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 200}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              furnace,
                /*Sprite*/              furnaceSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Furnace",
                /*Descriptione*/        "A place where one can smelt ore to bars"
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
    public int getAmountOfBuildingsInCatalog(){
        return buildingCatalog.Count;
    }
    public Building getBuilding(int buildingID){
        return buildingCatalog[buildingID];
    }
    public int getAmountOfBuildingsInGame(){
        return amountOfBuildingsInGame;
    }
    public void increaseAmountOfBuildingsInGame(int amount){
        amountOfBuildingsInGame += amount;
    }
    public void decreaseAmountOfBuildingsInGame(int amount){
        amountOfBuildingsInGame -= amount;
    }
    public void setBuildingLastClicked(GameObject building){
        buildingLastClicked = building;
    }
    public GameObject getBuildingLastClicked(){
        return buildingLastClicked;
    }

    public Building getBuildingByName(string name){
        foreach(Building building in buildingCatalog){
            if(name.Contains(building.getNameOfBuilding())){
                return building;
            }
        }
        return null; 
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
}
