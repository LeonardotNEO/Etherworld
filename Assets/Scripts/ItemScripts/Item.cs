using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private string itemType1;
    [SerializeField] private string itemType2;
    [SerializeField] Dictionary<string, int> costToCraftItem;
    private Equipment equipment;
    private GameObject itemPrefab;
    private Sprite itemSprite;
    private List<string> itemTypes1Available = new List<string>{
        "Melee",  
        "Magic", 
        "Ranged", 
        "Resiliance",
        "Woodcutting", 
        "Smithing", 
        "Mining",
        "Crafting", 
        "Food", 
        "Armor",

        "Mineral", 
        "Misc", 
        "Wood", 
        "Stone"
        };
    private List<string> itemTypes2Available = new List<string>{
        // MELEE //
        "Onehand", 
        "Twohand",

        // MINING //
        "Pickaxe",
        "Bar", 
        "Ore",
        "Mineral", 
        "Stone",

        // WOODCUTTING //
        "Axe",
        "Log", 
        "Plank", 
        "Wood", 

        // CRAFTING //
        "Hammer",

        // MAGIC //
        "Wand", 
        "Staff", 
        "Book", 
        "Dust", 

        // RANGED //
        "Bow", 
        "Crossbow",
        "Arrow",

        // RESILIANCE //
        "Shield",

        // ARMOR //
        "Head", 
        "Torso", 
        "Legs", 
        "Boots", 

        // FOOD //
        "Cattle", 
        "Food",

        // MISC //
        "Wood", 

        
        };

    public Item(string itemName, string itemDescription, string itemType1, string itemType2, Dictionary<string, int> costToCraftItem, GameObject itemPrefab, Sprite itemSprite, Equipment equipment){
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        if(itemTypes1Available.Contains(itemType1)){
            this.itemType1 = itemType1;
        } else {
            throw new System.ArgumentException("This type1 doesnt exist. " + itemType1);
        }
        if(itemTypes2Available.Contains(itemType2)){
            this.itemType2 = itemType2;
        } else {
            throw new System.ArgumentException("This type2 doesnt exist. " + itemType2);
        }
        this.itemPrefab = itemPrefab;
        this.itemSprite = itemSprite;
        this.costToCraftItem = costToCraftItem;
        this.equipment = equipment;
    }

    public string getName(){
        return itemName;
    }
    public string getType(){
        return itemType1;
    }
    public string getItemDescription(){
        return itemDescription;
    }
    public string getItemType1(){
        return itemType1;
    }
    public string getItemType2(){
        return itemType2;
    }
    public Item getItemObject(){
        return this;
    }
    public GameObject getItemPrefab(){
        return itemPrefab;
    }
    public Dictionary<string, int> getCostToCraftItem(){
        return costToCraftItem;
    }

    public Equipment getEquipment(){
        return equipment;
    }
}
