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

                foreach(Transform child in transform){
                    Destroy(child.gameObject);
                }

                for(int i = 0; i < inventory.getInventoryCapacity(); i++){
                    GameObject slot1 = Instantiate(slot, transform);
                    slot1.GetComponentInChildren<InventorySlotButton>().setInventorySlotID(i);

                    if(inventory.getInventorySlots()[i].getCurrentAmountInSlot() != 0){
                        slot1.transform.Find("Button/Text").GetComponent<Text>().text = inventory.getInventorySlots()[i].getItemInSlot();
                        slot1.transform.Find("Panel/Text").GetComponent<Text>().text = inventory.getInventorySlots()[i].getCurrentAmountInSlot().ToString();
                    } else {
                        slot1.transform.Find("Button/Text").GetComponent<Text>().text = "";
                        slot1.transform.Find("Panel/Text").GetComponent<Text>().text = "";
                    }
                }
            }
        }
    }
}
