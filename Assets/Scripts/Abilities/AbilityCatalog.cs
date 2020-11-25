using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityCatalog : MonoBehaviour
{
    public List<Ability> abilityCatalog = new List<Ability>();
    public Ability currentlySelectedAbility;

    // MAGIC //
    public Animation fireboltAnimation;
    public Sprite fireboltSprite;

    // RANGED //

    // ONEHAND //

    // TWOHAND //

    // SHIELD //
    void Awake()
    {
        //-------//
        // MAGIC //
        //-------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Firebolt",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Magic",
            /*Type*/            "Magic",
            /*Level*/           1,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));
        addAbilityToCatalog(new Ability(
            /*Name*/            "Icebolt",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Magic",
            /*Type*/            "Magic",
            /*Level*/           5,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));
        addAbilityToCatalog(new Ability(
            /*Name*/            "Lightningstrike",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Magic",
            /*Type*/            "Magic",
            /*Level*/           10,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));

        //---------//
        // RANGED  //
        //---------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Firearrow",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Ranged",
            /*Type*/            "Ranged",
            /*Level*/           10,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));

        //---------//
        // ONEHAND //
        //---------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Firestrike",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Onehand",
            /*Type*/            "Melee",
            /*Level*/           5,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));

        //---------//
        // TWOHAND //
        //---------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Firestrike",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Twohand",
            /*Type*/            "Melee",
            /*Level*/           5,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));
        
        //------------//
        // RESILIANCE //
        //------------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Defencive aura",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Resiliance",
            /*Type*/            "Shield",
            /*Level*/           5,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));

        //---------//
        // PICKAXE //
        //---------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Burst of strength",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Mining",
            /*Type*/            "Pickaxe",
            /*Level*/           10,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));

        //--------//
        // AXE    //
        //--------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Burst of strength",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Woodcutting",
            /*Type*/            "Axe",
            /*Level*/           10,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));

        //--------//
        // HAMMER //
        //--------//
        addAbilityToCatalog(new Ability(
            /*Name*/            "Burst of strength",
            /*Description*/     "Launch a small firebolt",
            /*Skill*/           "Crafting",
            /*Type*/            "Hammer",
            /*Level*/           10,
            /*Exp gain*/        400,
            /*Cost to cast*/    new Dictionary<string, int>(){{"Silver dust", 1}},
            /*CooldownTime*/    5.0f,
            /*Animation*/       fireboltAnimation,
            /*Sprite*/          fireboltSprite
        ));
    }

    public void addAbilityToCatalog(Ability ability){
        if(!abilityCatalog.Contains(ability)){  
            abilityCatalog.Add(ability);
        }
    }
    public void removeAbilityFromCatalog(Ability ability){
        abilityCatalog.Remove(ability);
    }
    public List<Ability> getAbilityCatalog(){
        return abilityCatalog;
    }
    public Ability getAbilityByName(string name){
        foreach(Ability ability in abilityCatalog){
            if(ability.getName().Equals(name)){
                return ability;
            }
        }
        return null;
    }
    public Ability getAbilityByNameAndType(string name, string type){
        foreach(Ability ability in abilityCatalog){
            if(ability.getName().Equals(name) && ability.getType().Equals(type)){
                return ability;
            }
        }
        return null;
    }
    public List<Ability> getAbilityBySkill(string skill){
        List<Ability> abilities = new List<Ability>();

        foreach(Ability ability in abilityCatalog){
            if(ability.getSkill().Equals(skill)){
                abilities.Add(ability);
            }
        }
        return abilities;
    }
    public List<Ability> getAbilityByLevel(int level){
        List<Ability> abilities = new List<Ability>();

        foreach(Ability ability in abilityCatalog){
            if(ability.getLevel().Equals(level)){
                abilities.Add(ability);
            }
        }
        return abilities;
    }
}
