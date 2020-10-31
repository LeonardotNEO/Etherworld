using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMainInventory : MonoBehaviour
{
    GameManager gameManager;
    public GameObject slot;

    public Inventory inventory;
    public bool inventoryOpen;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        inventoryOpen = true;   
    }

    void OnDisable()
    {
        inventoryOpen = false;
    }

    void Start()
    {
        for(int i = 0; i < inventory.getInventoryCapacity(); i++){
            Instantiate(slot, transform);
        }
    }

    void Update()
    {
        if(inventoryOpen){
            for(int i = 0; i < inventory.getInventoryCapacity(); i++){
                changeSlot(i);
            }
        }
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
        transform.GetChild(slotNumber).Find("Button/Text").GetComponent<Text>().text = inventory.getInventorySlots()[slotNumber].getItemInSlot();
        transform.GetChild(slotNumber).Find("Panel/Text").GetComponent<Text>().text = inventory.getInventorySlots()[slotNumber].getCurrentAmountInSlot().ToString();
        checkIfEmpty(slotNumber);
    }
}
