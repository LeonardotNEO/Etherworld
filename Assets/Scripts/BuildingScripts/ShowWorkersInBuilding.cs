using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWorkersInBuilding : MonoBehaviour
{
    GameManager gameManager;
    BuildingAttributes buildingAttributes;
    public GameObject citizenButton;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        updateAvailableWorkersList();
    }

    public void updateAvailableWorkersList(){
        if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
            buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        }

        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        if(buildingAttributes){
            foreach(Citizen citizen in buildingAttributes.getWorkersInBuilding()){
                GameObject newButton = Instantiate(citizenButton, transform);
                newButton.GetComponentInChildren<Text>().text = citizen.getName();
                newButton.GetComponent<WorkersButton>().setCitizenID(citizen.getCitizenID());

                if(gameManager.getCitizenCatalog().getSelectedCitizen()){
                    if(citizen.getCitizenID() == gameManager.getCitizenCatalog().getSelectedCitizen().getCitizenID()){
                        Button button = newButton.GetComponent<Button>();
                        ColorBlock block = button.colors;
                        block.normalColor = Color.green; //new Color(159, 12, 255);
                        button.colors = block;
                    } else {
                        Button button = newButton.GetComponent<Button>();
                        ColorBlock block = button.colors;
                        block.normalColor = new Color(255, 255, 255);
                        button.colors = block;
                    }
                }
            }
        }
    }
}
