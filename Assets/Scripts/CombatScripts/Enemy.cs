using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    private string name;
    private string description;
    private string type;
    private List<string> typesAvailable = new List<string>{"Human", "Zombie", "Belmmyae", "Manticore", "Troll", "Huldra", "Werewolf", "Bahkauv"};
    private int level;
    private int baseHealth;
    private int baseDamage;
    private float baseCritChance;
    private float attackRange;
    private int baseArmor;
    private int baseMovementspeed;
    private int baseAttackSpeed;
    private bool aggressive;

    public Enemy(string name, string description, string type, int level, int maxHealth, int damage, float critChance, float attackRange, int armor, int movementspeed, int baseAttackSpeed, bool aggressive){
        this.name = name;
        this.description = description;
        if(!typesAvailable.Contains(type)){
            throw new System.ArgumentException("That type does not exist");
        } else {
            this.type = type;
        }
        this.type = type;
        this.level = level;
        this.baseHealth = maxHealth;
        this.baseDamage = damage;
        this.baseCritChance = critChance;
        this.attackRange = attackRange;
        this.baseArmor = armor;
        this.baseMovementspeed = movementspeed;
        this.baseAttackSpeed = baseAttackSpeed;
        this.aggressive = aggressive;
    }

    public string getName(){
        return name;
    }
    public string getDescription(){
        return description;
    }
    public string getType(){
        return type;
    }
    public int getLevel(){
        return level;
    }
    public int getBaseHealth(){
        return baseHealth;
    }
    public int getBaseDamage(){
        return baseDamage;
    }
    public float getBaseCritChange(){
        return baseCritChance;
    }
    public int getBaseArmor(){
        return baseArmor;
    }
    public int getBaseMovementSpeed(){
        return baseMovementspeed;
    }
    public int getAttackSpeed(){
        return baseAttackSpeed;
    }
    public float getAttackRange(){
        return attackRange;
    }
}
