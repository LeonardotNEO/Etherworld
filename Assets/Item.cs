using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemName;
    private string itemDescription;
    private string itemType;
    Dictionary<string, int> costToCraftItem;
    private GameObject itemPrefab;
    private Sprite itemSprite;
    private List<string> itemTypesAvailable = new List<string>{"Weapon", "Resource", "Tool", "Food"};

    public Item(string itemName, string itemDescription, string itemType, Dictionary<string, int> costToCraftItem, GameObject itemPrefab, Sprite itemSprite){
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        if(itemTypesAvailable.Contains(itemType)){
            this.itemType = itemType;
        } else {
            throw new System.ArgumentException("This type doesnt exist. Chose from: " + itemTypesAvailable[0] + " " + itemTypesAvailable[1] + " " + itemTypesAvailable[2] + " " + itemTypesAvailable[3] + " ");
        }
        this.itemPrefab = itemPrefab;
        this.itemSprite = itemSprite;
        this.costToCraftItem = costToCraftItem;
    }

    public string getName(){
        return itemName;
    }
    public string getItemDescription(){
        return itemDescription;
    }
    public string getItemType(){
        return itemType;
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
