using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Toolbelt : MonoBehaviour
{
    GameManager gameManager;
    public List<InventorySlot> toolbar = new List<InventorySlot>();

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        addSlotToToolbar(new InventorySlot(1, "Magic"));
        addSlotToToolbar(new InventorySlot(1, "Melee"));
        addSlotToToolbar(new InventorySlot(1, "Ranged"));
        addSlotToToolbar(new InventorySlot(1, "Shield"));
        addSlotToToolbar(new InventorySlot(1, "Pickaxe"));
        addSlotToToolbar(new InventorySlot(1, "Axe"));
        addSlotToToolbar(new InventorySlot(1, "Hammer"));
        addSlotToToolbar(new InventorySlot(99, "Food"));
    }

    public List<InventorySlot> getToolbelt(){
        return toolbar;
    }
    public bool addToSlot(InventorySlot inventorySlot){
        string itemType = gameManager.getItemCatalog().getItemType1ByName(inventorySlot.getItemInSlot());

        foreach(InventorySlot slot in toolbar){
            if(slot.getInventorySlotType().Equals(itemType)){
                slot.addInventorySlotToThis(inventorySlot);
                gameManager.GetUI().updateToolbarInterface();
                return true;
            } 
        }
        return false;
    }

    public void transferFromToolbarToInventory(Inventory inventory, string type){
        foreach(InventorySlot inventorySlot in toolbar){
            if(type.Equals(inventorySlot.getInventorySlotType()) && inventorySlot.getCurrentAmountInSlot() > 0){
                int amountToNotRemove = gameManager.getPlayerBehavior().getInventory().addItemToInventory(inventorySlot.getItemInSlot(), inventorySlot.getCurrentAmountInSlot());
                inventorySlot.decreaseCurrentAmountInSlot(inventorySlot.getCurrentAmountInSlot() - amountToNotRemove);
            }
        }
        gameManager.GetUI().updateToolbarInterface();
    }

    public void addSlotToToolbar(InventorySlot slot){
        toolbar.Add(slot);
    }
    public List<InventorySlot> getToolbar(){
        return toolbar;
    }
}
