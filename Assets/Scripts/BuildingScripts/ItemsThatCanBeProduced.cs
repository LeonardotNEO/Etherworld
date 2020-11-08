using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsThatCanBeProduced : MonoBehaviour
{
    GameManager gameManager;
    BuildingAttributes buildingAttributes;
    public GameObject itemButton;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        updateItemsThatCanBeProducedList();
    }

    public void updateItemsThatCanBeProducedList(){
        if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
            buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        }

        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        if(buildingAttributes){
            foreach(var item in buildingAttributes.getItemsProducedInBuilding()){
                GameObject newButton = Instantiate(itemButton, transform);
                newButton.GetComponentInChildren<Text>().text = item.Key;
                newButton.GetComponent<SelectItemButton>().setItem(item.Key);


                if(gameManager.getItemCatalog().getSelectedItem() != null){
                    if(gameManager.getItemCatalog().getSelectedItem().getName() == item.Key){
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
