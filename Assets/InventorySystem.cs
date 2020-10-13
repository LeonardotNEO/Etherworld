using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    Inventory mainInventory;

    Text inventoryTextName;
    Text inventoryTextAmount;
    
    void Start()
    {
        mainInventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
    }

    void Update()
    {
        // Showing items and amout to the inventory UI
        inventoryTextName = GameObject.FindGameObjectWithTag("InventoryName").GetComponentInChildren<Text>();
        inventoryTextAmount = GameObject.FindGameObjectWithTag("InventoryAmount").GetComponentInChildren<Text>();
        inventoryTextName.text = mainInventory.getNameOfResourcesInInventory();
        inventoryTextAmount.text = mainInventory.getAmountOfResourcesInInventory();
    }
}

