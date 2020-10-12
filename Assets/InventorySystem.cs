using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    //Inventory mainInventory = new Inventory();
    Inventory mainInventory;

    Text inventoryTextName;
    Text inventoryTextAmount;
    
    void Start()
    {
        mainInventory = GetComponent<Inventory>();
    }

    void Update()
    {
        // Showing items and amout to the inventory UI
        inventoryTextName = GameObject.Find("/UI Panel/Inventory/Panel/InventoryItemsName").GetComponent<Text>();
        inventoryTextAmount = GameObject.Find("/UI Panel/Inventory/Panel/InventoryItemsAmount").GetComponent<Text>();
        inventoryTextName.text = mainInventory.getNameOfResourcesInInventory();
        inventoryTextAmount.text = mainInventory.getAmountOfResourcesInInventory();
    }
}

