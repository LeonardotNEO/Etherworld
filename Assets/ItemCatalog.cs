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
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Spruce log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Pine log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Yew log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Birch log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Oak log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ash log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Woodplanks", 5}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Spruce plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Pine plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Yew plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Birch plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Oak plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ash plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite
            )
        );

        //------------------//
        // MINING RESOURCES //
        //------------------//
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

        //------//
        // BARS //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Bronze bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Copper ore", 1}, {"Tin ore", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Iron ore", 1}, {"Coal ore", 2}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Steel bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Iron ore", 3}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Silver ore", 3}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Resource",
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
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Tin ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Coal ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Kimberlite ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Neonium ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ethereum ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Resource",
                /*CostToCraft*/     new Dictionary<string, int>(){},
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
}
