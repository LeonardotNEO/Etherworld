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

    void Start()
    {

    }

    void Update()
    {
        if(gameManager.getBuildingCatalog().getBuildingLastClicked()){
            if(gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>()){
                inventory = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<Inventory>();
                changeSlot(0);
                changeSlot(1);
                changeSlot(2);
                changeSlot(3);
                changeSlot(4);
                changeSlot(5);
                changeSlot(6);
                changeSlot(7);
                changeSlot(8);
                changeSlot(9);
                changeSlot(10);
                changeSlot(11);
            }

        }

    }
    public void checkIfEmpty(int slotNumber){
        if(inventory.getInventorySlots()[slotNumber].getCurrentAmountInSlot() == 0){
            transform.GetChild(slotNumber).gameObject.SetActive(false);
        } else {
            transform.GetChild(slotNumber).gameObject.SetActive(true);
        }
    }

    public void changeSlot(int slotNumber){
        checkIfEmpty(slotNumber);
        transform.GetChild(slotNumber).Find("Text (1)").GetComponent<Text>().text = inventory.getInventorySlots()[slotNumber].getItemInSlot();
        transform.GetChild(slotNumber).Find("Text").GetComponent<Text>().text = inventory.getInventorySlots()[slotNumber].getCurrentAmountInSlot().ToString();
    }
}
