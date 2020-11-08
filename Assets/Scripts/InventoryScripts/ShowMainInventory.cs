using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMainInventory : MonoBehaviour
{
    GameManager gameManager;
    public GameObject slot;

    public Inventory inventory;
    public bool instansiatet;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        updateInventory();
    }
    
    public void updateInventory(){
        inventory = gameManager.getInventoryCatalog().getMainInventory();

        for(int i = 0; i < inventory.getInventoryCapacity(); i++){
            GameObject slot1;

            if(!instansiatet){
                slot1 = Instantiate(slot, transform);
                slot1.GetComponentInChildren<InventorySlotButton>().setInventorySlotID(i);
            }

            if(inventory.getInventorySlots()[i].getCurrentAmountInSlot() != 0){
                transform.GetChild(i).transform.Find("Button/Text").GetComponent<Text>().text = inventory.getInventorySlots()[i].getItemInSlot();
                transform.GetChild(i).transform.Find("Panel/Text").GetComponent<Text>().text = inventory.getInventorySlots()[i].getCurrentAmountInSlot().ToString();
            } else {
                transform.GetChild(i).transform.Find("Button/Text").GetComponent<Text>().text = "";
                transform.GetChild(i).transform.Find("Panel/Text").GetComponent<Text>().text = "";
            }
        }
        instansiatet = true;
    }
}
