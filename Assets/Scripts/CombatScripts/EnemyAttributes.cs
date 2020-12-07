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
    public Inventory inventory;
    public Toolbelt toolbelt;
    public Skills skills;
    public Dictionary<Dictionary<string, int>, float> droptable;
    public string enemyName;
    public string description;
    public bool aggressive;
    public string type;
    public int level;
    public int health;
    public int armor;
    public int ranged;
    public int magic;
    public int melee;
    public int attackSpeed;
    public float critChance;
    public float attackRange;
    public int movementspeed;
    public int inventoryCapacity;
    public int frostResistance;
    public int fireRestistance;
    public bool attackCooldown;
    public bool walkingToClosestEnemy;
    public bool attackingEnemy;
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        agent = this.transform.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if(GetComponent<Toolbelt>()){
            toolbelt = GetComponent<Toolbelt>();
        }
        if(GetComponent<Skills>()){
            skills = GetComponent<Skills>();
        }

        enemyCatalog = gameManager.getEnemyCatalog();
        enemyName = enemyCatalog.getNameContains(transform.name);
        transform.name = enemyName;
        if(enemyCatalog.getEnemyByName(enemyName) != null){
            description = enemyCatalog.getEnemyByName(enemyName).getDescription();
            type = enemyCatalog.getEnemyByName(enemyName).getType();
            level = enemyCatalog.getEnemyByName(enemyName).getLevel();
            health = enemyCatalog.getEnemyByName(enemyName).getBaseHealth();
            ranged = enemyCatalog.getEnemyByName(enemyName).getRanged();
            magic = enemyCatalog.getEnemyByName(enemyName).getMagic();
            melee = enemyCatalog.getEnemyByName(enemyName).getMelee();
            armor = enemyCatalog.getEnemyByName(enemyName).getBaseArmor();
            movementspeed = enemyCatalog.getEnemyByName(enemyName).getBaseMovementSpeed();
            agent.speed = movementspeed;
            attackSpeed = enemyCatalog.getEnemyByName(enemyName).getAttackSpeed();
            critChance = enemyCatalog.getEnemyByName(enemyName).getBaseCritChange();
            attackRange = enemyCatalog.getEnemyByName(enemyName).getAttackRange();
            frostResistance = enemyCatalog.getEnemyByName(enemyName).getFrostResistance();
            fireRestistance = enemyCatalog.getEnemyByName(enemyName).getFireResistance();
            aggressive = enemyCatalog.getEnemyByName(enemyName).getAggressive();
            droptable = enemyCatalog.getEnemyByName(enemyName).getDroptable();
            inventory = transform.GetComponent<Inventory>();
            abilities = enemyCatalog.getEnemyByName(enemyName).getAbilities();
        }
    }

    void Update()
    {
        checkMovement();
        if(enemiesInRange.Count > 0 && this.transform.tag != "player"){
            enemiesInRange.RemoveAll(item => item == null);
            StartCoroutine(walkToClosestEnemy());
        }
    }

    void OnMouseDown()
    {
        if(transform.tag != "player"){
            StartCoroutine(gameManager.getPlayerBehavior().transform.GetComponent<EnemyAttributes>().walkToEnemy(this));
        }
    }

    void OnMouseEnter()
    {
        // INDICATORS
        setMobIndicator(true);
        if(aggressive){
            setAllIndicatorsFallsAndActivate("red");
        } 
        if(!aggressive){
            setAllIndicatorsFallsAndActivate("green");
        }
    }

    void OnMouseExit()
    {
        // INDICATORS
        setMobIndicator(false);
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

    void OnDestroy()
    {
        // ADD THAT IF AMOUNT OF INVENTORYSLOTS WITH ITEM IN IT IS MORE THAN 3,4 SPAWN SACK OF ITEMS
        if(health <= 0){
            foreach(InventorySlot inventorySlot in inventory.getInventorySlots()){
                if(gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()) != null){
                    GameObject item = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getItemPrefab();
                    item.transform.name = inventorySlot.getItemInSlot();
                    item.transform.GetComponent<ItemAttributes>().setItemName(inventorySlot.getItemInSlot());
                    item.transform.GetComponent<ItemAttributes>().setItemAmount(inventorySlot.getCurrentAmountInSlot());
                    Instantiate(item, new Vector3(this.transform.position.x, 1, this.transform.position.z), Quaternion.identity);
                }
            }
        }
    
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
                enemy = getClosestEnemyInRange();

                if(enemy == null || !enemiesInRange.Contains(enemy)){
                    //Debug.Log("No enemy found");
                    stopMovement();
                    break;
                }
                
                if(Vector3.Distance(this.getPosition(), enemy.getPosition()) < attackRange){
                    //Debug.Log("clsoe enough");
                    setCurrentTarget(enemy);
                    StartCoroutine(attackEnemy(enemy));
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

    // THIS ONE IS FOR PLAYER
    public IEnumerator walkToEnemy(EnemyAttributes enemy){
        bool walkingToEnemy = true;
        EnemyAttributes player = this;
        Vector3 hitGroundPosition = gameManager.getPlayerBehavior().getHitGround();


        while(walkingToEnemy){   
            enemy.setMobIndicator(true);
            enemy.setAllIndicatorsFallsAndActivate("yellow");
            //Debug.Log("player WALKINMG to enemy");

            if(hitGroundPosition != gameManager.getPlayerBehavior().getHitGround()){
                //Debug.Log("new walking position");
                enemy.setMobIndicator(false);
                enemy.setAllIndicatorsFallsAndActivate("red");
                break;
            }

            if(Vector3.Distance(gameManager.getPlayerBehavior().getPlayerPosition(), enemy.getPosition()) < player.getAttackRange()){
                //Debug.Log("clsoe enough");
                stopMovement();
                player.setCurrentTarget(enemy);
                StartCoroutine(attackEnemy(enemy, true));

                break;
            } else {
                player.getNavMeshAgent().SetDestination(enemy.getPosition());
            }
            yield return null;
        }
    }

    public void setCurrentTarget(EnemyAttributes enemy){
        currentTarget = enemy;
    }

    public IEnumerator attackEnemy(EnemyAttributes enemy){
        if(!attackingEnemy){
            attackingEnemy = true;
            lookAtTargetEnemy(enemy);

            while(attackingEnemy){
                //Debug.Log("attacking enemy running");

                if(Vector3.Distance(this.transform.position, enemy.getPosition()) > attackRange){
                    //Debug.Log("Lost target, or distance to high to attack");
                    //enemy.setHealthbar(false);
                    break;
                }

                if(enemy == null){
                    stopMovement();
                    //Debug.Log("Current target is null");
                    setHealthbar(false);
                    break;
                } else {
                    //enemy.setHealthbar(true);
                }

                this.dealDamage(enemy);
                yield return null;
            }
            attackingEnemy = false;
        }
    }

    // PLAYER
    public IEnumerator attackEnemy(EnemyAttributes enemy, bool player){
        EnemyAttributes playerEnemy = gameManager.getPlayerBehavior().transform.GetComponent<EnemyAttributes>();
        Vector3 hitGroundPosition = gameManager.getPlayerBehavior().getHitGround();

        //Debug.Log("start attacking");
        if(!playerEnemy.getIsAttackingEnemy() && enemy != this){
            playerEnemy.setIsAttackigEnemy(true);
            playerEnemy.lookAtTargetEnemy(enemy);

            while(playerEnemy.getIsAttackingEnemy()){

                if(hitGroundPosition != gameManager.getPlayerBehavior().getHitGround()){
                    //Debug.Log("not attacking anymore because player clicked elsewhere");
                    enemy.setMobIndicator(false);
                    enemy.setHealthbar(false);
                    enemy.setAllIndicatorsFallsAndActivate("red");
                    break;
                }
                if(enemy == null){
                    break;
                } else {
                    enemy.setHealthbar(true);
                    enemy.setMobIndicator(true);
                    enemy.setAllIndicatorsFallsAndActivate("yellow");
                    //Debug.Log("attacking enemy running");
                }

                if(!playerEnemy.getAttackCooldown()){
                    playerEnemy.dealDamage(enemy);
                }
                yield return null;
            }
            //Debug.Log("end attacking");
            playerEnemy.setIsAttackigEnemy(false);
        }
    }


    public IEnumerator activateAttackCooldown(){
        attackCooldown = true;
        yield return new WaitForSeconds((float)10/attackSpeed);
        attackCooldown = false;
    }

    public void dealDamage(EnemyAttributes enemy){
        if(!attackCooldown){
            GetComponent<Animator>().SetTrigger("punch");
            StartCoroutine(activateAttackCooldown());
            //Debug.Log("punch");
            int damageToDeal = melee;
            enemy.takeDamage(melee);
        }
    }

    public void takeDamage(int amount){
        //Debug.Log(transform.name +" took " + amount + " damaage");
        health -= amount;
        checkIfDead();
        if(transform.tag.Equals("player")){
            gameManager.GetUI().updateBarsInterface();
        } else {
            updateHealthbar(this);
        }
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
        if(this != null){
            return this.transform.position;
        } 
        return Vector3.zero;
    }

    public void lookAtTargetEnemy(EnemyAttributes enemy){
        transform.LookAt(new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z));
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
        if(this != null){
            if(health <= 0){
                Debug.Log(transform.name + " died!");
                Destroy(transform.gameObject);
            }
        }
    }

    public void addAbilityToEnemy(Ability ability){
        if(!abilities.Contains(ability)){
            abilities.Add(ability);
        }
    }

    public void setMobIndicator(bool val){
        transform.Find("mob_indicator").gameObject.SetActive(val);
    }
    public void setIndicatorGreen(bool val){
        transform.Find("mob_indicator/green_indicator").gameObject.SetActive(val);
    }
    public void setIndicatorRed(bool val){
        transform.Find("mob_indicator/red_indicator").gameObject.SetActive(val); 
    }
    public void setIndicatorYellow(bool val){
        transform.Find("mob_indicator/yellow_indicator").gameObject.SetActive(val);
    }
    public void setAllIndicatorsFallsAndActivate(string colorName){
        setIndicatorGreen(false);
        setIndicatorRed(false);
        setIndicatorYellow(false);

        if(colorName.Equals("green")){
            setIndicatorGreen(true);
        }
        if(colorName.Equals("red")){
            setIndicatorRed(true);
        }
        if(colorName.Equals("yellow")){
            setIndicatorYellow(true);
        }
    }
    public void setHealthbar(bool val){
        if(this != null){
            transform.Find("Healthbar").gameObject.SetActive(val);
        }
    }
    public void updateHealthbar(EnemyAttributes enemy){
        enemy.transform.Find("Healthbar").GetComponent<Healthbar>().updateHealthBar();
    }

    public bool increaseCurrentHealth(int amount){
        if(health < getMaxHealth()){
            if(amount + health >= getMaxHealth()){
                health += getMaxHealth() - health;
            } else {
                health += amount;
            }
            return true;
        }
        return false;
    }

    public NavMeshAgent getNavMeshAgent(){
        return agent;
    }
    public bool getWalkingToClosestEnemy(){
        return walkingToClosestEnemy;
    }
    public bool getIsAttackingEnemy(){
        return attackingEnemy;
    }
    public void setIsAttackigEnemy(bool val){
        attackingEnemy = val;
    }
    public bool getAttackCooldown(){
        return attackCooldown;
    }

    public Dictionary<Dictionary<string, int>, float> getDroptable(){
        return droptable;
    }
    public void setMovementSpeed(int speed){
        movementspeed = speed;
        agent.speed = movementspeed;
    }



    public int getLevel(){
        if(skills){
            level = 
                (skills.getSkillByName("Magic").getLevel() + 
                skills.getSkillByName("Ranged").getLevel() + 
                skills.getSkillByName("Melee").getLevel() +
                skills.getSkillByName("Resiliance").getLevel())/4;
        } else {
            return level;
        }
        return level;
    }

    public int getMaxHealth(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);
        int maxHealth = enemy.getBaseHealth();
        if(toolbelt){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(inventorySlot.getItemInSlot() != null){
                    if(gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment() != null && inventorySlot.getInventorySlotType().Equals("Food")){
                        maxHealth += gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getHealth();
                    }
                }
            }
        }
        return maxHealth;
    }

    public int getHealth(){
        return health;
    }

    public int getArmor(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);
        armor = enemy.getBaseArmor();
        foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
            if(inventorySlot.getItemInSlot() != null){
                if(gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment() != null){
                    armor += gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getArmor();
                }
            }
        }
        return armor;
    }

    public int getRanged(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);

        if(toolbelt.getCurrentlySelectedSlot() != null){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(toolbelt.getCurrentlySelectedSlot() != null){
                    if(toolbelt.getCurrentlySelectedSlot().Equals(inventorySlot)){
                        // RETURNS CURRENTLY SELECTED ELEMENT IN TOOLBARS ATTACKSPEED
                        if(inventorySlot.getItemInSlot() != null){
                            return ranged = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getRanged();
                        }
                    }
                }
            }
        }

        // RETURNS STANDARD ATTACK SPEED VALUE FOR NPC
        return ranged = enemy.getRanged();
    }

    public int getMagic(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);

        if(toolbelt.getCurrentlySelectedSlot() != null){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(toolbelt.getCurrentlySelectedSlot() != null){
                    if(toolbelt.getCurrentlySelectedSlot().Equals(inventorySlot)){
                        // RETURNS CURRENTLY SELECTED ELEMENT IN TOOLBARS ATTACKSPEED
                        if(inventorySlot.getItemInSlot() != null){
                            return magic = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getMagic();
                        }
                    }
                }
            }
        }

        // RETURNS STANDARD ATTACK SPEED VALUE FOR NPC
        return magic = enemy.getMagic();
    }

    public int getMelee(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);

        if(toolbelt.getCurrentlySelectedSlot() != null){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(toolbelt.getCurrentlySelectedSlot() != null){
                    if(toolbelt.getCurrentlySelectedSlot().Equals(inventorySlot)){
                        // RETURNS CURRENTLY SELECTED ELEMENT IN TOOLBARS ATTACKSPEED
                        if(inventorySlot.getItemInSlot() != null){
                            return melee = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getMelee();
                        }
                    }
                }
            }
        }

        // RETURNS STANDARD ATTACK SPEED VALUE FOR NPC
        return melee = enemy.getMelee();
    }

    // SAME AS MELEE?
    public int getAttackSpeed(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);

        if(toolbelt.getCurrentlySelectedSlot() != null){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(toolbelt.getCurrentlySelectedSlot() != null){
                    if(toolbelt.getCurrentlySelectedSlot().Equals(inventorySlot)){
                        // RETURNS CURRENTLY SELECTED ELEMENT IN TOOLBARS ATTACKSPEED
                        if(inventorySlot.getItemInSlot() != null){
                            return attackSpeed = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getAttackSpeed();
                        }
                    }
                }
            }
        }

        // RETURNS STANDARD ATTACK SPEED VALUE FOR NPC
        return attackSpeed = enemy.getAttackSpeed();
    }

    // SAME AS MELEE?
    public float getCritChance(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);

        if(toolbelt.getCurrentlySelectedSlot() != null){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(toolbelt.getCurrentlySelectedSlot().Equals(inventorySlot)){
                    // RETURNS CURRENTLY SELECTED ELEMENT IN TOOLBARS CRITCHANCE
                    if(inventorySlot.getItemInSlot() != null){
                        return critChance = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getCritChance();
                    }
                }
            }
        }

        // RETURNS STANDARD CRIT CHANCE VALUE FOR NPC
        return critChance = enemy.getBaseCritChange();
    }

    // SAME AS MELEE?
    public float getAttackRange(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);

        if(toolbelt.getCurrentlySelectedSlot() != null){
            foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
                if(toolbelt.getCurrentlySelectedSlot().Equals(inventorySlot)){
                    // RETURNS CURRENTLY SELECTED ELEMENT IN TOOLBARS CRITCHANCE
                    if(inventorySlot.getItemInSlot() != null){
                        return attackRange = gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getAttackRange();
                    }
                }
            }
        }

        // RETURNS STANDARD CRIT CHANCE VALUE FOR NPC
        return attackRange = enemy.getAttackRange();
    }

    public int getMovementSpeed(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);
        movementspeed = enemy.getBaseMovementSpeed();
        foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
            if(inventorySlot.getItemInSlot() != null){
                if(gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment() != null){
                    movementspeed += gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getMovementSpeed();
                }
            }
        }
        setMovementSpeed(movementspeed);
        return movementspeed;
    }

    public int getInventoryCapacity(){
        return inventory.getInventoryCapacity();
    }    

    public int getFrostResistance(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);
        frostResistance = enemy.getFrostResistance();
        foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
            if(inventorySlot.getItemInSlot() != null){
                if(gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment() != null){
                    frostResistance += gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getFrostResistance();
                }
            }
        }
        return frostResistance;
    }

    public int getFireResistance(){
        Enemy enemy = gameManager.getEnemyCatalog().getEnemyByName(name);
        fireRestistance = enemy.getFireResistance();
        foreach(InventorySlot inventorySlot in toolbelt.getToolbar()){
            if(inventorySlot.getItemInSlot() != null){
                if(gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment() != null){
                    fireRestistance += gameManager.getItemCatalog().getItemByName(inventorySlot.getItemInSlot()).getEquipment().getFireResistance();
                }
            }
        }
        return fireRestistance;
    }

}
