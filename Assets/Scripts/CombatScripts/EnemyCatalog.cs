using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyCatalog : MonoBehaviour
{
    private List<Enemy> enemyCatalog = new List<Enemy>();
    private List<EnemyAttributes> enemiesInGame = new List<EnemyAttributes>();
    private EnemyAttributes enemyCurrentlySelected;

    void Awake()
    {
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Bandit",
                /*Desription*/      "Watch out so you dont get robbed",
                /*Type*/            "Human",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*baseDamage*/      8,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    4,
                /*BaseAttackSpeed*/ 10,
                /*Aggressive*/      true
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "player",
                /*Desription*/      "Player character",
                /*Type*/            "Human",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*baseDamage*/      2,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    8,
                /*BaseAttackSpeed*/ 10,
                /*Aggressive*/      false
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Citizen",
                /*Desription*/      "A normal citizen",
                /*Type*/            "Human",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*baseDamage*/      2,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    8,
                /*BaseAttackSpeed*/ 10,
                /*Aggressive*/      false
            )
        );
        addEnemyToGame(
            new Enemy(
                /*Name*/            "Troll",
                /*Desription*/      "A chuncky booii",
                /*Type*/            "Troll",
                /*Level*/           1,
                /*baseHealth*/      100,  
                /*baseDamage*/      8,        
                /*baseCrit*/        1.01f,
                /*Attackrange*/     1.5f,
                /*BaseArmor*/       1,
                /*BaseMovement*/    8,
                /*BaseAttackSpeed*/ 5,
                /*Aggressive*/      false
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
