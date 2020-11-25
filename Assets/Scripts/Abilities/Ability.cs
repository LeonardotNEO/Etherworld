using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string skill;
    [SerializeField] private string type;
    [SerializeField] private int level;
    [SerializeField] private int expGain;
    [SerializeField] private Dictionary<string, int> costToCast;
    [SerializeField] private float cooldownTime;
    [SerializeField] private bool onCooldown;
    [SerializeField] private bool abilityUnlocked;
    private Animation animation;
    private Sprite sprite;

    public Ability(string name, string description, string skill, string type, int level, int expGain, Dictionary<string, int> costToCast, float cooldownTime, Animation animation, Sprite sprite){
        this.name = name;
        this.description = description;
        this.skill = skill;
        this.type = type;
        this.level = level;
        this.expGain = expGain;
        this.costToCast = costToCast;
        this.cooldownTime = cooldownTime;
        this.animation = animation;
        this.sprite = sprite;
    }
    public string getName(){
        return name;
    }
    public string getDescription(){
        return description;
    }
    public string getSkill(){
        return skill;
    }
    public string getType(){
        return type;
    }
    public float getCooldownTime(){
        return cooldownTime;
    }
    public bool getOnCooldown(){
        return onCooldown;
    }
    public void setOnCooldown(bool val){
        onCooldown = val;
    }
    public int getLevel(){
        return level;
    }
    public int getExpGain(){
        return expGain;
    }
    public Dictionary<string, int> getCostToCast(){
        return costToCast;
    }
    public bool getAbilityUnlocked(){
        return abilityUnlocked;
    }
    public void setAbilityUnlocked(bool val){
        abilityUnlocked = val;
    }
}
