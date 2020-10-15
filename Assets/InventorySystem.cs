using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    Inventory mainInventory;
    public GameObject npc;
    
    void Start()
    {
        mainInventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
    }

    void Update()
    {
        // Showing inventory of player: items and amout to the inventory UI
        Text inventoryTextName = GameObject.FindGameObjectWithTag("InventoryName").GetComponentInChildren<Text>();
        Text inventoryTextAmount = GameObject.FindGameObjectWithTag("InventoryAmount").GetComponentInChildren<Text>();
        inventoryTextName.text = mainInventory.getNameOfResourcesInInventoryToString();
        inventoryTextAmount.text = mainInventory.getAmountOfResourcesInInventoryToString();

        if(Input.GetKeyDown("g")){
            RaycastHit mouseButtonPressed;
            Ray movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movementRay, out mouseButtonPressed, Mathf.Infinity, LayerMask.GetMask("Ground"))){
                Instantiate(npc, new Vector3(mouseButtonPressed.point.x, mouseButtonPressed.point.y, mouseButtonPressed.transform.position.z), mouseButtonPressed.transform.rotation);
            }
        }
    }
}

