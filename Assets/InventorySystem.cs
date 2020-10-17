using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    Inventory mainInventory;
    public GameObject npc;
    public int amountOfInventoriesInGame;
    
    void Start()
    {
        mainInventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryCatalog>().getMainInventory();
    }

    void Update()
    {
        // Showing inventory of player: items and amout to the inventory UI
        Text inventoryTextName = GameObject.FindGameObjectWithTag("InventoryName").GetComponentInChildren<Text>();
        Text inventoryTextAmount = GameObject.FindGameObjectWithTag("InventoryAmount").GetComponentInChildren<Text>();
        inventoryTextName.text = mainInventory.getNameOfResourcesInInventoryToString();
        inventoryTextAmount.text = mainInventory.getAmountOfResourcesInInventoryToString();
    }

    public void spawnNPC(){
        RaycastHit mouseButtonPressed;
        Ray movementRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(movementRay, out mouseButtonPressed, Mathf.Infinity, LayerMask.GetMask("Ground"))){
            for(int i = 0; i < 10; i++){
                Instantiate(npc, new Vector3(mouseButtonPressed.point.x, mouseButtonPressed.point.y, mouseButtonPressed.point.z), mouseButtonPressed.transform.rotation);
            }
        }
    }
}

