using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBuildingsToMenu : MonoBehaviour
{
    GameManager gameManager;
    public GameObject element;
    public GameObject costToCraftSlot;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        List<Building> buildingCatalog = gameManager.getBuildingCatalog().getBuildingsCatalog();
/*
        for(int i = 0; i < buildingCatalog.Count; i++){
            GameObject thisElement = (GameObject)Instantiate(element, transform);
            thisElement.transform.Find("Text").GetComponent<Text>().text = gameManager.getBuildingCatalog().getBuildingsCatalog()[i].getNameOfBuilding();
        }
        */
        foreach(Building building in buildingCatalog){
            GameObject thisBuilding = (GameObject)Instantiate(element, transform);
            thisBuilding.transform.Find("Text").GetComponent<Text>().text = building.getNameOfBuilding();
            thisBuilding.GetComponent<CraftingButton>().setBuildingName(building.getNameOfBuilding());
            thisBuilding.GetComponent<CraftingButton>().setCostToCraftBuilding(building.getCostToCraftBuilding());

            foreach(var item in building.getCostToCraftBuilding()){
                GameObject costToCraftItem = Instantiate(costToCraftSlot, thisBuilding.transform.Find("Hover/Cost").transform);
                costToCraftItem.GetComponent<Text>().text = item.Value + " " + item.Key;
                costToCraftItem.GetComponent<CostText>().setItemName(item.Key);
                costToCraftItem.GetComponent<CostText>().setAmount(item.Value);
            }
        }
    }
}
