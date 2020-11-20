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
        
        updateWorkersInBuildingList();
    }

    public void updateWorkersInBuildingList(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if(gameManager.getBuildingCatalog().getBuildingLastClicked() != null){
            buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        }
        

        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        if(buildingAttributes){
            // SHOWING PLAYER IF INSIDE BUILDING
            if(buildingAttributes.getPlayerEnteredBuilding()){
                GameObject newButton = Instantiate(citizenButton, transform);
                newButton.GetComponentInChildren<Text>().text = gameManager.getPlayerBehavior().getPlayerFirstName() + " " + gameManager.getPlayerBehavior().getPlayerLastName();
                newButton.GetComponent<WorkersButton>().setCitizenID(9999);
                Button button = newButton.GetComponent<Button>();
                ColorBlock block = button.colors;
                block.normalColor = Color.yellow; //new Color(159, 12, 255);
                button.colors = block;
            }

            // SHOWING CITIZENS
            foreach(Citizen citizen in buildingAttributes.getWorkersInBuilding()){
                GameObject newButton = Instantiate(citizenButton, transform);
                newButton.GetComponentInChildren<Text>().text = citizen.getFirstName() + " " + citizen.getLastName();
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
