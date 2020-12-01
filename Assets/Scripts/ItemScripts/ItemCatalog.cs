using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCatalog : MonoBehaviour
{
    [SerializeField] public List<Item> itemCatalog = new List<Item>();
    [SerializeField] public Item selectedItem;
    public GameObject woodModel;
    public Sprite woodSprite;
    public GameObject stonePileModel;
    public Sprite stonePileSprite;
    void Awake()
    {
        //ADDING ITEMS TO CATALOG

        //------//
        // FOOD //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Beef",
                /*Description*/     "Yummy",
                /*Type1*/           "Food",
                /*Type2*/           "Food",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );

        //--------------//
        // WOOD, PLANKS, LOGS //
        //--------------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood log",
                /*Description*/     "A piece of a tree",
                /*Type1*/           "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Spruce log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Pine log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Yew log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Birch log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Oak log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ash log",
                /*Description*/     "A piece of a tree",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Log",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Spruce plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Spruce log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Pine plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Pine log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Yew plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Yew log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Birch plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Birch log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Oak plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Oak log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ash plank",
                /*Description*/     "Planks used for building",
                /*Type*/            "Wood",
                /*Type2*/           "Plank",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Ash log", 1}, },
                /*Prefab*/          woodModel,
                /*Sprite*/          woodSprite,
                /*Equipment*/       null
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
                /*Type2*/           "Stone",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );

        //------//
        // BARS //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Bronze bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Smithing",
                /*Type2*/           "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Copper ore", 1}, {"Tin ore", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Smithing",
                /*Type2*/           "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Iron ore", 1}, {"Coal ore", 2}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Steel bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Smithing",
                /*Type2*/           "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Iron ore", 3}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Smithing",
                /*Type2*/           "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Silver ore", 3}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold bar",
                /*Description*/     "A bar used for smithing stuff",
                /*Type*/            "Smithing",
                /*Type2*/           "Bar",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Gold ore", 5}, },
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );

        //------//
        // ORES //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Copper ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Tin ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Coal ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Kimberlite ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Neonium ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ethereum ore",
                /*Description*/     "Ores can be smelted into bars",
                /*Type*/            "Mining",
                /*Type2*/           "Ore",
                /*CostToCraft*/     new Dictionary<string, int>(){},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );

        //-------//
        // MELEE //
        //-------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood sword",
                /*Description*/     "Try swinging it",
                /*Type*/            "Melee",
                /*Type2*/           "Onehand",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               2,
                    /*Attackspeed*/         10,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Battleaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Melee",
                /*Type2*/           "Twohand",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Greatsword",
                /*Description*/     "Try swinging it",
                /*Type*/            "Melee",
                /*Type2*/           "Twohand",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //-------//
        // MAGIC //
        //-------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wand",
                /*Description*/     "Try swinging it",
                /*Type*/            "Magic",
                /*Type2*/           "Wand",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Staff",
                /*Description*/     "Try swinging it",
                /*Type*/            "Magic",
                /*Type2*/           "Staff",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Magic book",
                /*Description*/     "Try swinging it",
                /*Type*/            "Magic",
                /*Type2*/           "Book",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //--------//
        // RANGED //
        //--------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood bow",
                /*Description*/     "Try swinging it",
                /*Type*/            "Ranged",
                /*Type2*/           "Bow",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //--------//
        // SHIELD //
        //--------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood shield",
                /*Description*/     "Try swinging it",
                /*Type*/            "Resiliance",
                /*Type2*/           "Shield",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //-------//
        // ARMOR //
        //-------//
        //------//
        // HEAD //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wool hat",
                /*Description*/     "Try swinging it",
                /*Type*/            "Armor",
                /*Type2*/           "Head",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               2,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Helmet",
                /*Description*/     "Try swinging it",
                /*Type*/            "Armor",
                /*Type2*/           "Head",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               5,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //-------//
        // TORSO //
        //-------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wool jacket",
                /*Description*/     "Try swinging it",
                /*Type*/            "Armor",
                /*Type2*/           "Torso",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               5,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //------//
        // LEGS //
        //------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wool pants",
                /*Description*/     "Try swinging it",
                /*Type*/            "Armor",
                /*Type2*/           "Legs",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               4,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //----------//
        // PICKAXE  //
        //----------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Stone pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               1,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Bronze pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               2,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               3,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Steel pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               4,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               5,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               6,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Diamond pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               7,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Neon pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               8,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ether pickaxe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Mining",
                /*Type2*/           "Pickaxe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               9,
                    /*Attackspeed*/         1,
                    /*Attackrange*/         1f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //----------//
        // AXE  //
        //----------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Stone axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Bronze axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Steel axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Diamond axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Neon axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ether axe",
                /*Description*/     "Try swinging it",
                /*Type*/            "Woodcutting",
                /*Type2*/           "Axe",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );

        //----------//
        // HAMMER   //
        //----------//
        addItemToCatalog(
            new Item(
                /*Name*/            "Wood hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Stone hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Bronze hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Iron hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Steel hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Silver hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Gold hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Diamond hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Neon hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Ether hammer",
                /*Description*/     "Try swinging it",
                /*Type*/            "Crafting",
                /*Type2*/           "Hammer",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       new Equipment(
                    /*Health*/              0,
                    /*Armor*/               0,
                    /*Ranged*/              0,
                    /*Magic*/               0,
                    /*Melee*/               0,
                    /*Attackspeed*/         0,
                    /*Attackrange*/         0f,
                    /*Critchance*/          0f,
                    /*Movementspeed*/       0,
                    /*Inventorycapacity*/   0,
                    /*Frostrestistance*/    0,
                    /*Fireresistance*/      0
                )
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
                /*Type2*/           "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Wood plank", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
            )
        );
        addItemToCatalog(
            new Item(
                /*Name*/            "Bucket of water",
                /*Description*/     "A bucket filled with water",
                /*Type*/            "Misc",
                /*Type2*/           "Wood",
                /*CostToCraft*/     new Dictionary<string, int>(){{"Bucket", 1}},
                /*Prefab*/          stonePileModel,
                /*Sprite*/          stonePileSprite,
                /*Equipment*/       null
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

    public string getItemType1ByName(string name){
        foreach(Item item in itemCatalog){
            if(item.getName().Equals(name)){
                return item.getItemType1();
            }
        }
        return null;
    }
    public string getItemType2ByName(string name){
        foreach(Item item in itemCatalog){
            if(item.getName().Equals(name)){
                return item.getItemType2();
            }
        }
        return null;
    }
}
