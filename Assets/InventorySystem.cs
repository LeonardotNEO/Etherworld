using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public bool inventoryOpen = false;

    
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey("i") && inventoryOpen == false){
            openInventory();
        } else if(Input.GetKey("escape")){
            inventoryOpen = false;
        }
    }

    void openInventory(){
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Inventory mainInventory = player.GetComponent<Inventory>();
        
        inventoryOpen = true;
        Debug.Log(mainInventory.getInventory());
    }


    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.tag == "WoodPile"){
            // WOOD // 20
            GameObject player = GameObject.FindGameObjectWithTag("player");
            Item item = player.GetComponent<Item>();
            Inventory mainInventory = player.GetComponent<Inventory>();

            item.setItemNameAmount("Wood", 20);
            mainInventory.updateInventory(item);
            //
            Destroy(collisionInfo.collider.gameObject);
            Debug.Log(item.getAmount());
            Debug.Log(item.getName());

        }
    }
}

