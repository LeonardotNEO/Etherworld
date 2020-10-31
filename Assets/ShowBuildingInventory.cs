using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuildingInventory : MonoBehaviour
{
    GameManager gameManager;
    public GameObject slot;
    Inventory inventory;
    bool inventoryInstansiatet;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {
        if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>()){
                inventory = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>();

                if(!inventoryInstansiatet){
                    for(int i = 0; i < inventory.getInventoryCapacity(); i++){
                        Instantiate(slot, transform);
                    }
                    inventoryInstansiatet = true;
                }
                inventory = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>();
                for(int i = 0; i < inventory.getInventoryCapacity(); i++){
                    changeSlot(i);
                }
            }
        }
    }

    void OnDisable()
    {
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
        inventoryInstansiatet = false;
    }
    public void checkIfEmpty(int slotNumber){
        if(inventory.getInventorySlots()[slotNumber].getCurrentAmountInSlot() == 0){
            transform.GetChild(slotNumber).Find("Button/Text").gameObject.SetActive(false);
            transform.GetChild(slotNumber).Find("Panel/Text").gameObject.SetActive(false);
        } else {
            transform.GetChild(slotNumber).Find("Button/Text").gameObject.SetActive(true);
            transform.GetChild(slotNumber).Find("Panel/Text").gameObject.SetActive(true);
        }
    }

    public void changeSlot(int slotNumber){
        checkIfEmpty(slotNumber);
        transform.GetChild(slotNumber).Find("Button/Text").GetComponent<Text>().text = inventory.getInventorySlots()[slotNumber].getItemInSlot();
        transform.GetChild(slotNumber).Find("Panel/Text").GetComponent<Text>().text = inventory.getInventorySlots()[slotNumber].getCurrentAmountInSlot().ToString();
    }

    public void resetSlot(int slotNumber){
        transform.GetChild(slotNumber).Find("Button/Text").gameObject.SetActive(false);
        transform.GetChild(slotNumber).Find("Panel/Text").gameObject.SetActive(false);
    }
}
