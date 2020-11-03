using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // SCRIPTS //
    public PlayerBehavior playerBehavior;
    private CraftingSystem craftingSystem;
    private BuildingsCatalog buildingCatalog;
    private ItemCatalog itemCatalog;
    private InventoryCatalog inventoryCatalog;
    private UI UI;
    private MessageLogText messageLogText;
    private Clock clock;

    void Awake()
    {
        //playerBehavior = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerBehavior>();
        craftingSystem = GetComponent<CraftingSystem>();
        buildingCatalog = GetComponent<BuildingsCatalog>();
        itemCatalog = GetComponent<ItemCatalog>();
        inventoryCatalog = GetComponent<InventoryCatalog>();
        UI = GetComponent<UI>();
        messageLogText = GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponentInChildren<MessageLogText>();
        clock = GetComponent<Clock>();
    }

    // GET SCRIPTS //
    public BuildingsCatalog getBuildingCatalog(){
        return buildingCatalog;
    }
    public ItemCatalog getItemCatalog(){
        return itemCatalog;
    }
    public InventoryCatalog getInventoryCatalog(){
        return inventoryCatalog;
    }
    public PlayerBehavior getPlayerBehavior(){
        return playerBehavior;
    }
    public CraftingSystem getCraftingSystem(){
        return craftingSystem;
    }
    public UI GetUI(){
        return UI;
    }
    public MessageLogText getMessageLogText(){
        return messageLogText;
    }
    public Clock getClock(){
        return clock;
    }
}
