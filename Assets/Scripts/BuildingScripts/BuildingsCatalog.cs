using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingsCatalog : MonoBehaviour
{   
    public List<Building> buildingCatalog = new List<Building>();
    public List<BuildingAttributes> buildingsInWorld = new List<BuildingAttributes>();
    public int amountOfBuildingsInGame;
    GameObject buildingLastClicked = null; // IMPORTANT
    UnfinishedBuilding unfinishedBuildingSelected = null;

    //-------------//
    // TOWN CENTER //
    //-------------//
    public GameObject townCenterRank1;
    public Sprite townCenterRank1Sprite;
    public GameObject townCenterRank2;
    public Sprite townCenterRank2Sprite;
    public GameObject townCenterRank3;
    public Sprite townCenterRank3Sprite;
    public GameObject townCenterRank4;
    public Sprite townCenterRank4Sprite;
    public GameObject townCenterRank5;
    public Sprite townCenterRank5Sprite;

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

    //---------//
    // STORAGE //
    //---------//
    public GameObject warehouse;
    public Sprite warehouseSprite;

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
        // TOWN CENTER //
        //-------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ },
                /*Upkeep*/              new Dictionary<string, int>(){ },
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              townCenterRank1,
                /*Sprite*/              townCenterRank1Sprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       0,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Town center rank 1",
                /*Tag*/                 "Town center",
                /*Description*/         "A town center makes it possible to found a town",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              townCenterRank2,
                /*Sprite*/              townCenterRank2Sprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       0,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Town center rank 2",
                /*Tag*/                 "Town center",
                /*Description*/         "A town center makes it possible to found a town",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              townCenterRank3,
                /*Sprite*/              townCenterRank3Sprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       0,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Town center rank 3",
                /*Tag*/                 "Town center",
                /*Description*/         "A town center makes it possible to found a town",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              townCenterRank4,
                /*Sprite*/              townCenterRank4Sprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       0,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Town center rank 4",
                /*Tag*/                 "Town center",
                /*Description*/         "A town center makes it possible to found a town",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              townCenterRank5,
                /*Sprite*/              townCenterRank5Sprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       0,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Town center rank 5",
                /*Tag*/                 "Town center",
                /*Description*/         "A town center makes it possible to found a town",
                /*Job Name*/            null
            )
        );

        //-------------//
        // RESIDENTIAL //
        //-------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              smallWoodHouse,
                /*Sprite*/              smallWoodHouseSprite,
                /*StorageCapcity*/      3,
                /*Initial Value*/       200,
                /*Residential Limit*/   3,
                /*Worker Limit*/        0,
                /*Name*/                "Small Wood House",
                /*Tag*/                 "Residential",
                /*Description*/         "A house that can be used to store items and used as shelter",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumWoodHouse,
                /*Sprite*/              mediumWoodHouseSprite,
                /*StorageCapcity*/      4,
                /*Initial Value*/       200,
                /*Residential Limit*/   4,
                /*Worker Limit*/        0,
                /*Name*/                "Medium Wood House",
                /*Tag*/                 "Residential",
                /*Description*/         "A house that can be used to store items and used as shelter",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 100}, {"Stone", 100}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 5}, {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              boardingHouse,
                /*Sprite*/              boardingHouseSprite,
                /*StorageCapcity*/      5,
                /*Initial Value*/       200,
                /*Residential Limit*/   20,
                /*Worker Limit*/        0,
                /*Name*/                "Boarding House",
                /*Tag*/                 "Residential",
                /*Descriptione*/        "Housing for those that dont have their own house",
                /*Job Name*/            null
            )
        );
        
        //------------//
        // INDUSTRIAL //
        //------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 20}, {"Stone", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 1}, {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){ {"Bucket of water", 1}},
                /*NeededForProduction*/ new Dictionary<string, int>(){ {"Bucket", 1}},
                /*Prefab*/              waterWell,
                /*Sprite*/              waterWellSprite,
                /*StorageCapcity*/      2,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        2,
                /*Name*/                "Waterwell",
                /*Tag*/                 "Industrial",
                /*Descriptione*/        "Can gather water here",
                /*Job Name*/            "Water gatherer"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 200}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 5}},
                /*Production*/          new Dictionary<string, int>(){ {"Wood plank", 1}, {"Spruce plank", 1}, {"Pine plank", 1}, {"Yew plank", 1}, {"Birch plank", 1}, {"Oak plank", 1}, {"Ash plank", 1}},
                /*NeededForProduction*/ new Dictionary<string, int>(){ {"Wood log", 1}, {"Spruce log", 1}, {"Pine log", 1}, {"Yew log", 1}, {"Birch log", 1}, {"Oak log", 1}, {"Ash log", 1}},
                /*Prefab*/              sawmill,
                /*Sprite*/              sawmillSprite,
                /*StorageCapcity*/      8,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        10,
                /*Name*/                "Sawmill",
                /*Tag*/                 "Industrial",
                /*Descriptione*/        "Sawmill produce planks",
                /*Job Name*/            "Sawmiller"
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Stone", 200}, {"Wood plank", 25}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Stone", 5}, {"Wood plank", 1}},
                /*Production*/          new Dictionary<string, int>(){ {"Bronze bar", 1}, {"Iron bar", 1}, {"Steel bar", 1}, {"Silver bar", 1}, {"Gold bar", 1}},
                /*NeededForProduction*/ new Dictionary<string, int>(){ {"Copper ore", 1}, {"Tin ore", 1}, {"Iron ore", 1}, {"Coal ore", 1}, {"Silver ore", 1}, {"Gold ore", 1}, {"Kimberlite", 1}, {"Neonium", 1}, {"Ethereum", 1}},
                /*Prefab*/              furnace,
                /*Sprite*/              furnaceSprite,
                /*StorageCapcity*/      8,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        5,
                /*Name*/                "Furnace",
                /*Tag*/                 "Industrial",
                /*Descriptione*/        "A place where one can smelt ore to bars",
                /*Job Name*/            "Smelter"
            )
        );

        //-------------------//
        // STORAGE BUILDINGS //
        //-------------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 150}, {"Stone", 50}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 4}, {"Stone", 1}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              warehouse,
                /*Sprite*/              warehouseSprite,
                /*StorageCapcity*/      50,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Warehouse",
                /*Tag*/                 "Storage",
                /*Descriptione*/        "A place for storing items",
                /*Job Name*/            null
            )
        );

        //---------------//
        // FORTIFICATION //
        //---------------//
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 5}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 1}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              woodFence,
                /*Sprite*/              woodFenceSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Wood Fence",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "Keeps things out",
                /*Job Name*/            null
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
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Small Stone Wall",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A small stone sized wall for keeping things out",
                /*Job Name*/            null
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
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Medium Stone Wall",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A medium sized stone wall used for protection",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 5}, {"Stone", 10}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 2}, {"Stone", 3}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              mediumStoneWallGate,
                /*Sprite*/              mediumStoneWallGateSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Medium Stone Gate",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A gate for getting through to the other side of the wall",
                /*Job Name*/            null
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
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Medium Stone Wall Corner",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A medium sized stone wall corner",
                /*Job Name*/            null
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
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Big Stone Wall",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A big sized stone wall used for protection",
                /*Job Name*/            null
            )
        );
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new Dictionary<string, int>(){ {"Wood plank", 5}, {"Stone", 30}},
                /*Upkeep*/              new Dictionary<string, int>(){ {"Wood plank", 2}, {"Stone", 5}},
                /*Production*/          new Dictionary<string, int>(){},
                /*NeededForProduction*/ new Dictionary<string, int>(){},
                /*Prefab*/              bigStoneWallGate,
                /*Sprite*/              bigStoneWallGateSprite,
                /*StorageCapcity*/      0,
                /*Initial Value*/       200,
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Big Stone Gate",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A gate for getting through to the other side of the wall",
                /*Job Name*/            null
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
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Big Stone Wall Corner",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A big sized stone wall corner",
                /*Job Name*/            null
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
                /*Residential Limit*/   0,
                /*Worker Limit*/        0,
                /*Name*/                "Big Stone Wall Inverted Corner",
                /*Tag*/                 "Fortification",
                /*Descriptione*/        "A big sized stone wall inverted corner",
                /*Job Name*/            null
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



    public void addBuildingToWorld(BuildingAttributes building){
        buildingsInWorld.Add(building);
    }
    public void removeBuildingFromWorld(BuildingAttributes building){
        buildingsInWorld.Remove(building);
    }
    public BuildingAttributes getBuildingInWorldByID(int ID){
        foreach(BuildingAttributes building in buildingsInWorld){
            if(building.getBuildingID() == ID){
                return building;
            }
        }
        return null;
    }   
    public List<BuildingAttributes> getBuildingInWorld(){
        return buildingsInWorld;
    }
    public List<BuildingAttributes> getBuildingInWorldByTag(string tag){
        List<BuildingAttributes> buildingsByTag = new List<BuildingAttributes>();
        foreach(BuildingAttributes building in buildingsInWorld){
            if(building.tag == tag){
                buildingsByTag.Add(building);
            }
        }
        return buildingsByTag;
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
        if(buildingLastClicked){
            return buildingLastClicked.GetComponent<Inventory>();
        }
        return null;
    }
    public BuildingAttributes getBuildingLastClickedAttributes(){
        if(buildingLastClicked){
            return buildingLastClicked.GetComponent<BuildingAttributes>();
        }
        return null;
    }

    public Building getBuildingByName(string name){
        foreach(Building building in buildingCatalog){
            if(name.Contains(building.getNameOfBuilding())){
                return building;
            }
        }
        return null; 
    }
    public Building getBuildingByIndex(int index){
        return buildingCatalog[index];
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

    public List<Building> getBuildingsByTag(string tag){
        List<Building> buildingsByTag = new List<Building>();

        foreach(Building building in buildingCatalog){
            if(building.getTag().Equals(tag)){
                buildingsByTag.Add(building);
                //Debug.Log(building.getNameOfBuilding());
            }
        }
        return buildingsByTag;
    }

    public void setUnfinishedBuildingSelected(UnfinishedBuilding building){
        unfinishedBuildingSelected = building;
    }

    public UnfinishedBuilding getUnfinishedBuildingSelected(){
        return unfinishedBuildingSelected;
    }

}
