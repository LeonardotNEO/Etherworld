using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public int level;
    public int experience;
    public int lastExperience;
    public int speedMultiplier;
    public bool playerSkill;
    public float levelingDifficulty;

    public Skill(string name, bool playerSkill){
        skillName = name;
        level = 1;
        experience = 0;
        lastExperience = 0;
        speedMultiplier = 10;
        this.playerSkill = playerSkill;
        levelingDifficulty = 1.04f;
    }


    //-------------//
    // BASE METHOD //
    //-------------//
    // GETTERS
    public int getLevel(){
        return level;
    }
    public int getExperience(){
        return experience;
    }
    public string getName(){
        return skillName;
    }
    public int getExperienceNextLevel(){
        if(level == 99){
            return 0;
        } else {
            return (int)(Mathf.Pow(levelingDifficulty, level) * 1000 + lastExperience);
        }
    }
    public int getExperienceLeft(){
        if(level == 99){
            return 0;
        } else {
            return (int)(Mathf.Pow(levelingDifficulty, level) * 1000 + lastExperience) - experience;
        }
    }
    public float getPercentageLeft(){
        if(level == 99){
            return 0;
        } else {
            return (float)((double)100/(getExperienceNextLevel() - lastExperience)) * (experience - lastExperience);
        }
    }
    public int getSpeedMultiplier(){
        return speedMultiplier;
    }

    // SETTERS
    public void increaseLevel(int amount){
        level += amount;
        speedMultiplier = 10 + level/2;
    }
    public void increaseExperience(int amount){
        experience += amount;
        checkIfNewLevel();
        
    }
    public void checkIfNewLevel(){
        while(experience >= Mathf.Pow(levelingDifficulty, level) * 1000 + lastExperience){
            lastExperience = (int)(Mathf.Pow(levelingDifficulty, level) * 1000 + lastExperience);
            if(level < 99){
                increaseLevel(1);
            }
        }
        if(playerSkill){
            GameObject.FindGameObjectWithTag("Skills").GetComponent<ShowSkillsMenu>().updateSkills();
        }
        
    }
}
