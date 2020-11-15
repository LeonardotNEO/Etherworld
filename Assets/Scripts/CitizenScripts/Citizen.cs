using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{
    GameManager gameManager;
    public Vector3 position;
    NavMeshAgent citizenAgent;
    public List<Skill> skills = new List<Skill>();
    public List<Citizen> relatives = new List<Citizen>();
    public Inventory inventory;
    public BuildingAttributes house = null;
    public BuildingAttributes work = null;
    public BuildingAttributes buildingInsideOf = null;
    public Town townAlliegence;
    public Town townCurrentlyInsideOf;
    public string gender;
    public string citizenName;
    public string status;
    public string citizensLord;
    public string job;

    public int age;
    public int health;
    public int hunger;
    public int thirst;
    public int happiness;
    public int movementSpeed;
    public int wealth;
    public int taxPayment;
    public int citizenID;
    //public List<Hobby> hobbies;
    //public List<Personalities> personalityTraits;
    //public List<Ambition> ambitions; 

    // BOOLS //
    public bool initalizedCitizen;
    public bool isMovingToDestination;
    public bool reachedDestination;
    public bool isMovingToBuilding;
    public bool movedToBuilding;
    public bool isMovingToHome;
    public bool movedToHome;
    public bool isMovingToWork;
    public bool movedToWork;
    public bool isInsideBuilding;
    public bool isLookingForWork;
    public bool isLookingForHouse;
    public bool puttingItemInStorage;
    public bool newAction;

    public bool isResting;

    public bool isWorking;

    public bool isGatheringResources;
    public bool isSick;

    public bool isSleepy;
    public bool isLookingForPartner;
    public bool isDoingRandomAction;
    public bool isMating;
    public bool isInTheArmy;

    public bool hour0;
    public bool hour1;
    public bool hour2;
    public bool hour3;
    public bool hour4;
    public bool hour5;
    public bool hour6;
    public bool hour7;
    public bool hour8;
    public bool hour9;
    public bool hour10;
    public bool hour11;
    public bool hour12;
    public bool hour13;
    public bool hour14;
    public bool hour15;
    public bool hour16;
    public bool hour17;
    public bool hour18;
    public bool hour19;
    public bool hour20;
    public bool hour21;
    public bool hour22;
    public bool hour23;


    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        citizenAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(initialisingCitizen());
    }

    void Update()
    {
        if(initalizedCitizen){
            checkCitizenMovement();

            switch(gameManager.getClock().getHours()){
                case 0:
                    // Dont reset while testing working()
                    resetHourTriggers();
                    break;
                case 3:
                    if(gameManager.getClock().getMinutes() == 0 && !hour3){ 
                        degenerateHunger();
                        degenerateThirst();
                        hour3 = true;
                    }
                    break;
                case 2:
                    if(gameManager.getClock().getMinutes() == 0 && !hour6){ 
                        degenerateHunger();
                        degenerateThirst();
                        hour6 = true;
                    }
                    break;
                case 9:
                    /*if(gameManager.getClock().getMinutes() == 0 && !hour9){ 
                        StartCoroutine(putItemInStorage("Stone", 20));  
                        degenerateHunger();
                        degenerateThirst();
                        hour9 = true;
                    }*/
                    break;
                case 12:
                    /*if(gameManager.getClock().getMinutes() == 0 && !hour12){
                        StartCoroutine(goToBuilding(work));  
                        degenerateHunger();
                        degenerateThirst();
                        hour12 = true;
                    }*/
                    break;
                case 15:
                    /*if(gameManager.getClock().getMinutes() == 0 && !hour15){ 
                        StartCoroutine(putItemInStorage("Wood log", 20)); 
                        degenerateHunger();
                        degenerateThirst();
                        hour15 = true;
                    }*/
                    break;
                case 18:
                    /*if(gameManager.getClock().getMinutes() == 0 && !hour18){ 
                        StartCoroutine(goToBuilding(work)); 
                        degenerateHunger();
                        degenerateThirst();
                        hour18 = true;
                    }*/
                    break;
                case 21:
                    /*if(gameManager.getClock().getMinutes() == 0 && !hour21){ 
                        StartCoroutine(putItemInStorage("Iron", 20)); 
                        degenerateHunger();
                        degenerateThirst();
                        hour21 = true;
                    }*/
                    break;
                default:
                    break;
            }

            StartCoroutine(working()); 

            if(work == null){
                lookForWork();
            } else {
                foundWork();
            }

            if(house == null){
                lookForHousing();
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<Town>()){
            setTownCurrentlyInsideOf(other.gameObject.GetComponent<Town>());
            StartCoroutine(waitForCitizenToLoadThenAdd(other));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Town>()){
            setTownCurrentlyInsideOf(null);
        }
    }

    void OnDisable()
    {
        if(house){
            house.removeResidentFromBuilding(this);
        }
        if(work){
            work.removeWorkerFromBuilding(this);
        }
        if(townAlliegence){
            townAlliegence.removeInventoryFromTown(inventory);
        }
        gameManager.getCitizenCatalog().removeCitizenFromAllCitizens(this);
    }

    public IEnumerator initialisingCitizen(){
        yield return new WaitForSeconds(0.1f);

        // GAMEMANAGER AND ID
        citizenID = gameManager.getCitizenCatalog().getAmountOfCitizens();
        gameManager.getCitizenCatalog().addCitizenToAllCitizens(this);

        // GENDER
        List<string> genders = new List<string>{"Male", "Female"};
        gender = genders[Random.Range(0,genders.Count)];

        // NAME
        List<string> maleNames = new List<string>{"Hans","Peter","Ole","Leonard","Fredrik","Markus","Chad","Rick","Morty","Erlend","James","Bertil"};
        List<string> femaleNames = new List<string>{"Siri","Grete","Karen","Martha","Marte","Monica"};
        if(gender == "Male"){
            citizenName = maleNames[Random.Range(0, maleNames.Count)];
        } else {
            citizenName = femaleNames[Random.Range(0, femaleNames.Count)];
        }
        transform.name = citizenName + " " + citizenID;

        // AGE
        age = Random.Range(16,60);

        // SKILLS

        // INVENTORY
        inventory = transform.GetComponent<Inventory>();

        // MODIFIERS
        health = 100;
        hunger = 100;
        thirst = 100;
        happiness = 100;

        // MOVEMENTSPEED
        movementSpeed = Random.Range(5,12); 
        citizenAgent.speed = movementSpeed;

        // STATUS
        status = null;

        // TOWN ALLIEGENCE AND LORD ALLIEGENCE
        if(house == null){
            townAlliegence = null;
        } else {
            if(house.getTownBuildingIsApartOf()){
                townAlliegence = house.getTownBuildingIsApartOf();
            }
        }
        if(townAlliegence){
            citizensLord = townAlliegence.getTownOwner();
        } else {
            citizensLord = null;
        }
        
        // WEALTH
        wealth = Random.Range(0,100);

        // POSITION
        position = transform.position;

        // ADD ITEMS TO INVENTORY
        Dictionary<string,int> listOfItems = new Dictionary<string, int>{
        {"Wood plank", 99}, 
        {"Stone", 99}, 
        {"Wood log", 99}, 
        {"Iron ore", 99}
        };
            
        foreach(var item in listOfItems){
            getCitizenInventory().addItemToInventory(item.Key, item.Value);
        }


        relatives = null;

        initalizedCitizen = true;
    }

    public IEnumerator waitForCitizenToLoadThenAdd(Collider other){
        Town town = other.gameObject.GetComponent<Town>();
        yield return new WaitForSeconds(0.3f);

        if(house){
            foreach(BuildingAttributes building in town.getBuildingsInTown()){
                if(getCitizenHouse().getBuildingID() == building.getBuildingID()){
                    town.addCitizenToTown(this);
                    town.addInventoryToTown(inventory);
                }
            }
        }
    }

    public IEnumerator waitForCitizenToLoadThenRemove(Collider other){
        Town town = other.gameObject.GetComponent<Town>();
        yield return new WaitForSeconds(0.3f);

        if(house){
            foreach(BuildingAttributes building in town.getBuildingsInTown()){
                if(getCitizenHouse().getBuildingID() == building.getBuildingID()){
                    town.removeCitizenFromTown(this);
                    town.removeInventoryFromTown(inventory);
                }
            }
        }
    }

    public void checkCitizenMovement(){
        if(GetComponent<NavMeshAgent>().enabled == true){
            if(citizenAgent.remainingDistance <= 0.2){
                isMovingToDestination = false;
                reachedDestination = true;
                GetComponent<Animator>().SetBool("isMoving" , false);
            } else {
                isMovingToDestination = true;
                reachedDestination = false;
                GetComponent<Animator>().SetBool("isMoving" , true);
            }
        }
    }

    public void resetHourTriggers(){
        hour0 = false;
        hour1 = false;
        hour2 = false;
        hour3 = false;
        hour4 = false;
        hour5 = false;
        hour6 = false;
        hour7 = false;
        hour8 = false;
        hour9 = false;
        hour10 = false;
        hour11 = false;
        hour12 = false;
        hour13 = false;
        hour14 = false;
        hour15 = false;
        hour16 = false;
        hour17 = false;
        hour18 = false;
        hour19 = false;
        hour20 = false;
        hour21 = false;
        hour22 = false;
        hour23 = false;
    }

    public void goToDestination(Vector3 position){
        citizenAgent.SetDestination(position);
    }

    public IEnumerator newActionTrigger(){
        newAction = true;
        yield return new WaitForSeconds(0.1f);
        newAction = false;
    }

    public IEnumerator goToBuilding(BuildingAttributes building){
        if(building != null){
            if(!isMovingToBuilding){
                isMovingToBuilding = true;
                movedToBuilding = false;
                Vector3 buildingPosition = new Vector3(building.getPositionX(), building.getPositionY(), building.getPositionZ());

                if(isInsideBuilding == true){
                    leaveBuilding();
                }

                citizenAgent.SetDestination(buildingPosition);

                while(isMovingToBuilding){
                    if(citizenAgent.hasPath){
                        if(citizenAgent.remainingDistance <= 0.6){
                            movedToBuilding = true;
                            citizenAgent.ResetPath();
                            goInsideBuilding(building);
                            break;
                        }
                    }
                    yield return null;
                }
            }
            isMovingToBuilding = false;
        } else {
            if(isInsideBuilding == true){
                leaveBuilding();
            }
            if(townCurrentlyInsideOf){
                citizenAgent.SetDestination(new Vector3(townCurrentlyInsideOf.getTownCenter().x + Random.Range(-8.0f, 8.0f), 0, townCurrentlyInsideOf.getTownCenter().z + Random.Range(-8.0f, 8.0f)));
            }
        }
    }

    public BuildingAttributes getBuildingInsideOf(){
        return buildingInsideOf;
    }
    public void goInsideBuilding(BuildingAttributes building){
        setBuildingIsInsideOf(building);
        building.addCitizenToInsideBuilding(this);
        transform.GetChild(0).gameObject.SetActive(false);
        isInsideBuilding = true;
    }
    public void leaveBuilding(){
        buildingInsideOf.removeCitizenFromInsideBuilding(this);
        setBuildingIsInsideOf(null);
        transform.GetChild(0).gameObject.SetActive(true);
        isInsideBuilding = false;
    }


    public void putItemInBuildingInventory(string itemName, int amount){
        if(buildingInsideOf){
            getCitizenInventory().sendItemsFromThisToOther(buildingInsideOf.getBuildingAttributes().getBuildingInventory(), itemName, amount);
        } else {
            gameManager.getMessageLogText().addMessageToLog("Citizen is not inside a building");
        }
    }
    public void gatherFromBuildingInventory(string itemName, int amount){
        if(buildingInsideOf){
            buildingInsideOf.getBuildingAttributes().getBuildingInventory().sendItemsFromThisToOther(getCitizenInventory(), itemName, amount);
        } else {
            gameManager.getMessageLogText().addMessageToLog("Citizen is not inside a building");
        }
    }

    public IEnumerator working(){
        if(townAlliegence){
            if(work && !isWorking && gameManager.getClock().getHours() >= townAlliegence.getWorktimeStart() && gameManager.getClock().getHours() <= townAlliegence.getWorktimeEnd()){
                //Debug.Log("Workday has started");
                isWorking = true;
                bool movedToWorkplace = false;
                bool gatheringItem = false;
                bool movingItemToStorage = false;
                bool morningMeetingStarted = false;
                bool morningMeetingFinished = false;
                BuildingAttributes closestStorageBuilding = null;
                IEnumerator goToBuildingCourentine = null;

                while(isWorking && work){
                    if(work.getBuildingName().Equals("Sawmill") || work.getBuildingName().Equals("Furnace") || work.getBuildingName().Equals("Waterwell")){
                        //--------------//
                        // STOP WORKING //
                        //--------------//
                        if(gameManager.getClock().getHours() >= townAlliegence.getWorktimeEnd()){
                            //Debug.Log("Workday has endend");
                            break;
                        }

                        //--------------------------//
                        // FIRST GO TO WORKBUILDING //
                        //--------------------------//
                        if(!movedToWorkplace && !isMovingToWork){
                            //Debug.Log("Is moving to work");
                            isMovingToWork = true;
                            StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                        }

                        //-----------------------------//
                        // NOTIFY WHEN WORK IS REACHED //
                        //-----------------------------//
                        if(buildingInsideOf && !movedToWorkplace){
                            if(buildingInsideOf.getBuildingAttributes().getBuildingName().Equals(work.getBuildingName())){
                                //Debug.Log("Reached work");
                                movedToWorkplace = true;
                                isMovingToWork = false;
                            }
                        }

                        //------------------------//
                        // HAVING MORNING MEETING //
                        //------------------------//
                        if(!morningMeetingStarted && movedToWorkplace){
                            //Debug.Log("Is having morning meeting");
                            morningMeetingStarted = true;
                            yield return new WaitForSeconds(10);
                            morningMeetingFinished = true;
                        }

                        // LUNCH BREAK?

                        //-----------------------------------------------------------------//
                        // GETTING RESOURCES IF WORKINVENTORY IS DONT HAVE PRODUCTION ITEM //
                        //-----------------------------------------------------------------//
                        if(work.getItemCurrentlyProduced() != null){
                            if(!gatheringItem && !movingItemToStorage && morningMeetingFinished && movedToWorkplace && !work.getBuildingInventory().checkIfListOfItemsAreInInventory(work.getItemCurrentlyProduced().getCostToCraftItem())){
                                gatheringItem = true;
                                //Debug.Log("Gathering item from storage");
                                
                                bool isWalkingToStorage = false;
                                bool isWalkingToWork = false;

                                bool movingBackToWork = false;

                                bool transferringItem = false;
                                bool transferedItem = false;

                                bool findAmountToGather = false;
                                Dictionary<string, int> itemsToGather = work.getItemCurrentlyProduced().getCostToCraftItem();
                                string itemName = null;
                                int amountToGather = 10;
                                
                                

                                while(gatheringItem){
                                    

                                    // TODO: finn ut hvor mye som skal hentes
                                    if(!findAmountToGather){
                                        findAmountToGather = true;
                                        List<int> lowestNumber = new List<int>();
                                        foreach(var item in itemsToGather){
                                            lowestNumber.Add(work.getBuildingInventory().getFreeInventorySpaceForSpecificItem(item.Key));
                                            lowestNumber.Add(this.inventory.getFreeInventorySpaceForSpecificItem(item.Key));
                                            itemName = item.Key;

                                            foreach(int number in lowestNumber){
                                                if(number == 1){
                                                    amountToGather = number;
                                                }
                                                if(number <= amountToGather){
                                                    amountToGather = number;
                                                }
                                            }
                                            if(amountToGather > 99){
                                                amountToGather = 99;
                                            }
                                        }
                                        closestStorageBuilding = townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather);
                                    }
                                    
                                    // IF THERE IS NO STORAGE WITH ITEM, BREAK
                                    if(townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather) == null && !transferedItem){
                                        //Debug.Log("found no storage with item");
                                        if(!buildingInsideOf && !movingBackToWork){
                                            movingBackToWork = true;
                                            isMovingToBuilding = false;
                                            StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                                        }
                                        break;
                                    }

                                    // IF THERE IS STORAGE, BUT IT HAS CHANGED TO ANOTHER BUILDING
                                    if(townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather)){
                                        if(!(closestStorageBuilding.name).Equals(townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather).name) && townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather) != null && !transferedItem){
                                            //Debug.Log(closestStorageBuilding.name);
                                            //Debug.Log(townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather).name);
                                            Debug.Log("found new storage building");
                                            if(work.getBuildingInventory().checkIfListOfItemsAreInInventory(work.getItemCurrentlyProduced().getCostToCraftItem())){
                                                isWalkingToStorage = false;
                                            } else {
                                                break;
                                            }
                                            
                                        }
                                    }
                                    
                                        


                                    // WALK TO STORAGE
                                    if(!isWalkingToStorage && closestStorageBuilding != null){
                                        StopCoroutine(goToBuildingCourentine);
                                        isWalkingToStorage = true;
                                        isMovingToBuilding = false;
                                        //Debug.Log("is walking to storage");
                                        //Debug.Log(closestStorageBuilding.name);
                                        
                                        // GO TO CLOSEST STORAGEBUILDING WITH ITEM
                                        closestStorageBuilding = townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather);
                                        StartCoroutine(goToBuildingCourentine = goToBuilding(closestStorageBuilding));
                                    }

                                    // WHEN INSIDE STORAGE, PICK UP ITEM
                                    if(buildingInsideOf){
                                        if(buildingInsideOf.Equals(closestStorageBuilding) && !transferringItem){
                                            //Debug.Log("picking up item");
                                            transferringItem = true;
                                            foreach(var item in itemsToGather){
                                                buildingInsideOf.getBuildingAttributes().getBuildingInventory().sendItemsFromThisToOther(this.inventory, item.Key, amountToGather);
                                            }

                                            yield return new WaitForSeconds(2);
                                            transferedItem = true;
                                        }
                                    }

                                    // WHEN ITEM IS PICKED UP, GO BACK TO WORK
                                    if(transferedItem && !isWalkingToWork){
                                        //Debug.Log("returning to work");
                                        isWalkingToWork = true;
                                        StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                                    }

                                    // IF ITEM IS SUCCESFULLY TRANSFERED AND CITIZEN IS BACK IN WORKBUILDING, TRANSFER THEN STOP GATHERING ITEM
                                    if(buildingInsideOf){
                                        if(buildingInsideOf.Equals(work) && transferedItem){
                                            foreach(var item in itemsToGather){
                                                this.inventory.sendItemsFromThisToOther(work.getBuildingInventory(), item.Key, amountToGather);
                                            }
                                            //Debug.Log("items transfered to work");
                                            break;
                                        }
                                    }
                                    yield return null;
                                }
                                // RESET BOOLS WHEN DONE GETTING ITEM
                                gatheringItem = false;
                            }
                        }
                        //-------------------------------------//
                        // MOVE RESOURCES FROM WORK TO STORAGE //
                        //-------------------------------------//
                        if(work.getItemCurrentlyProduced() != null){
                            if(!movingItemToStorage && !gatheringItem && morningMeetingFinished && movedToWorkplace && work.getBuildingInventory().checkIfListOfItemsAreInInventory(new Dictionary<string, int>(){{work.getItemCurrentlyProduced().getName(), 20}})){
                                movingItemToStorage = true;
                                bool isWalkingToStorage = false;
                                bool isWalkingToWork = false;

                                bool movingBackToWork = false;

                                bool transferedItem = false;

                                bool pickedUpItem = false;

                                string itemName = work.getItemCurrentlyProduced().getName();
                                int itemAmount = 20;
                                closestStorageBuilding = townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, itemAmount);

                                while(movingItemToStorage){
                                    // IF THERE IS NO FREE STORAGE, BREAK
                                    if(townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, itemAmount) == null && !pickedUpItem){
                                        //Debug.Log("found no storage with item");
                                        if(!buildingInsideOf && !movingBackToWork){
                                            movingBackToWork = true;
                                            isMovingToBuilding = false;
                                            StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                                        }
                                        break;
                                    }
                                    // IF THERE IS STORAGE, BUT IT HAS CHANGED TO ANOTHER BUILDING
                                    if(townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, itemAmount)){
                                        if(!(closestStorageBuilding.name).Equals(townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, itemAmount).name) && townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, itemAmount) != null && !transferedItem){
                                            //Debug.Log(closestStorageBuilding.name);
                                            //Debug.Log(townAlliegence.getClosestStorageBuildingWithItem(this.transform.position, itemName, amountToGather).name);
                                            //Debug.Log("found new storage building");
                                            closestStorageBuilding = townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, itemAmount);
                                            isWalkingToStorage = false;
                                            
                                        }
                                    }

                                    // WHEN INSIDE WORKPLACE, PICK UP ITEM
                                    if(buildingInsideOf){
                                        if(buildingInsideOf.Equals(work) && !pickedUpItem){
                                            //Debug.Log("picking up item");
                                            pickedUpItem = true;
                                            buildingInsideOf.getBuildingAttributes().getBuildingInventory().sendItemsFromThisToOther(this.inventory, itemName, itemAmount);
                                            
                                        }
                                    }
                                    // WALK TO STORAGE
                                    if(!isWalkingToStorage && pickedUpItem && closestStorageBuilding != null){
                                        StopCoroutine(goToBuildingCourentine);
                                        isWalkingToStorage = true;
                                        isMovingToBuilding = false;
                                        //Debug.Log("is walking to storage");
                                        //Debug.Log(closestStorageBuilding.name);
                                        
                                        // GO TO CLOSEST STORAGEBUILDING WITH FREE SPACE
                                        StartCoroutine(goToBuildingCourentine = goToBuilding(closestStorageBuilding));
                                    }
                                    // WHEN INSIDE STORAGE, DROP OFF ITEM
                                    if(buildingInsideOf){
                                        if(buildingInsideOf.Equals(closestStorageBuilding) && !transferedItem){
                                            //Debug.Log("transfered item to storage");
                                            transferedItem = true;
                                            this.inventory.sendItemsFromThisToOther(buildingInsideOf.getBuildingAttributes().getBuildingInventory(), itemName, itemAmount);
                                            yield return new WaitForSeconds(2f);
                                        }
                                    }
                                    // WALK BACK TO WORK
                                    if(transferedItem && !isWalkingToWork){
                                        StopCoroutine(goToBuildingCourentine);
                                        isWalkingToWork = true;
                                        isMovingToBuilding = false;
                                        //Debug.Log("is walking to back to work");
                                        //Debug.Log(closestStorageBuilding.name);
                                        
                                        // GO TO CLOSEST STORAGEBUILDING WITH FREE SPACE
                                        StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                                    }
                                    // IF ITEM IS SUCCESFULLY TRANSFERED AND CITIZEN IS BACK IN WORKBUILDING, BREAK
                                    if(buildingInsideOf){
                                        if(buildingInsideOf.Equals(work) && transferedItem){
                                            //Debug.Log("reached workbuilding");
                                            break;
                                        }
                                    }
                                    yield return null;
                                }
                            }
                            movingItemToStorage = false;
                        }

                    }
                    yield return null;
                }            
                isWorking = false;
            }
        }
    }
    

    public void goToHangoutDestination(){

    }
    public void degenerateThirst(){
        if(thirst > 0){
            thirst-=4;   
        }
        if(thirst <= 0){
            takeDamage(2);
        }
    }
    public void degenerateHunger(){
        if(hunger > 0){
            hunger-=2;
        }  
        if(hunger <= 0){
            takeDamage(4);
        }      
    }
    public void takeDamage(int damage){
        if(health > 0){
            health -= damage;
        }
        if(health <= 0){
            die();
        }  

    }
    public void die(){
        Destroy(gameObject);
    }
    
    public void gatherResources(){

    }

    public void putItemDown(){
        //play animation
    }
    public Inventory getCitizenInventory(){
        return inventory;
    }


    public void doHousework(){

    }


    public void eatFood(){

    }
    public void drinkSomething(){

    }
    public void mating(){

    }
    public void lookForWork(){
        if(townAlliegence && !isLookingForWork){
            setIsLookingForWork(true);
            townAlliegence.addAvailableWorkerToTown(this);

        }
    }
    public void foundWork(){
        setIsLookingForWork(false);
        if(townAlliegence){
            townAlliegence.removeAvailableWorkerFromTown(this);
        }
    }
    public void lookForHousing(){
        if(!isLookingForHouse){
            
        }
        setIsLookingForHousing(true);
        if(getTownCurrentlyInsideOf()){
            if(getTownCurrentlyInsideOf().getTownAttractivnes() > 20){
                if(getTownCurrentlyInsideOf().getAvailableResidentialBuildingsInTown().Count != 0){
                    setHouse(getTownCurrentlyInsideOf().getAvailableResidentialBuildingsInTown()[Random.Range(0, getTownCurrentlyInsideOf().getAvailableResidentialBuildingsInTown().Count)]);
                    setIsLookingForHousing(false);    
                    house.addResidentToBuilding(this);
                    setTownAlliegence(house.getTownBuildingIsApartOf());
                } else {
                    //Debug.Log("No more housing");
                }
            }
        } else {
            gameManager.GetTownCatalog().getNearestTown();
            // Get nearest town with attractivnes over 20?
        }
    }
    public void foundHousing(){
        
    }
    public void lookForPartner(){

    }
    public void sleep(){

    }
    public void rest(){

    }
    public void randomAction(){
        List<string> actions = new List<string>{"dance", "pray", "sing", "cry", "look at the sky in wonder", "murder", "talk with citizen", "complain about the weather", "philosiphy", "do hooby", "throw tomato at player", "spit at player",
        "ask for handshake", "prise player", "salut player", "ask for money", "compliment player", "clap for player", "die from stroke", "cough"};

        string randomAction = actions[Random.Range(0,actions.Count)];
        //dance
        //pray
        //Dance
        //sing
        //cry
        //look at the sky in wonder
        //murder citizen
        //talk with citizen
        //complain about the weather
        //philosiphy
        //do hobby
        //look for partner
        //throw tomato at player if bad relation
        //spit at player if really bad realtion
        //ask for handshake from player if good realtions
        //praise player
        //salut player
        //ask for money from player
        //compliment player
        //clap when player is in range
        //die from stroke
        //cough
        //
    }
    public void movementDisabled(){

    }
    public void hideBody(bool val){
        if(val){
            transform.GetChild(0).gameObject.SetActive(false);
        } else {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void lookAt(){

    }
    public void stopMovement(){
        citizenAgent.ResetPath();
    }
    public void teleportToPosition(){
        
    }
    public void pickUpItem(){

    }
    public void moveItemToHouse(){

    }
    public void moveItemToWorkplace(){

    }

    // GETTERS
    public string getName(){
        return citizenName;
    }
    public BuildingAttributes getCitizenHouse(){
        return house;
    }
    public Town getTownAlliegence(){
        return townAlliegence;
    }
    public Town getTownCurrentlyInsideOf(){
        return townCurrentlyInsideOf;
    }
    public bool getIsLookingForWork(){
        return isLookingForWork;
    }
    public int getCitizenID(){
        return citizenID;
    }

    // SETTERS
    public void setHouse(BuildingAttributes building){
        house = building;
    }
    public void setWork(BuildingAttributes building){
        work = building;
    }
    public void setTownAlliegence(Town town){
        townAlliegence = town;
    }
    public void setTownCurrentlyInsideOf(Town town){
        townCurrentlyInsideOf = town;
    }
    public void setIsLookingForWork(bool val){
        isLookingForWork = val;
    }
    public void setIsLookingForHousing(bool val){
        isLookingForHouse = val;
    }
    public void setBuildingIsInsideOf(BuildingAttributes building){
        buildingInsideOf = building;
    }
    
}
