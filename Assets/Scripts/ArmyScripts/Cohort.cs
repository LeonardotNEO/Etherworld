using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT WILL SIT ON COHORT COLLIDER

public class Cohort : MonoBehaviour
{
    public Army army;
    public List<Soldier> soldiers = new List<Soldier>();
    public PlayerBehavior commanderPlayer;
    public Soldier commanderSoldier;
    public Inventory cohortInventory;
    public string status;
    public string mission;
    public int size;
    public int morale;
    public int cohortLoyality;
    public string cohortType;

    // PREFABS
    public GameObject cohortPrefab;
    public GameObject soldierPositionPrefab;

    // BOOLS
    public bool pickupItems;

    public Cohort(){

    }

    public void selectCohort(){
        
    }
    public void setupCamp(){

    }
    public void followCommander(){

    }
    public void goToLocation(){

    }
    public void prepareDefences(){

    }
    public void defenceMode(){

    }
    public void attackAtWill(){

    }
    public void attackNearbyCity(){

    }
    public void charge(){

    }
    public void skirmish(){

    }
}
