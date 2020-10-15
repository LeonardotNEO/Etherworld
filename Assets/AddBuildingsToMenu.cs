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
            foreach(var item in buildingsCatalog.getBuildingsCatalog()[i].getCostToCraftBuilding()){
                thisElement.transform.Find("ResourcesToCraft/Text").GetComponent<Text>().text += item.Value + " " + item.Key + "\n";
            }
            thisElement.transform.Find("Sprite").GetComponent<Image>().sprite = buildingsCatalog.getBuildingsCatalog()[i].getBuildingSprite();
            thisElement.transform.Find("Manager").GetComponent<CraftingSystem>().setButtonNr(i);
        }
    }
}
