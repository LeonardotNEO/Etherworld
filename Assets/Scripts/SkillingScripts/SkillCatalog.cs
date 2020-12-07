using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCatalog : MonoBehaviour
{
    [SerializeField] public List<string> skillCatalog = new List<string>(){};

    void Awake()
    {
        addSkillToCatalog("Resiliance");
        addSkillToCatalog("Melee");
        addSkillToCatalog("Ranged");
        addSkillToCatalog("Magic");
        addSkillToCatalog("Woodcutting");
        addSkillToCatalog("Mining");
        addSkillToCatalog("Crafting");
        addSkillToCatalog("Engineering");
        addSkillToCatalog("Commanding");
        addSkillToCatalog("Founding");
        //addSkillToCatalog("Leadership"); <- A part of founding
        addSkillToCatalog("Administration");
        //addSkillToCatalog("Researching"); <- Teachnology not planned yet, building perks are unlocked through other skills
        addSkillToCatalog("Surgeoning");
    }

    public List<string> getSkillCatalog(){
        return skillCatalog;
    }
    public void addSkillToCatalog(string name){
        if(!skillCatalog.Contains(name)){
            skillCatalog.Add(name);
        }
    }
}
