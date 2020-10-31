using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsThatCanBeProduced : MonoBehaviour
{
    GameManager gameManager;
    BuildingAttributes buildingAttributes;
    public GameObject itemButton;

    void Update()
    {
        
    }

    void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
            buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        }

        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        foreach(var item in buildingAttributes.getItemsProducedInBuilding()){
            GameObject newButton = Instantiate(itemButton, transform);
            newButton.GetComponentInChildren<Text>().text = item.Key; 
        }
    }
}
