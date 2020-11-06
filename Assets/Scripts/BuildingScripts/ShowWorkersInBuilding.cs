using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWorkersInBuilding : MonoBehaviour
{
    GameManager gameManager;
    BuildingAttributes buildingAttributes;
    public GameObject citizenButton;
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
        foreach(Citizen citizen in buildingAttributes.getWorkersInBuilding()){
            GameObject newButton = Instantiate(citizenButton, transform);
            newButton.GetComponentInChildren<Text>().text = citizen.getName(); 
        }
    }
}
