using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMainInventory : MonoBehaviour
{
    GameManager gameManager;
    public GameObject slot;

    public Inventory inventory;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        for(int i = 0; i < inventory.getInventoryCapacity(); i++){
            Instantiate(slot, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
