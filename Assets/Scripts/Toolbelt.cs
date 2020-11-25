using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Toolbelt : MonoBehaviour
{
    GameManager gameManager;
    public List<InventorySlot> toolbar = new List<InventorySlot>();
    public InventorySlot magic = null;
    public InventorySlot melee = null;
    public InventorySlot ranged = null;
    public InventorySlot shield = null;
    public InventorySlot pickaxe = null;
    public InventorySlot axe = null;
    public InventorySlot hammer = null;
    public InventorySlot food = null;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        addSlotToToolbar(magic = new InventorySlot(1, "Magic"));
        addSlotToToolbar(melee = new InventorySlot(1, "Melee"));
        addSlotToToolbar(ranged = new InventorySlot(1, "Ranged"));
        addSlotToToolbar(shield = new InventorySlot(1, "Shield"));
        addSlotToToolbar(pickaxe = new InventorySlot(1, "Pickaxe"));
        addSlotToToolbar(axe = new InventorySlot(1, "Axe"));
        addSlotToToolbar(hammer = new InventorySlot(1, "Hammer"));
        addSlotToToolbar(food = new InventorySlot(99, "Food"));
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
