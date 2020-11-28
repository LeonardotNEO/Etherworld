using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skills : MonoBehaviour
{
    [SerializeField]
    List<Skill> skills = new List<Skill>();
    GameManager gameManager;
    bool playerSkill;
    bool addedSkills;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(transform.tag == "player"){
            playerSkill = true;
        }

        foreach(string skill in gameManager.getSkillCatalog().getSkillCatalog()){
            skills.Add(new Skill(skill, playerSkill));
        }
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
        //Debug.Log("No skill with that name");
        return null;
    }

    public void increaseLevel(string skillName, int amount){
        Skill skill = getSkillByName(skillName);


        skill.increaseLevel(amount);
        checkIfNewPerk(skillName);
        skill.setSpeedMultiplier(10 + skill.getLevel()/2);
    }
    public void increaseExperience(string skillName, int amount){
        Skill skill = getSkillByName(skillName);
        skill.increaseExperience(amount);
        checkIfNewLevel(skillName);
        
    }
    public void checkIfNewLevel(string skillName){
        Skill skill = getSkillByName(skillName);

        while(skill.getExperience() >= Mathf.Pow(skill.getLevelingDifficulty(), skill.getLevel()) * 1000 + skill.getLastExperience()){
            skill.setLastExperience((int)(Mathf.Pow(skill.getLevelingDifficulty(), skill.getLevel()) * 1000 + skill.getLastExperience()));
            if(skill.getLevel() < 99){
                increaseLevel(skillName, 1);
            }
        }

        if(transform.tag == "player" && GameObject.FindGameObjectWithTag("Skills").transform.Find("Background").gameObject.activeSelf){
            gameManager.GetUI().updateSkills();
        }

        if(gameManager.getCitizenCatalog().getSelectedCitizen()){
            if(gameManager.getCitizenCatalog().getSelectedCitizen().Equals(this.transform.GetComponent<Citizen>()) && GameObject.FindGameObjectWithTag("CitizenMenuUI").transform.Find("Background").gameObject.activeSelf){
                gameManager.GetUI().updateCitizenSkills();
            }
        }
        
    }

    public void checkIfNewPerk(string skillName){
        Skill skill = getSkillByName(skillName);
        PerkAttributes perkAttributes;
        if(this.transform.GetComponent<PerkAttributes>()){
            perkAttributes = this.transform.GetComponent<PerkAttributes>();
            perkAttributes.updatePerksUnlocked();
        } else {
            Debug.Log("This player does not have perks");
        }
    }

    public Skill getSkillWithHighestLevel(){
        Skill highestSkill = null;
        foreach(Skill skill in skills){
            if(highestSkill == null){
                highestSkill = skill;
                continue;
            }
            if(skill.getLevel() >= highestSkill.getLevel()){
                highestSkill = skill;
            }
        }   
        return highestSkill;
    }

    public Skill getSkillWithSecondHighestLevel(){
        Skill highestSkill = null;
        Skill secondHighestSkill = null;
        foreach(Skill skill in skills){
            if(highestSkill == null || secondHighestSkill == null){
                highestSkill = skill;
                secondHighestSkill = skill;
                continue;
            }
            if(skill.getLevel() >= highestSkill.getLevel()){
                highestSkill = skill;
                continue;
            }
            if(skill.getLevel() >= secondHighestSkill.getLevel() && skill != highestSkill){
                secondHighestSkill = skill;
            }
        }   
        return highestSkill;
    }
}
