using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsCatalog : MonoBehaviour
{   
    public List<Building> buildingCatalog = new List<Building>();
    public int amountOfBuildingsInGame;
    public GameObject buildingLastClicked = null; // IMPORTANT

    //-------------//
    // RESIDENTIAL //
    //-------------//
    public GameObject smallWoodHouse;
    public Sprite smallWoodHouseSprite;
    public GameObject mediumWoodHouse;
    public Sprite mediumWoodHouseSprite;
    public GameObject boardingHouse;
    public Sprite boardingHouseSprite;

    //------------//
    // INDUSTRIAL //
    //------------//
    public GameObject waterWell;
    public Sprite waterWellSprite;
    public GameObject sawmill;
    public Sprite sawmillSprite;
    public GameObject furnace;
    public Sprite furnaceSprite;

    //---------------//
    // FORTIFICATION //
    //---------------//
    public GameObject woodFence;
    public Sprite woodFenceSprite;
    public GameObject smallStoneWall;
    public Sprite smallStoneWallSprite;
    public GameObject mediumStoneWall;
    public Sprite mediumStoneWallSprite;
    public GameObject mediumStoneWallGate;
    public Sprite mediumStoneWallGateSprite;
    public GameObject mediumStoneWallCorner;
    public Sprite MediumStoneWallCornerSprite;
    public GameObject bigStoneWall;
    public Sprite bigStoneWallSprite;
    public GameObject bigStoneWallGate;
    public Sprite bigStoneWallGateSprite;
    public GameObject bigStoneWallCorner;
    public Sprite bigStoneWallCornerSprite;
    public GameObject bigStoneWallInvertedCorner;
    public Sprite bigStoneWallInvertedCornerSprite;


    void Awake()
    {
        //-----------------------------//
        // ADDING BUILDINGS TO CATALOG //
        //-----------------------------//

        //-------------//
        // RESIDENTIAL //
        //-------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 3}},
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
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 5}},
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
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 100}, {"Stone", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 5}, {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              boardingHouse,
                /*Sprite*/              boardingHouseSprite,
                /*StorageCapcity*/      5,
                /*Initial Value*/       200,
                /*Name*/                "Boarding House",
                /*Descriptione*/        "Housing for those that dont have their own house"
            )
        );
        
        //------------//
        // INDUSTRIAL //
        //------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 20}, {"Stone", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 1}, {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){ {"Bucket of water", 1}},
                /*NeededForProduction*/ new Dictionary<string, int>(){ {"Bucket", 1}},
                /*Prefab*/              waterWell,
                /*Sprite*/              waterWellSprite,
                /*StorageCapcity*/      3,
                /*Initial Value*/       200,
                /*Name*/                "Waterwell",
                /*Descriptione*/        "Can gather water here"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 200}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 5}},
                /*Production*/          new Dictionary<string, int>(){ {"Wood plank", 1}, {"Spruce plank", 1}, {"Pine plank", 1}, {"Yew plank", 1}, {"Birch plank", 1}, {"Oak plank", 1}, {"Ash plank", 1}},
                /*NeededForProduction*/ new Dictionary<string, int>(){ {"Wood log", 1}, {"Spruce log", 1}, {"Pine log", 1}, {"Yew log", 1}, {"Birch log", 1}, {"Oak log", 1}, {"Ash log", 1}},
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
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 200}, {"Wood planks", 25}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 5}, {"Planks", 1}},
                /*Production*/          new Dictionary<string, int>(){ {"Bronze bar", 1}, {"Iron bar", 1}, {"Steel bar", 1}, {"Silver bar", 1}, {"Gold bar", 1}},
                /*NeededForProduction*/ new Dictionary<string, int>(){ {"Copper ore", 1}, {"Tin ore", 1}, {"Iron ore", 1}, {"Coal ore", 1}, {"Silver ore", 1}, {"Gold ore", 1}, {"Kimberlite", 1}, {"Neonium", 1}, {"Ethereum", 1}},
                /*Prefab*/              furnace,
                /*Sprite*/              furnaceSprite,
                /*StorageCapcity*/      10,
                /*Initial Value*/       200,
                /*Name*/                "Furnace",
                /*Descriptione*/        "A place where one can smelt ore to bars"
            )
        );

        //---------------//
        // FORTIFICATION //
        //---------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 5}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 1}},
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
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 3}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 1}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              smallStoneWall,
                /*Sprite*/              smallStoneWallSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Small Stone Wall",
                /*Descriptione*/        "A small stone sized wall for keeping things out"
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
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Medium Stone Wall",
                /*Descriptione*/        "A medium sized stone wall used for protection"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 5}, {"Stone", 10}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 2}, {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumStoneWallGate,
                /*Sprite*/              mediumStoneWallGateSprite,
                /*StorageCapcity*/      0,
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
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Medium Stone Wall Corner",
                /*Descriptione*/        "A medium sized stone wall corner"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 40}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              bigStoneWall,
                /*Sprite*/              bigStoneWallSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Big Stone Wall",
                /*Descriptione*/        "A big sized stone wall used for protection"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood planks", 5}, {"Stone", 30}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood planks", 2}, {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              bigStoneWallGate,
                /*Sprite*/              bigStoneWallGateSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Big Stone Gate",
                /*Descriptione*/        "A gate for getting through to the other side of the wall"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 20}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              bigStoneWallCorner,
                /*Sprite*/              bigStoneWallCornerSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Big Stone Wall Corner",
                /*Descriptione*/        "A big sized stone wall corner"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 20}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              bigStoneWallInvertedCorner,
                /*Sprite*/              bigStoneWallInvertedCornerSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Name*/                "Big Stone Wall Inverted Corner",
                /*Descriptione*/        "A big sized stone wall inverted corner"
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
    public Inventory getBuildingLastClickedInventory(){
        return buildingLastClicked.GetComponent<Inventory>();
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
