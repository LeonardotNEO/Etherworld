using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAttributes : MonoBehaviour
{
    public GameManager gameManager;
    public NavMeshAgent agent;
    public EnemyCatalog enemyCatalog;
    public EnemyAttributes currentTarget;
    public List<EnemyAttributes> enemiesInRange;
    public Vector3 position;
    public List<Ability> abilities;
    public string enemyName;
    public string description;
    public string type;
    public int level;
    public int health;
    public int damage;
    public int armor;
    public int movementspeed;
    public int attackSpeed;
    public float critChance;
    public float attackRange;
    public bool aggressive;
    public bool attackCooldown;
    public bool enemyCloseEnoughToAttack;
    public bool walkingToClosestEnemy;
    public bool followingEnemyClosely;
    public bool attackingEnemy;
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        enemyCatalog = gameManager.getEnemyCatalog();
        agent = this.transform.GetComponent<NavMeshAgent>();

        
        enemyName = enemyCatalog.getNameContains(transform.name);
        transform.name = enemyName;
        if(enemyCatalog.getEnemyByName(enemyName) != null){
            description = enemyCatalog.getEnemyByName(enemyName).getDescription();
            type = enemyCatalog.getEnemyByName(enemyName).getType();
            level = enemyCatalog.getEnemyByName(enemyName).getLevel();
            health = enemyCatalog.getEnemyByName(enemyName).getBaseHealth();
            damage = enemyCatalog.getEnemyByName(enemyName).getBaseDamage();
            armor = enemyCatalog.getEnemyByName(enemyName).getBaseArmor();
            movementspeed = enemyCatalog.getEnemyByName(enemyName).getBaseMovementSpeed();
            agent.speed = movementspeed;
            attackSpeed = enemyCatalog.getEnemyByName(enemyName).getAttackSpeed();
            critChance = enemyCatalog.getEnemyByName(enemyName).getBaseCritChange();
            attackRange = enemyCatalog.getEnemyByName(enemyName).getAttackRange();
            aggressive = false;
            abilities = enemyCatalog.getEnemyByName(enemyName).getAbilities();
        }
    }

    void Update()
    {
        checkMovement();
    }


    public void updatePosition(){
        if(position != this.transform.position){
            position = this.transform.position;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other is BoxCollider){
            if(other.transform.GetComponent<EnemyAttributes>() != null && !enemiesInRange.Contains(other.transform.GetComponent<EnemyAttributes>())){
                if(other.transform.GetComponent<EnemyAttributes>().getName() != null && !other.transform.GetComponent<EnemyAttributes>().getName().Equals(this.getName())){
                    addEnemyToEnemyInRange(other.transform.GetComponent<EnemyAttributes>());
                    if(enemiesInRange.Count > 0){
                        StartCoroutine(walkToClosestEnemy());
                    }
                }
            }
        }
        if(other is CapsuleCollider){
            if(other.transform.GetComponent<EnemyAttributes>() != null){
                //addEnemyToEnemyInRange(other.transform.GetComponent<EnemyAttributes>());
            }
        }
            
    }
    void OnTriggerExit(Collider other)
    {
        if(other is BoxCollider){
            if(other.transform.GetComponent<EnemyAttributes>() != null && Vector3.Distance(getPosition(), other.transform.GetComponent<EnemyAttributes>().getPosition()) > attackRange){
                removeEnemyFromEnemyInRange(other.transform.GetComponent<EnemyAttributes>());
            }
        }
        if(other is CapsuleCollider){
            if(other.transform.GetComponent<EnemyAttributes>() != null){
                //addEnemyToEnemyInRange(other.transform.GetComponent<EnemyAttributes>());
            }
        }
    }

    void OnDisable()
    {
        removeEnemyFromEnemyInRange(this);
    }

    public IEnumerator waitUntilEnemyAttributesLoad(){
        yield return new WaitForSeconds(0.5f);
    }

    public void addEnemyToEnemyInRange(EnemyAttributes enemy){
        enemiesInRange.Add(enemy);
    }
    public void removeEnemyFromEnemyInRange(EnemyAttributes enemy){
        enemiesInRange.Remove(enemy);
    }

    public EnemyAttributes getClosestEnemyInRange(){
        if(enemiesInRange.Count != 0){
            List<EnemyAttributes> enemiesArranged = new List<EnemyAttributes>();
            Dictionary<EnemyAttributes, float> enemiesAndDistance = new Dictionary<EnemyAttributes, float>();

            foreach(EnemyAttributes enemy in enemiesInRange){
                if(enemy){
                    enemiesAndDistance.Add(enemy, Vector3.Distance(this.transform.position, enemy.transform.position));
                }
            }
            foreach(KeyValuePair<EnemyAttributes, float> enemy in enemiesAndDistance.OrderBy(key => key.Value)){
                enemiesArranged.Add(enemy.Key);
            }
            if(enemiesArranged.Count != 0){
                return enemiesArranged[0];
            }
        }
        return null;
    }

    public IEnumerator walkToClosestEnemy(){
        if(!walkingToClosestEnemy){
            walkingToClosestEnemy = true;
            EnemyAttributes enemy = getClosestEnemyInRange();

            while(walkingToClosestEnemy){
                //Debug.Log("walking running");
                enemy = getClosestEnemyInRange();

                if(enemy == null || !enemiesInRange.Contains(enemy)){
                    //Debug.Log("No enemy found");
                    stopMovement();
                    break;
                }
                
                if(Vector3.Distance(this.getPosition(), enemy.getPosition()) < attackRange){
                    //Debug.Log("clsoe enough");
                    currentTarget = enemy;
                    StartCoroutine(attackEnemy());
                    break;
                } else {
                    agent.SetDestination(enemy.getPosition());
                }

                yield return null;
            }
            stopMovement();
            walkingToClosestEnemy = false;
        }
    }

    public void setCurrentTarget(EnemyAttributes enemy){
        currentTarget = enemy;
    }

    public IEnumerator attackEnemy(){
        if(!attackingEnemy){
            attackingEnemy = true;

            while(attackingEnemy){
                //Debug.Log("attacking enemy running");

                if(Vector3.Distance(this.transform.position, currentTarget.getPosition()) > attackRange){
                    //Debug.Log("Lost target, or distance to high to attack");
                    break;
                }
                if(currentTarget == null){
                    stopMovement();
                    break;
                }

                StartCoroutine(dealDamage(currentTarget));
                yield return null;
            }
            attackingEnemy = false;
        }
    }

    public IEnumerator dealDamage(EnemyAttributes enemy){
        if(!attackCooldown){
            lookAt(enemy.transform.gameObject);
            attackCooldown = true;
            GetComponent<Animator>().SetTrigger("punch");
            //Debug.Log("punch");

            int damageToDeal = damage;
            enemy.takeDamage(damage);
            yield return new WaitForSeconds(10/attackSpeed);
            GetComponent<Animator>().SetTrigger("punch");
            attackCooldown = false;
        }
    }

    public void takeDamage(int amount){
        //Debug.Log(transform.name +" took " + amount + " damaage");
        health -= amount;
        checkIfDead();
    }

    public void checkMovement(){
        if(GetComponent<NavMeshAgent>().enabled == true){
            if(agent.remainingDistance <= 0.2){
                GetComponent<Animator>().SetBool("isMoving" , false);
            } else {
                GetComponent<Animator>().SetBool("isMoving" , true);
            }
        }
    }

    public void stopMovement(){
        if(GetComponent<NavMeshAgent>()){
            agent.ResetPath();
        }
    }

    public Vector3 getPosition(){
        return this.transform.position;
    }

    public void lookAtTargetEnemy(EnemyAttributes enemy){

    }

    public bool getIsAggressive(){
        return aggressive;
    }

    public string getType(){
        return type;
    }

    public string getName(){
        return name;
    }

    public void checkIfDead(){
        if(health <= 0){
            die();
        }
    }
    public void die(){
        if(health <= 0){
            Debug.Log(transform.name + " died!");
            Destroy(transform.gameObject);
        }
    }

    public void lookAt(GameObject lookAt){
        // FIX THIS
        //transform.LookAt(new Vector3(lookAt.transform.position.x, 0, lookAt.transform.position.z));
    }

    public void addAbilityToEnemy(Ability ability){
        if(!abilities.Contains(ability)){
            abilities.Add(ability);
        }
    }
}
