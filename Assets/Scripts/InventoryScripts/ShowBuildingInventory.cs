using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuildingInventory : MonoBehaviour
{
    GameManager gameManager;
    public GameObject slot;
    Inventory inventory;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        for(int i = 0; i < 100; i++){
                GameObject slot1 = Instantiate(slot, transform);
                slot1.GetComponentInChildren<InventorySlotButton>().setInventorySlotID(i);
        }
    }

    void Update()
    {

    }

    void OnEnable()
    {
        updateInventory();
    }


    public void updateInventory(){
        if(gameManager.getBuildingCatalog().getBuildingLastClicked() != null){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>()){
                inventory = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>();

                for(int i = 0; i < inventory.getInventoryCapacity(); i++){
                    transform.GetChild(i).gameObject.SetActive(true);
                }

                for(int i = 0; i < inventory.getInventoryCapacity(); i++){
                    if(inventory.getInventorySlots()[i].getCurrentAmountInSlot() != 0){
                        transform.GetChild(i).transform.Find("Button/Text").GetComponent<Text>().text = inventory.getInventorySlots()[i].getItemInSlot();
                        transform.GetChild(i).transform.Find("Panel/Text").GetComponent<Text>().text = inventory.getInventorySlots()[i].getCurrentAmountInSlot().ToString();
                    } else {
                        transform.GetChild(i).transform.Find("Button/Text").GetComponent<Text>().text = "";
                        transform.GetChild(i).transform.Find("Panel/Text").GetComponent<Text>().text = "";
                    }
                }
            }
        }

    }
}
