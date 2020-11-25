using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Perk
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string skill;
    [SerializeField] private int level;
    [SerializeField] private Ability ability;
    [SerializeField] private Building building;

    public Perk(string name, string description, string skill, int level, Ability ability, Building building){
        this.name = name;
        this.description = description;
        this.skill = skill;
        this.level = level;
        this.ability = ability;
        this.building = building;
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
    public int getLevel(){
        return level;
    }
    public Ability getAbility(){
        return ability;
    }
    public Building getBuilding(){
        return building;
    }
}
