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
    private GameObject itemPrefab;
    private Sprite itemSprite;
    private List<string> itemTypes1Available = new List<string>{
        "Melee", 
        "Pickaxe", 
        "Axe", 
        "Hammer", 
        "Magic", 
        "Ranged", 
        "Shield",
        "Armor", 
        "Food", 
        "Mineral", 
        "Misc", 
        "Wood", 
        "Stone", 
        "Woodcutting", 
        "Smithing", 
        "Mining"
        };
    private List<string> itemTypes2Available = new List<string>{
        // MELEE //
        "Onehand", 
        "Twohand",

        // Pickaxe //
        "Pickaxe",

        // AXE//
        "Axe",

        // HAMMER //
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

        // SHIELD //
        "Shield",

        // ARMOR //
        "Helmet", 
        "Torso", 
        "Legs", 
        "Boots", 

        // FOOD //
        "Cattle", 

        // MINERAL //
        "Mineral", 

        // MISC //
        "Wood", 

        // WOODCUTTING //
        "Log", 
        "Plank", 

        // WOOD //
        "Wood", 

        // STONE //
        "Stone",

        // SMITHING //
        "Bar", 

        //MINGING // 
        "Ore"
        };

    public Item(string itemName, string itemDescription, string itemType1, string itemType2, Dictionary<string, int> costToCraftItem, GameObject itemPrefab, Sprite itemSprite){
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
}
