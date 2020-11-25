using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerkCatalog : MonoBehaviour
{
    public List<Perk> perkCatalog = new List<Perk>();
    public List<string> categories;

    void Awake()
    {
        categories = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillCatalog>().getSkillCatalog();
        
        // WOODCUTTING //
        addPerkToCatalog(new Perk(
            /*Name*/            "Stone axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           10,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Bronze axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           20,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Steel axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           30,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Silver axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           40,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Gold axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           50,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Diamond axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           60,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Neon axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           70,
            /*Ability*/         null,
            /*Building*/        null
        ));
        addPerkToCatalog(new Perk(
            /*Name*/            "Ether axe",
            /*Description*/     "The ability to wield stone axe",
            /*Skill*/           "Woodcutting",
            /*Level*/           80,
            /*Ability*/         null,
            /*Building*/        null
        ));
    }

    void Start()
    {
        
    }

    public List<Perk> getPerkCatalog(){
        return perkCatalog;
    }
    public void addPerkToCatalog(Perk perk){
        if(!perkCatalog.Contains(perk)){
            perkCatalog.Add(perk);
        }
    }
    public void removePerkFromCatalog(Perk perk){
        perkCatalog.Remove(perk);
    }

    public List<Perk> getPerksBySkill(string skill){
        List<Perk> perks = new List<Perk>();

        foreach(Perk perk in perks){
            if(perk.getSkill().Equals(skill) && !perks.Contains(perk)){
                perks.Add(perk);
            }
        }
        return perks;
    }

}
