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
    private CitizenCatalog citizenCatalog;
    private TownCatalog townCatalog;
    private EnemyCatalog enemyCatalog;
    private AbilityCatalog abilityCatalog;
    private PerkCatalog perkCatalog;
    private SkillCatalog skillCatalog;
    public Toolbelt toolbelt;
    private UI UI;
    private MessageLogText messageLogText;
    private Clock clock;
    private World world;

    void Awake()
    {
        //playerBehavior = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerBehavior>();
        craftingSystem = GetComponent<CraftingSystem>();
        buildingCatalog = GetComponent<BuildingsCatalog>();
        itemCatalog = GetComponent<ItemCatalog>();
        inventoryCatalog = GetComponent<InventoryCatalog>();
        citizenCatalog = GetComponent<CitizenCatalog>();
        townCatalog = GetComponent<TownCatalog>();
        enemyCatalog = GetComponent<EnemyCatalog>();
        abilityCatalog = GetComponent<AbilityCatalog>();
        perkCatalog = GetComponent<PerkCatalog>();
        skillCatalog = GetComponent<SkillCatalog>();
        toolbelt = GetComponent<Toolbelt>();
        UI = GetComponent<UI>();
        messageLogText = GameObject.FindGameObjectWithTag("MessageLogBarUI").GetComponentInChildren<MessageLogText>();
        clock = GetComponent<Clock>();
        //world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
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
    public CitizenCatalog getCitizenCatalog(){
        return citizenCatalog;
    }
    public TownCatalog getTownCatalog(){
        return townCatalog;
    }
    public EnemyCatalog getEnemyCatalog(){
        return enemyCatalog;
    }
    public AbilityCatalog getAbilityCatalog(){
        return abilityCatalog;
    }
    public PerkCatalog getPerkCatalog(){
        return perkCatalog;
    }
    public SkillCatalog getSkillCatalog(){
        return skillCatalog;
    }
    public PlayerBehavior getPlayerBehavior(){
        return playerBehavior;
    }
    public CraftingSystem getCraftingSystem(){
        return craftingSystem;
    }
    public Toolbelt getToolbelt(){
        return toolbelt;
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
    public World getWorld(){
        return world;
    }

}
