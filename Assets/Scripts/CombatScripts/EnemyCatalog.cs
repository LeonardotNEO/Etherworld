using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyCatalog : MonoBehaviour
{
    public List<Enemy> enemyCatalog = new List<Enemy>();
    public List<EnemyAttributes> enemiesInGame = new List<EnemyAttributes>();
    public EnemyAttributes enemyCurrentlySelected;

    void Awake()
    {
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Bandit",
                /*Desription*/      "Watch out so you dont get robbed",
                /*Type*/            "Human",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*Ranged*/          0,        
                /*Magic*/           0,        
                /*Melee*/           8,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    4,
                /*BaseAttackSpeed*/ 10,
                /*Frostresistance*/ 8,
                /*Fireresistance*/  5,
                /*Aggressive*/      true,
                /*Abilities*/       new List<string>(){},
                /*Droptable*/       new Dictionary<Dictionary<string, int>, float>(){
                    {new Dictionary<string, int>{{"Wood log", 1}}, 0.25f},
                    {new Dictionary<string, int>{{"Ether pickaxe", 1}}, 0.25f},
                    {new Dictionary<string, int>{{"Bucket of water", 1}}, 0.25f},
                    {new Dictionary<string, int>{{"Gold ore", 1}}, 0.25f},
                    {new Dictionary<string, int>{{"Iron bar", 1}}, 0.25f},
                    {new Dictionary<string, int>{{"Wood hammer", 1}}, 0.25f},
                    }
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Wizard",
                /*Desription*/      "Watch out so you dont get robbed",
                /*Type*/            "Human",
                /*Level*/           5,
                /*baseHealth*/      100,  
                /*Ranged*/          0,        
                /*Magic*/           8,        
                /*Melee*/           0,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    4,
                /*BaseAttackSpeed*/ 10,
                /*Frostresistance*/ 8,
                /*Fireresistance*/  5,
                /*Aggressive*/      true,
                /*Abilities*/       new List<string>(){"Firebolt"},
                /*Droptable*/       new Dictionary<Dictionary<string, int>, float>(){
                    {new Dictionary<string, int>{{"Wood plank", 1}}, 0.50f},
                    {new Dictionary<string, int>{{"Stone", 1}}, 0.50f},
                    {new Dictionary<string, int>{{"Gold coin", 1}}, 0.1f},
                    {new Dictionary<string, int>{{"Silver coin", 5}}, 0.30f},
                    }
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "player",
                /*Desription*/      "Player character",
                /*Type*/            "Human",
                /*Level*/           1,
                /*baseHealth*/      10000,  
                /*Ranged*/          0,        
                /*Magic*/           0,        
                /*Melee*/           0,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    8,
                /*BaseAttackSpeed*/ 20,
                /*Frostresistance*/ 5,
                /*Fireresistance*/  5,
                /*Aggressive*/      false,
                /*Abilities*/       new List<string>(){},
                /*Droptable*/       null
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Citizen",
                /*Desription*/      "A normal citizen",
                /*Type*/            "Human",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*Ranged*/          0,        
                /*Magic*/           8,        
                /*Melee*/           0,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    8,
                /*BaseAttackSpeed*/ 10,
                /*Frostresistance*/ 8,
                /*Fireresistance*/  5,
                /*Aggressive*/      false,
                /*Abilities*/       new List<string>(){},
                /*Droptable*/       null
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Troll",
                /*Desription*/      "A chuncky booii",
                /*Type*/            "Troll",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*Ranged*/          0,        
                /*Magic*/           8,        
                /*Melee*/           0,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    8,
                /*BaseAttackSpeed*/ 5,
                /*Frostresistance*/ 8,
                /*Fireresistance*/  5,
                /*Aggressive*/      false,
                /*Abilities*/       new List<string>(){},
                /*Droptable*/       new Dictionary<Dictionary<string, int>, float>(){
                    {new Dictionary<string, int>{{"Wood plank", 1}}, 0.50f},
                    {new Dictionary<string, int>{{"Stone", 1}}, 0.50f},
                    {new Dictionary<string, int>{{"Gold coin", 1}}, 0.1f},
                    {new Dictionary<string, int>{{"Silver coin", 5}}, 0.30f},
                    }
            )
        );
    }

    public Enemy getEnemyByName(string name){
        foreach(Enemy enemy in enemyCatalog){
            if(enemy.getName().Equals(name)){
                return enemy;
            }
        }
        return null;
    }
    public EnemyAttributes getEnemyCurrentlySelected(){
        return enemyCurrentlySelected;
    }
    public void setEnemyCurrentlySelected(EnemyAttributes enemy){
        enemyCurrentlySelected = enemy;
    }
     public void addEnemyToGame(Enemy enemy){
        enemyCatalog.Add(enemy);
    }
    public void removeEnemyFromGame(Enemy enemy){
        enemyCatalog.Remove(enemy);
    }
    public void addEnemyToWorld(EnemyAttributes enemy){
        enemiesInGame.Add(enemy);
    }
    public void removeEnemyFromWorld(EnemyAttributes enemy){
        enemiesInGame.Remove(enemy);
    }

    public string getNameContains(string name){
        foreach(Enemy enemy in enemyCatalog){
            if(name.Contains(enemy.getName())){
                return enemy.getName();
            }
        }
        return null;
    }
}
