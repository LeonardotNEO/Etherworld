using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour
{
    List<Item> itemCatalog = new List<Item>();
    public GameObject item01Model;
    public Sprite item01Sprite;
    public GameObject item02Model;
    public Sprite item02Sprite;
    void Start()
    {
        //ADDING ITEMS TO CATALOG
        addItemToCatalog(
            new Item(
                /*Name*/            "WoodPile",
                /*Description*/     "A pile of wood",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 5}, },
                /*Prefab*/          item01Model,
                /*Sprite*/          item01Sprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "StonePile",
                /*Description*/     "A pile of stone",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Stone", 5}, },
                /*Prefab*/          item01Model,
                /*Sprite*/          item01Sprite
            )
        );
        
    }
        

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("h")){
            Debug.Log(itemCatalogToString());
        }
    }

    public void addItemToCatalog(Item item){
        itemCatalog.Add(item);
    }

    public string itemCatalogToString(){
        string itemCatalogToString = "";
        foreach(var item in itemCatalog){
            itemCatalogToString += 
            "------------ITEM------------- " 
            + "\nITEM NAME:\n" + item.getName() + "\n"
            + "\nITEM DESCRIPTION:\n" + item.getItemDescription() + "\n"
            + "\nITEM TYPE:\n" + item.GetType() + "\n";
        }
        return itemCatalogToString;
    }

    public List<Item> getItemCatalog(){
        return itemCatalog;
    }
}
