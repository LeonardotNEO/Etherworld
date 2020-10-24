using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour
{
    List<Item> itemCatalog = new List<Item>();
    public GameObject woodModel;
    public Sprite woodSprite;
    public GameObject stonePileModel;
    public Sprite stonePileSprite;
    void Awake()
    {
        //ADDING ITEMS TO CATALOG
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Stone",
                /*Description*/     "A pile of stone",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Stone", 5}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
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
    
    public Item getItemByIndex(int index){
        return itemCatalog[index];
    }
}
