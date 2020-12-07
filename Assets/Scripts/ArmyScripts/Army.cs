using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    public List<Cohort> cohorts = new List<Cohort>();
    public PlayerBehavior leaderPlayer;
    public Citizen leaderCitizen;
    public Town townAlliegence;
    public int cohortLimitSize;
    public int armyLoyality;

    // PREFABS
    public GameObject cohortPrefab;
    public GameObject soldierPositionPrefab;

    void Awake()
    {
        townAlliegence = GetComponent<Town>();
    }

    void Start()
    {
        instantiateCohort();
    }


    // INSTANTIATE COHORT
    public void instantiateCohort(){
        int row = 0;
        int kolonne = 0;
        for(int i = 1; i < cohortLimitSize; i++){
            int difference = cohortLimitSize;
            if(cohortLimitSize % i == 0){
                if(kolonne - row < difference && cohortLimitSize/i >= i){
                    kolonne = cohortLimitSize/i;
                    row = i;
                    difference = kolonne - row;
                }
            }
        }

        GameObject cohort = (GameObject)Instantiate(cohortPrefab, transform);
        cohort.GetComponent<BoxCollider>().size = new Vector3(kolonne, 2, row);

        for(int i = 0; i > -row; i--){
            for(int y = 0; y < kolonne; y++){
                GameObject soldierPosition = (GameObject)Instantiate(soldierPositionPrefab, cohort.transform);
                float extraX = 0;
                if(kolonne % 2 == 0){
                    extraX = (kolonne / 2) - 0.5f; 
                } else {
                    extraX = (int) kolonne/2;
                }
                soldierPosition.transform.localPosition = new Vector3(y - extraX, 0, i + (row/2));
            }
        }
    }
}
