using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour
{
    List<Item> itemCatalog = new List<Item>();
    public Item selectedItem;
    public GameObject woodModel;
    public Sprite woodSprite;
    public GameObject stonePileModel;
    public Sprite stonePileSprite;
    void Awake()
    {
        //ADDING ITEMS TO CATALOG

        //--------------//
        // WOOD, PLANKS //
        //--------------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Spruce log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Pine log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Yew log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Birch log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Oak log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ash log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Spruce plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Spruce log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Pine plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Pine log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Yew plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Yew log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Birch plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Birch log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Oak plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Oak log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ash plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Ash log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );

        //------------------//
        // STONE,           //
        //------------------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Stone",
                /*Description*/     "A pile of stone",
                /*Type*/            "Stone",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );

        //------//
        // BARS //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Bronze bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Copper ore", 1}, {"Tin ore", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Iron ore", 1}, {"Coal ore", 2}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Steel bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Iron ore", 3}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Silver ore", 3}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Gold ore", 5}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );

        //------//
        // ORES //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Copper ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Tin ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Coal ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Kimberlite ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Neonium ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ethereum ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        //------//
        // MISC //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Bucket",
                /*Description*/     "An empty bucket",
                /*Type*/            "Misc",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Bucket of water",
                /*Description*/     "A bucket filled with water",
                /*Type*/            "Misc",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Bucket", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
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

    public Item getItemByName(string name){
        Item itemReturn = null;
        foreach(Item item in itemCatalog){
            if(item.getName() == name){
                itemReturn = item;
            }
        }
        return itemReturn;
    }
    public Item getSelectedItem(){
        return selectedItem;
    }
    public void setSelectedItem(Item item){
        selectedItem = item;
    }

    public List<Item> getItemByType(string type){
        List<Item> items = new List<Item>();

        foreach(Item item in itemCatalog){
            if(item.getType().Equals(type)){
                items.Add(item);
            }
        }
        return items;
    }
}
