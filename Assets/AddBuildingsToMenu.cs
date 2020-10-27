using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBuildingsToMenu : MonoBehaviour
{
    GameManager gameManager;
    public GameObject element;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        for(int i = 0; i < gameManager.getBuildingCatalog().getAmountOfBuildingsInCatalog(); i++){
            GameObject thisElement = (GameObject)Instantiate(element, transform);
            thisElement.transform.Find("Text").GetComponent<Text>().text = gameManager.getBuildingCatalog().getBuildingsCatalog()[i].getNameOfBuilding();
            /*thisElement.transform.Find("Text").GetComponent<Text>().text = buildingsCatalog.getBuildingsCatalog()[i].getDescriptionOfBulding();
            foreach(var item in buildingsCatalog.getBuildingsCatalog()[i].getCostToCraftBuilding()){
                thisElement.transform.Find("ResourcesToCraft/Text").GetComponent<Text>().text += item.Value + " " + item.Key + "\n";
            }
            thisElement.transform.Find("Sprite").GetComponent<Image>().sprite = buildingsCatalog.getBuildingsCatalog()[i].getBuildingSprite();
            */
        }
    }
}
