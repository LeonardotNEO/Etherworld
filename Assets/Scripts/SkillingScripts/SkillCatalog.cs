using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCatalog : MonoBehaviour
{
    [SerializeField] public List<string> skillCatalog = new List<string>(){};

    void Awake()
    {
        addSkillToCatalog("Woodcutting");
        addSkillToCatalog("Mining");
        addSkillToCatalog("Crafting");
        addSkillToCatalog("Engineering");
        addSkillToCatalog("Resiliance");
        addSkillToCatalog("Onehand");
        addSkillToCatalog("Twohand");
        addSkillToCatalog("Ranged");
        addSkillToCatalog("Magic");
        addSkillToCatalog("Leadership");
        addSkillToCatalog("Commanding");
        addSkillToCatalog("Administration");
        addSkillToCatalog("Researching");
        addSkillToCatalog("Surgeoning");
        addSkillToCatalog("Founding");
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
