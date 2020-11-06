using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAvailableWorkers : MonoBehaviour
{
    GameManager gameManager;
    Town town;
    public GameObject citizenButton;
    void Update()
    {
        
    }

    void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
            town = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>().getTownBuildingIsApartOf();
        }

        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        foreach(Citizen citizen in town.getAvailableWorkersInTown()){
            GameObject newButton = Instantiate(citizenButton, transform);
            newButton.GetComponentInChildren<Text>().text = citizen.getName(); 
        }
    }
}
