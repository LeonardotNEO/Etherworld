using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAvailableWorkers : MonoBehaviour
{
    GameManager gameManager;
    Town town;
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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if(gameManager.getBuildingCatalog().getBuildingLastClicked() != null){
            town = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>().getTownBuildingIsApartOf();
        }

        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        if(town){
            foreach(Citizen citizen in town.getAvailableWorkersInTown()){
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
