using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skills : MonoBehaviour
{
    [SerializeField]
    List<Skill> skills = new List<Skill>();
    bool playerSkill;
    bool addedSkills;

    void Awake()
    {
        if(transform.tag == "player"){
            playerSkill = true;
        }
            skills.Add(new Skill("Woodcutting", playerSkill));
            skills.Add(new Skill("Mining", playerSkill));
            skills.Add(new Skill("Crafting", playerSkill));
            skills.Add(new Skill("Engineering", playerSkill));
            skills.Add(new Skill("Resiliance", playerSkill));
            skills.Add(new Skill("Onehand", playerSkill));
            skills.Add(new Skill("Twohand", playerSkill));
            skills.Add(new Skill("Ranged", playerSkill));
            skills.Add(new Skill("Magic", playerSkill));
            skills.Add(new Skill("Leadership", playerSkill));
            skills.Add(new Skill("Commanding", playerSkill));
            skills.Add(new Skill("Administration", playerSkill));
            skills.Add(new Skill("Researching", playerSkill));
            skills.Add(new Skill("Surgeoning", playerSkill));
            skills.Add(new Skill("Founding", playerSkill));
    }
    void OnEnable()
    {
        
    }

    public List<Skill> GetSkills(){
        return skills;
    }

    public Skill getSkillByName(string skillName){
        foreach(Skill skill in skills){
            if(skill.getName().Equals(skillName)){
                return skill;
            }
        }
        Debug.Log("No skill with that name");
        return null;
    }
}
