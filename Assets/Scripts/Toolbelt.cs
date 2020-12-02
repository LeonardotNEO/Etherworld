using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Toolbelt : MonoBehaviour
{
    GameManager gameManager;
    public List<InventorySlot> toolbar = new List<InventorySlot>();
    public InventorySlot currentlySelectedSlot;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        // TOOLBELT
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Wand", "Staff", "Book"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Onehand", "Twohand"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Bow"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Shield"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Pickaxe"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Axe"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Hammer"}));
        addSlotToToolbar(new InventorySlot(99, new List<string>(){"Food"}));

        // EQUIPMENT
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Head"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Torso"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Legs"}));
        addSlotToToolbar(new InventorySlot(1, new List<string>(){"Boots"}));
        addSlotToToolbar(new InventorySlot(500, new List<string>(){"Magic dust"}));
        addSlotToToolbar(new InventorySlot(500, new List<string>(){"Arrow"}));

    }

    public List<InventorySlot> getToolbelt(){
        return toolbar;
    }
    public bool addToSlot(InventorySlot inventorySlot){
        string itemType = gameManager.getItemCatalog().getItemType2ByName(inventorySlot.getItemInSlot());

        foreach(InventorySlot slot in toolbar){
            if(slot.getInventorySlotType().Contains(itemType)){
                if(slot.getItemInSlot() != null){
                    transferFromToolbarToInventory(this.transform.GetComponent<Inventory>(), itemType);
                }
                slot.addInventorySlotToThis(inventorySlot);

                if(this.transform.tag.Equals("player")){
                    // UPDATE TOOLBAR
                    gameManager.GetUI().updateToolbarInterface();

                    // UPDATE EQUIPMENT
                    gameManager.GetUI().updateEquipmentInterface();
                }

                return true;
            } 
        }
        return false;
    }

    public void transferFromToolbarToInventory(Inventory inventory, string types){
        string[] typesArray = types.Split(' ');

        foreach(InventorySlot inventorySlot in toolbar){
            if(inventorySlot.getCurrentAmountInSlot() > 0){
                foreach(var itemTypesInSlot in inventorySlot.getInventorySlotType()){
                    foreach(var itemTypes in typesArray){
                        if(itemTypesInSlot.Equals(itemTypes)){
                            int amountToNotRemove = gameManager.getPlayerBehavior().getInventory().addItemToInventory(inventorySlot.getItemInSlot(), inventorySlot.getCurrentAmountInSlot());
                            inventorySlot.decreaseCurrentAmountInSlot(inventorySlot.getCurrentAmountInSlot() - amountToNotRemove);

                            if(this.transform.tag.Equals("player")){
                                gameManager.GetUI().updateToolbarInterface();
                                gameManager.GetUI().updateEquipmentInterface();
                            }
                            return;
                        }
                    }
                }
            }
        }
    }

    public void addSlotToToolbar(InventorySlot slot){
        toolbar.Add(slot);
    }
    public List<InventorySlot> getToolbar(){
        return toolbar;
    }
    public void setCurrentlySelectedSlot(InventorySlot slot){
        currentlySelectedSlot = slot;
    }
    public InventorySlot getCurrentlySelectedSlot(){
        return currentlySelectedSlot;
    }
}
