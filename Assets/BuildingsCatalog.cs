using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsCatalog : MonoBehaviour
{   
    public List<Building> buildingsCatalog = new List<Building>();
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


    void Start()
    {
        //ADDING BUILDINGS TO CATALOG
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new List<Item>{new Item("Wood", 100), new Item("Stone", 20)},
                /*Prefab*/              house01Model,
                /*Sprite*/              house01Sprite,
                /*Name*/                "House 1",
                /*Description*/         "This is just a simple house"
            ));
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new List<Item>{new Item("Wood", 200), new Item("Stone", 100)},
                /*Prefab*/              house02Model,
                /*Sprite*/              house02Sprite,
                /*Name*/                "House 2",
                /*Description*/         "This house is a litle bigger than house 1, but still almost the same"
            ));
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new List<Item>{new Item("Wood", 300), new Item("Stone", 150)},
                /*Prefab*/              house03Model,
                /*Sprite*/              house03Sprite,
                /*Name*/                "House 3",
                /*Descriptione*/        "House 3... Now we're talking. Look at those walls! Look at he floor! Truly a magnificent beast of a building"
            ));
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new List<Item>{new Item("Wood", 500), new Item("Stone", 200)},
                /*Prefab*/              house04Model,
                /*Sprite*/              house04Sprite,
                /*Name*/                "House 4",
                /*Descriptione*/        "The house cant possible get better than this, just look at the price!"
            ));
        addBuildingToCatalog(
            new Building(
                /*Cost to craft*/       new List<Item>{new Item("Wood", 1000), new Item("Stone", 500)},
                /*Prefab*/              house05Model,
                /*Sprite*/              house05Sprite,
                /*Name*/                "House 5",
                /*Descriptione*/        "Why is it snowing with this house?"
            ));

        Debug.Log(buildingsCatalogToString());     
    }

    public void addBuildingToCatalog(Building building){
        buildingsCatalog.Add(building);
    }
    public void removeBuildingFromCatalog(Building building){
        buildingsCatalog.Remove(building);
    }
    public List<Building> getBuildingsCatalog(){
        return buildingsCatalog;
    }

    public string buildingsCatalogToString(){
        string buildingsCatalogToString = "";
        foreach(var building in buildingsCatalog){
            buildingsCatalogToString += 
            "------------ITEM------------- " 
            + "\nHOUSE NAME:\n" + building.getNameOfBuilding() + "\n"
            + "\nCOST TO CRAFT BUILDING:\n" + building.getCostToCraftBuildingToString() + "\n";
        }
        return buildingsCatalogToString;
    }

    public int getAmountOfBuildingsInCatalog(){
        return buildingsCatalog.Count;
    }
}
