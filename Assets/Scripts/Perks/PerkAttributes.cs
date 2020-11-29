using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerkAttributes : MonoBehaviour
{
    [SerializeField] List<Perk> perks = new List<Perk>();
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void addPerkToPerks(Perk perk){
        if(!perks.Contains(perk)){
            perks.Add(perk);
        } else {
            //Debug.Log("Perk already in perks");
        }
    }

    public void addPerkToPerksByName(string name){
        foreach(Perk perk in perks){
            if(perk.getName().Equals(name)){
                perks.Add(perk);
            }
        }
    }

    public void removePerkFromPerks(Perk perk){
        perks.Remove(perk);
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

    public List<Perk> getPerksByItem(string itemName){
        List<Perk> perks = new List<Perk>();

        foreach(Perk perk in perks){
            if(perk.getSkill().Equals(gameManager.getItemCatalog().getItemByName(itemName).getItemType1()) && !perks.Contains(perk)){
                perks.Add(perk);
            }
        }
        return perks;
    }

    public Perk getPerksByName(string perkName){
        foreach(Perk perk in perks){
            if(perk.getName().Equals(perkName)){
                return perk;
            }
        }
        return null;
    }

    public void updatePerksUnlocked(){
        List<Perk> perkCatalog = gameManager.getPerkCatalog().getPerkCatalog();

        if(GetComponent<PlayerBehavior>()){
            foreach(Perk perk in perkCatalog){
                if(perk.getLevel() <= GetComponent<PlayerBehavior>().getSkills().getSkillByName(perk.getSkill()).getLevel()){
                    addPerkToPerks(perk);
                }
            }
        }
    }

    public List<Ability> getAbilitiesUnlocked(){
        List<Ability> abilities = new List<Ability>();
        foreach(Perk perk in perks){
            if(perk.getAbility() != null){
                abilities.Add(perk.getAbility());
            }
        }

        return abilities;
    }

    public List<Ability> getAbilitiesUnlockedBySkill(string name){
        List<Ability> abilities = new List<Ability>();
        foreach(Perk perk in perks){
            if(perk.getAbility() != null && perk.getAbility().getSkill().Equals(name)){
                abilities.Add(perk.getAbility());
            }
        }

        return abilities;
    }
}
