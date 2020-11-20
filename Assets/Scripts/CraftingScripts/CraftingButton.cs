using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    GameManager gameManager;
    public int buttonId;
    public string buildingName;
    public Dictionary<string, int> costToCraftBuilding;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        buttonId = gameManager.getCraftingSystem().getCraftingButtonIDCounter();
        gameManager.getCraftingSystem().updateCraftingButtonIDCounter();
    }

    public int getButtonId(){
        return buttonId;
    }

    public void setBuildingName(string buildingName){
        this.buildingName = buildingName;
    }

    public void setCostToCraftBuilding(Dictionary<string, int> list){
        costToCraftBuilding = list;
    }

    public void hoverBuildingEnter(){
        transform.Find("Hover").gameObject.SetActive(true);
        updateColors();
    }
    public void hovedBuildingExit(){
        transform.Find("Hover").gameObject.SetActive(false);
    }

    public void updateColors(){
        foreach(Transform child in transform.Find("Hover/Cost")){
            if(child.GetComponent<CostText>()){
                string itemName = child.GetComponent<CostText>().getItemName();
                int amount = child.GetComponent<CostText>().getAmount();

                if(gameManager.getPlayerBehavior().getInventory().checkIfListOfItemsAreInInventory(new Dictionary<string, int>(){{itemName, amount}})){
                    child.GetComponent<Text>().color = Color.green;
                } else {
                    child.GetComponent<Text>().color = Color.red;
                }
            }
        }
    }
}
