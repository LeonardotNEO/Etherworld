using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    [SerializeField] private int health;
    [SerializeField] private int armor;
    [SerializeField] private int ranged;
    [SerializeField] private int magic;
    [SerializeField] private int melee;
    [SerializeField] private int attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float critChance;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int invetoryCapacity;
    [SerializeField] private int frostResistance;
    [SerializeField] private int fireResistance;

    public Equipment(int health, int armor, int ranged, int magic, int melee, int attackSpeed, float attackRange, float critChance, int movementSpeed, int invetoryCapacity, int frostResistance, int fireResistance)
    {
        this.health = health;
        this.armor = armor;
        this.ranged = ranged;
        this.magic = magic;
        this.melee = melee;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.critChance = critChance;
        this.movementSpeed = movementSpeed;
        this.invetoryCapacity = invetoryCapacity;
        this.frostResistance = frostResistance;
        this.fireResistance = fireResistance;
    }

    public int getHealth()
    {
        return this.health;
    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getArmor()
    {
        return this.armor;
    }

    public void setArmor(int armor)
    {
        this.armor = armor;
    }

    public int getRanged()
    {
        return this.ranged;
    }

    public void setRanged(int ranged)
    {
        this.ranged = ranged;
    }

    public int getMagic()
    {
        return this.magic;
    }

    public void setMagic(int magic)
    {
        this.magic = magic;
    }

    public int getMelee()
    {
        return this.melee;
    }

    public void setMelee(int melee)
    {
        this.melee = melee;
    }

    public int getAttackSpeed()
    {
        return this.attackSpeed;
    }

    public void setAttackSpeed(int attackSpeed)
    {
        this.attackSpeed = attackSpeed;
    }

    public int getMovementSpeed()
    {
        return this.movementSpeed;
    }

    public void setMovementSpeed(int movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public int getInvetoryCapacity()
    {
        return this.invetoryCapacity;
    }

    public void setInvetoryCapacity(int invetoryCapacity)
    {
        this.invetoryCapacity = invetoryCapacity;
    }

    public int getFrostResistance()
    {
        return this.frostResistance;
    }

    public void setFrostResistance(int frostResistance)
    {
        this.frostResistance = frostResistance;
    }

    public int getFireResistance()
    {
        return this.fireResistance;
    }

    public void setFireResistance(int fireResistance)
    {
        this.fireResistance = fireResistance;
    }

    public float getAttackRange()
    {
        return this.attackRange;
    }

    public void setAttackRange(float attackRange)
    {
        this.attackRange = attackRange;
    }

    public float getCritChance()
    {
        return this.critChance;
    }

    public void setCritChance(float critChance)
    {
        this.critChance = critChance;
    }



}
