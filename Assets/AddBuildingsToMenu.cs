using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBuildingsToMenu : MonoBehaviour
{
    BuildingsCatalog buildingsCatalog;
    public GameObject element;
    void Start()
    {
        buildingsCatalog = GameObject.FindGameObjectWithTag("Crafting").GetComponent<BuildingsCatalog>();
        

        for(int i = 0; i < buildingsCatalog.getAmountOfBuildingsInCatalog(); i++){
            GameObject thisElement = (GameObject)Instantiate(element, transform);
            thisElement.transform.Find("Description/Text").GetComponent<Text>().text = buildingsCatalog.getBuildingsCatalog()[i].getDescriptionOfBulding();
            for(int y = 0; y < buildingsCatalog.getBuildingsCatalog()[i].getCostToCraftBuilding().Count; y++){
                thisElement.transform.Find("ResourcesToCraft/Text").GetComponent<Text>().text += buildingsCatalog.getBuildingsCatalog()[i].getCostToCraftBuilding()[y].getItemAmount() + " " + buildingsCatalog.getBuildingsCatalog()[y].getCostToCraftBuilding()[y].getName() + "\n";
            }
            thisElement.transform.Find("Sprite").GetComponent<Image>().sprite = buildingsCatalog.getBuildingsCatalog()[i].getBuildingSprite();
        }
    }
}
