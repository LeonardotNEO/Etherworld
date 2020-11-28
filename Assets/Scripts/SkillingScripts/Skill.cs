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

    public string getSkillName(){
        return skillName;
    }
    public int getLevel(){
        return level;
    }
    public void increaseLevel(int amount){
        level += amount;
    }
    public float getLevelingDifficulty(){
        return levelingDifficulty;
    }
    public float getSpeedMultiplier(){
        return speedMultiplier;
    }
    public void setSpeedMultiplier(int val){
        speedMultiplier = val;
    }
    public int getExperience(){
        return experience;
    }
    public void increaseExperience(int amount){
        experience += amount;
    }
    public int getLastExperience(){
        return lastExperience;
    }
    public void setLastExperience(int amount){
        lastExperience = amount;
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
}
