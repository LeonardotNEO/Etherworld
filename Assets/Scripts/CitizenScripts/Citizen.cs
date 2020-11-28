using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Citizen : MonoBehaviour
{
    GameManager gameManager;
    public Vector3 position;
    NavMeshAgent citizenAgent;
    public Skills skills;
    public PerkCatalog perks;
    public List<Citizen> relatives = new List<Citizen>();
    public List<ResourceAttributes> stoneDepotsInRange = new List<ResourceAttributes>();
    public List<ResourceAttributes> treesInRange = new List<ResourceAttributes>();
    public ResourceAttributes resourceBeingMined;
    public List<ItemAttributes> itemsToPickUp = new List<ItemAttributes>();

    IEnumerator goToBuildingCourentine = null;

    public Inventory inventory;
    public BuildingAttributes home = null;
    public BuildingAttributes work = null;
    public BuildingAttributes buildingInsideOf = null;
    public BuildingAttributes closestStorageBuilding = null;
    public BuildingAttributes closestStorageBuildingWithFreeSpace = null;
    public Town townAlliegence;
    public Town townCurrentlyInsideOf;
    public string gender;
    public string citizenFirstName;
    public string citizenLastName;
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
    public int income;
    public int taxPayment;
    public int citizenID;
    //public List<Hobby> hobbies;
    //public List<Personalities> personalityTraits;
    //public List<Ambition> ambitions; 

    // BOOLS //
    // CITIZEN
    public bool initalizedCitizen;
    public bool isMovingToDestination;

    // BUILDING
    public bool isInsideBuilding;
    public bool isMovingToBuilding;
    public bool movedToBuilding;

    // HOUSE
    public bool isLookingForHouse;


    //WORK
    public bool isLookingForWork;
    public bool isWorking;
    public bool isMovingToWork;
    public bool morningMeetingStarted;
    public bool morningMeetingFinished;
    public bool gatheringItemFromBuilding;
    public bool movingItemToBuilding;
    public bool puttingItemInStorage;

    // REMOVE HUNGER/THIRST
    public bool degenerating;

    // MINING RESOURCE
    public bool gatheringResource;
    public bool pickingUpItem;

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
            // MOVEMENT
            checkCitizenMovement();

            // WORK
            StartCoroutine(working());

            if(work == null){
                lookForWork();
            } else {
                foundWork();
            }
            // THIRST HUNGER
            StartCoroutine(degenerateThirstHunger()); 
        
            // HOUSE
            if(home == null){
                lookForHome();
            } else {
                foundHome();
            }
            if(!gatheringResource && treesInRange.Count > 0 && itemsToPickUp.Count == 0 && !pickingUpItem){
                StartCoroutine(gatherResources("Tree"));
            } 
            if(itemsToPickUp.Count > 0 && !gatheringResource){
                StartCoroutine(pickUpItem());
            }

        }
    }

    void OnMouseDown()
    {
        gameManager.getCitizenCatalog().setSelectedCitizen(this);
        gameManager.GetUI().openCitizenMenu();
    }

    void OnMouseEnter()
    {
        setCitizenIndicator(true);
    }

    void OnMouseExit()
    {
        setCitizenIndicator(false);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<Town>()){
            setTownCurrentlyInsideOf(other.gameObject.GetComponent<Town>());
            StartCoroutine(waitForCitizenToLoadThenAdd(other));
        }
        if(other.gameObject.name.Contains("StoneDepot")){
            stoneDepotsInRange.Add(other.GetComponent<ResourceAttributes>());
        }
        if(other.gameObject.name.Contains("Tree")){
            treesInRange.Add(other.GetComponent<ResourceAttributes>());
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Town>()){
            setTownCurrentlyInsideOf(null);
        }
        if(other.gameObject.name.Contains("StoneDepot")){
            stoneDepotsInRange.Remove(other.GetComponent<ResourceAttributes>());
        }
        if(other.gameObject.name.Contains("Tree")){
            treesInRange.Remove(other.GetComponent<ResourceAttributes>());
        }
    }

    void OnDisable()
    {
        if(home){
            home.removeResidentFromBuilding(this);
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
        List<string> maleFirstNames = new List<string>{"Hans","Peter","Ole","Leonard","Fredrik","Markus","Chad","Rick","Morty","Erlend","James","Bertil"};
        List<string> femaleFirstNames = new List<string>{"Siri","Grete","Karen","Martha","Marte","Monica"};
        List<string> lastNames = new List<string>{"Hansen","Peterson","Trump","Opsal","Taklo","Berge","Iversen","Jakobsen","Storeide","Gaard","Fiska","Bush"};
        if(gender == "Male"){
            citizenFirstName = maleFirstNames[Random.Range(0, maleFirstNames.Count)];
        } else {
            citizenFirstName = femaleFirstNames[Random.Range(0, femaleFirstNames.Count)];
        }
        citizenLastName = lastNames[Random.Range(0, lastNames.Count)];
        transform.name = citizenFirstName + " " + citizenLastName + " " + citizenID;

        // AGE
        age = Random.Range(16,60);

        // SKILLS
        skills = GetComponent<Skills>();

        // PERKS
        perks = GetComponent<PerkCatalog>();

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
        if(home == null){
            townAlliegence = null;
        } else {
            if(home.getTownBuildingIsApartOf()){
                townAlliegence = home.getTownBuildingIsApartOf();
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
        /*Dictionary<string,int> listOfItems = new Dictionary<string, int>{
        {"Wood plank", 99}, 
        {"Stone", 99}, 
        {"Wood log", 99}, 
        {"Iron ore", 99}
        };
        foreach(var item in listOfItems){
            getCitizenInventory().addItemToInventory(item.Key, item.Value);
        }*/


        relatives = null;

        initalizedCitizen = true;
    }

    public IEnumerator waitForCitizenToLoadThenAdd(Collider other){
        /*
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
        */ // THE CITIZEN IS ADDED IN THE ONTRIGGER IN TOWNALLIEGENCE
        yield return null;
    }

    public IEnumerator waitForCitizenToLoadThenRemove(Collider other){
        Town town = other.gameObject.GetComponent<Town>();
        yield return new WaitForSeconds(0.3f);

        if(home){
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
                GetComponent<Animator>().SetBool("isMoving" , false);
                isMovingToDestination = false;
            } else {
                isMovingToDestination = true;
                GetComponent<Animator>().SetBool("isMoving" , true);
            }
        }
    }

    public IEnumerator degenerateThirstHunger(){
        if(!degenerating){
            degenerating = true;
            float initialTime = Time.time;

            while(degenerating){
                if(Time.time > initialTime + 240){
                    degenerateHunger();
                    degenerateThirst();
                    break;
                }
                yield return null;
            }
            degenerating = false;
        }
    }
    
    public void goToDestination(Vector3 position){
        if(buildingInsideOf){
            leaveBuilding();
        }
        stopMovement();
        citizenAgent.SetDestination(position);
    }

    public void goToTownCenter(){
        if(townCurrentlyInsideOf){
            goToDestination(new Vector3(townCurrentlyInsideOf.getTownCenter().x + Random.Range(-8.0f, 8.0f), 0, townCurrentlyInsideOf.getTownCenter().z + Random.Range(-8.0f, 8.0f)));
        }
    }

    public IEnumerator goToBuilding(BuildingAttributes building){
        if(building != null){
            if(!isMovingToBuilding){
                isMovingToBuilding = true;
                movedToBuilding = false;

                //Debug.Log("goToBuilding har started");
                Vector3 buildingPosition = new Vector3(building.getPositionX(), building.getPositionY(), building.getPositionZ());

                if(isInsideBuilding == true){
                    leaveBuilding();
                }

                citizenAgent.SetDestination(buildingPosition);

                while(isMovingToBuilding){
                    if(citizenAgent.hasPath){
                        if(citizenAgent.remainingDistance <= 0.6 && !movedToBuilding){
                            movedToBuilding = true;
                            goInsideBuilding(building);
                            //Debug.Log("reached destination");
                            citizenAgent.ResetPath();
                            break;
                        }
                    }
                    yield return null;
                }
                isMovingToBuilding = false;
            }
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
        //Debug.Log("went inside building");
        setBuildingIsInsideOf(building);
        building.addCitizenToInsideBuilding(this);
        transform.GetChild(0).gameObject.SetActive(false);
        isInsideBuilding = true;
    }
    public void leaveBuilding(){
        if(buildingInsideOf){
            //Debug.Log("left building");
            buildingInsideOf.removeCitizenFromInsideBuilding(this);
            setBuildingIsInsideOf(null);
            transform.GetChild(0).gameObject.SetActive(true);
            isInsideBuilding = false;
        } else {
            stopMovement();
        } 
    }


    public bool putItemInBuildingInventory(BuildingAttributes targetBuilding, string itemName, int amount){
        bool result = false;
        if(buildingInsideOf){
            result = getCitizenInventory().sendItemFromThisToOther(targetBuilding.getBuildingInventory(), itemName, amount);
        } else {
            gameManager.getMessageLogText().addMessageToLog("Citizen is not inside a building");
        }
        return result;
    }
    public bool getItemFromBuildingInventory(BuildingAttributes targetBuilding, string itemName, int amount){
        bool result = false;
        if(buildingInsideOf){
            result = targetBuilding.getBuildingAttributes().getBuildingInventory().sendItemFromThisToOther(getCitizenInventory(), itemName, amount);
        } else {
            gameManager.getMessageLogText().addMessageToLog("Citizen is not inside a building");
        }
        return result;
    }

    public bool notifyWhenWorkReached(){
        if(buildingInsideOf){
            if(buildingInsideOf.Equals(work.getBuildingName())){
                return true;
            }
        }
        return false;
    }

    public bool notifyWhenStorageBuildingChange(Dictionary<string, int> listOfItems){
        // IF CHANGING FROM SOMETHING TO NULL
        if(closestStorageBuilding != null && townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, listOfItems) == null){
            //Debug.Log("closest storage building went from something to null");
            closestStorageBuilding = null;
            return true;
        }

        // IF CHANGING FROM NULL TO SOMETHING
        if(townAlliegence){
            if(closestStorageBuilding == null && townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, listOfItems) != null){
                //Debug.Log("closest storage building went from null to something");
                closestStorageBuilding = townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, listOfItems);
                return true;
            }
        }

        // IF CHANGE TO NEW BUILDING
        if(closestStorageBuilding && townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, listOfItems)){
            if(!closestStorageBuilding.Equals(townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, listOfItems))){
                //Debug.Log("Closest storage building changed from something to other");
                closestStorageBuilding = townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, listOfItems);
                return true;
            }
        }

        return false;
    }

    public bool notifyWhenStorageBuildingWithFreeSpaceChange(string itemName, int amount){
        // IF CHANGING FROM SOMETHING TO NULL
        if(closestStorageBuildingWithFreeSpace != null && townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, amount) == null){
            //Debug.Log("closest storage building with freespace went from something to null");
            closestStorageBuildingWithFreeSpace = null;
            return true;
        }

        // IF CHANGING FROM NULL TO SOMETHING
        if(townAlliegence){
            if(closestStorageBuildingWithFreeSpace == null && townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, amount) != null){
                //Debug.Log("closest storage building with freespace went from null to something");
                closestStorageBuildingWithFreeSpace = townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, amount);
                return true;
            }
        }

        // IF CHANGE TO NEW BUILDING
        if(closestStorageBuildingWithFreeSpace && townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, amount)){
            if(!closestStorageBuildingWithFreeSpace.Equals(townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, amount))){
                //Debug.Log("Closest storage building with freespace changed from something to other");
                closestStorageBuildingWithFreeSpace = townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, itemName, amount);
                return true;
            }
        }

        return false;
    }

    public bool notifyWhenWorkplaceReached(){
        if(buildingInsideOf){
            if(buildingInsideOf.Equals(work)){
                return true;
            }
        }
        return false;
    }

    public bool notifyWhenStorageReached(){
        if(buildingInsideOf){
            if(buildingInsideOf.Equals(closestStorageBuilding)){
                //Debug.Log("citizen is inside closest storagebuilding with item " + this.transform.name);
                return true;
            }
        }
        return false;
    }

    public bool notifyIfWorkTime(){
        if(townAlliegence && work){
            if(gameManager.getClock().getHours() >= townAlliegence.getWorktimeStart()){
                if(gameManager.getClock().getHours() <= townAlliegence.getWorktimeEnd()){
                    return true;
                }
            }
        }
        return false;
    }

    public bool notifyWhenWorkInventoryRunningLow(){
        if(work){
            if(work.getItemCurrentlyProduced() != null){
                Dictionary<string,int> itemsNeeded = work.getListOfProductionItemsBasedOnTransferThresholds();
                
                if(!work.getBuildingInventory().checkIfListOfItemsAreInInventory(itemsNeeded)){
                    return true;
                }
            }
        }
        return false;
    }

    public bool notifyWhenCitizenInventoryHasWorkItems(){
        if(work){
            if(work.getItemCurrentlyProduced() != null){
                if(inventory.checkIfListOfItemsAreInInventory(work.getItemCurrentlyProduced().getCostToCraftItem())){
                    //Debug.Log("citizen has work producing items " + this.transform.name);
                    return true;
                } 
            }
        }
        return false;
    }
    public bool notifyWhenCitizenInventoryHasProductionItem(){
        if(work){
            if(work.getItemCurrentlyProduced() != null){
                if(inventory.checkIfListOfItemsAreInInventory(new Dictionary<string, int>(){{work.getItemCurrentlyProduced().getName(), work.getPutItemInStorageThreshold()}})){
                    //Debug.Log("citizen has work producing items " + this.transform.name);
                    return true;
                } 
            }
        }
        return false;
    }

    public bool notifyWhenWorkItemProducedThresholdReached(){
        if(work){
            if(work.getItemCurrentlyProduced() != null){
                if(work.getBuildingInventory().getAmountOfSpecificItem(work.getItemCurrentlyProduced().getName()) > work.getPutItemInStorageThreshold()){
                    //Debug.Log("produced threshold reached " + work.name);
                    return true;
                } 
            }
        }
        return false;
    }

    public bool notifyWhenTargetBuildingReached(BuildingAttributes targetBuilding){
        if(buildingInsideOf){
            if(buildingInsideOf.Equals(targetBuilding)){
                return true;
            }
        }
        return false;
    }

    public IEnumerator morningMeeting(){
        if(buildingInsideOf){
            if(buildingInsideOf.Equals(work) && !morningMeetingStarted){
                //Debug.Log("Is having morning meeting");
                morningMeetingStarted = true;
                yield return new WaitForSeconds(10);
                morningMeetingFinished = true;
                //Debug.Log("Morning meeting finished");
            }
        }
    }

    public IEnumerator getItemFromBuilding(Dictionary<string, int> listOfItems, BuildingAttributes targetBuilding){
        if(work){
            if(work.getItemCurrentlyProduced() != null && !gatheringItemFromBuilding){
                gatheringItemFromBuilding = true;
                //Debug.Log("started get item from building");
                while(gatheringItemFromBuilding){
                    if(notifyWhenTargetBuildingReached(targetBuilding)){
                        //Debug.Log("reched targetbuilding with lsit of items");

                        foreach(var item in listOfItems){
                            getItemFromBuildingInventory(targetBuilding ,item.Key, item.Value);
                            //Debug.Log("item transferred  from building to citizen");
                        }
                        yield return new WaitForSeconds(2);
                        break;
                    } else {
                        if(targetBuilding != null){
                            StartCoroutine(goToBuildingCourentine = goToBuilding(targetBuilding));
                        } else {
                            //Debug.Log("targetbuilding not found");
                            break;
                        }
                    }
                    if(notifyWhenStorageBuildingChange(listOfItems)){
                        //Debug.Log("storage building changed");
                        targetBuilding = closestStorageBuilding;
                        if(targetBuilding == null){
                            break;
                        }
                    }
                    yield return null;
                }
                StopCoroutine(goToBuildingCourentine);
                stopMovement();
                gatheringItemFromBuilding = false;
            }
        }
    }

    public IEnumerator putItemInBuilding(Dictionary<string, int> listOfItems, BuildingAttributes targetBuilding){
        if(!movingItemToBuilding){
            movingItemToBuilding = true;
            //Debug.Log("started put item in building");
            while(movingItemToBuilding){
                if(notifyWhenTargetBuildingReached(targetBuilding)){
                    //Debug.Log("reched targetbuilding with lsit of items");

                    foreach(var item in listOfItems){
                        putItemInBuildingInventory(targetBuilding, item.Key, item.Value);
                        //Debug.Log("item transferred  from building to citizen");
                    }
                    yield return new WaitForSeconds(2);
                    break;
                } else {
                    if(targetBuilding != null){
                        StartCoroutine(goToBuildingCourentine = goToBuilding(targetBuilding));
                    } else {
                        //Debug.Log("targetbuilding not found");
                        break;
                    }
                }
                foreach(var item in listOfItems){
                    notifyWhenStorageBuildingWithFreeSpaceChange(item.Key, item.Value);
                    if(notifyWhenStorageBuildingWithFreeSpaceChange(item.Key, item.Value)){
                        targetBuilding = closestStorageBuildingWithFreeSpace;
                        stopMovement();
                    }
                    if(targetBuilding == null){
                        break;
                    }
                }
                
                yield return null;
            }
            StopCoroutine(goToBuildingCourentine);
            stopMovement();
            movingItemToBuilding = false;
        }
        
    }

    public IEnumerator goToWorkbuilding(){
        if(buildingInsideOf){
            if(!buildingInsideOf.Equals(work) && !isMovingToWork){
                //Debug.Log("Is moving to work");
                isMovingToWork = true;
                while(isMovingToWork && work){
                    if(notifyWhenWorkplaceReached()){
                        //Debug.Log("workplace reached");
                        break;
                    } else {
                        StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                    }
                    yield return null;
                }
                isMovingToWork = false;
                stopMovement();
            }
        } else if(!isMovingToWork){
            //Debug.Log("Is moving to work");
            isMovingToWork = true;
            while(isMovingToWork && work){
                if(notifyWhenWorkplaceReached()){
                    //Debug.Log("workplace reached");
                    break;
                }  else {
                    StartCoroutine(goToBuildingCourentine = goToBuilding(work));
                }
                yield return null;
            }
            isMovingToWork = false;
            stopMovement();
        }
    }
    

    public IEnumerator working(){
        if(work && !isWorking && notifyIfWorkTime()){
            //Debug.Log("work started");
            isWorking = true;
            morningMeetingStarted = false;
            morningMeetingFinished = false;

            while(isWorking && work){
                if(work.getBuildingName().Equals("Sawmill") || work.getBuildingName().Equals("Furnace") || work.getBuildingName().Equals("Waterwell")){
                    if(!notifyIfWorkTime()){
                        goToTownCenter();
                        //Debug.Log("work is done for the day");
                        break;
                    }

                    // GO TO WORKBUILDING //
                    if(notifyIfWorkTime()){
                        do{
                            StartCoroutine(goToWorkbuilding());
                            //Debug.Log("moving to workplace");
                            yield return null;
                        } while(isMovingToWork && work);
                    }

                    // HAVING MORNING MEETING //
                    StartCoroutine(morningMeeting());
                    
                    // GETTING RESOURCES IF WORKINVENTORY DONT HAVE PRODUCTION ITEM //
                    if(notifyWhenWorkInventoryRunningLow() && !notifyWhenCitizenInventoryHasWorkItems() && morningMeetingFinished && !movingItemToBuilding){
                        do{
                            StartCoroutine(getItemFromBuilding(work.getListOfProductionItemsBasedOnTransferThresholds(), townAlliegence.getClosestStorageBuildingWithListOfItems(this.transform.position, work.getListOfProductionItemsBasedOnTransferThresholds())));
                            //Debug.Log("gathering item from storage");
                            yield return null;
                        } while (gatheringItemFromBuilding && work);
                    }
                    
                    // MOVE RESOURCES BACK TO WORKPLACE //
                    if(notifyWhenCitizenInventoryHasWorkItems() && morningMeetingFinished && !gatheringItemFromBuilding){
                        do{
                            StartCoroutine(putItemInBuilding(work.getListOfProductionItemsBasedOnTransferThresholds(), work));
                            //Debug.Log("putting item in workbuilding");
                            yield return null;
                        } while (movingItemToBuilding  && work);
                    }

                    // GET RESOURCE THRESHOLD ITEM FROM WORKBUILDING
                    if(notifyWhenWorkItemProducedThresholdReached() && morningMeetingFinished && !movingItemToBuilding){
                        do{
                            StartCoroutine(getItemFromBuilding(new Dictionary<string, int>(){{work.getItemCurrentlyProduced().getName(), work.getPutItemInStorageThreshold()}}, work));
                            //Debug.Log("gathering item from workbuilding");
                            yield return null;
                        } while(gatheringItemFromBuilding && work);
                    }

                    // MOVE RESOURCES TO STORAGE IF THRESHOLD REACHED
                    if(notifyWhenCitizenInventoryHasProductionItem() && morningMeetingFinished && !gatheringItemFromBuilding){
                        do{
                            StartCoroutine(putItemInBuilding(new Dictionary<string, int>(){{work.getItemCurrentlyProduced().getName(), work.getPutItemInStorageThreshold()}}, townAlliegence.getClosestStoragetBuildingWithFreeSpace(this.transform.position, work.getItemCurrentlyProduced().getName(), work.getPutItemInStorageThreshold())));
                            //Debug.Log("putting item in storage");
                            yield return null;
                        } while(movingItemToBuilding && work);
                    }

                }
                yield return null;
            }   

            if(goToBuildingCourentine != null){
                StopCoroutine(goToBuildingCourentine);  
            }     

            if(!work){
                //stopMovement();
                goToTownCenter();
            }
            morningMeetingStarted = false;
            morningMeetingFinished = false;
            isWorking = false;
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
    
    public IEnumerator gatherResources(string resourceName){
        if(!gatheringResource){
            gatheringResource = true;
            ResourceAttributes closestResource = null;
            IEnumerator coroutine = null;

            if(resourceName.Equals("StoneDepot") && findClosestResource(stoneDepotsInRange) != null){
                closestResource = findClosestResource(stoneDepotsInRange);
            }
            if(resourceName.Equals("Tree") && findClosestResource(treesInRange) != null){
                closestResource = findClosestResource(treesInRange);
            }
            if(closestResource != null){
                StartCoroutine(coroutine = closestResource.walkingToResource(this)); 
            }

            while(gatheringResource){
                if(closestResource == null){
                    break;
                }
                if(closestResource.getGatheringResourcesRunning() && this.getResourceBeingMined() != closestResource){
                    StopCoroutine(coroutine);
                    break;
                }
                yield return null;
            }
            gatheringResource = false;
        }
    }
    

    public ResourceAttributes findClosestResource(List<ResourceAttributes> resourceList){
        resourceList.RemoveAll(item => item == null);

        List<ResourceAttributes> resourceArrangeAfterDistance = new List<ResourceAttributes>();
        Dictionary<ResourceAttributes, float> resourceAfterDistance = new Dictionary<ResourceAttributes, float>();

        foreach(ResourceAttributes resource in resourceList){
            if(resource != null){
                if(!resourceAfterDistance.ContainsKey(resource)){
                    if(resource.GetComponent<ResourceAttributes>().getGatheringResourcesRunning() == false){
                        resourceAfterDistance.Add(resource, Vector3.Distance(this.transform.position, resource.transform.position));
                    } else {
                        //Debug.Log("cant add resource because it is already beeing mined");
                    }
                }
            }
        }

        foreach(KeyValuePair<ResourceAttributes, float> resource in resourceAfterDistance.OrderBy(key => key.Value)){
            resourceArrangeAfterDistance.Add(resource.Key);
        }

        if(resourceArrangeAfterDistance.Count > 0){
            return resourceArrangeAfterDistance[0];
        } else {
            return null;
        }
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
            job = "Unemployed";
            townAlliegence.addAvailableWorkerToTown(this);
            townAlliegence.updateAvailableWorkplacesInTown();
        }
    }
    public void foundWork(){
        if(townAlliegence && isLookingForWork){
            setIsLookingForWork(false);
            job = work.getJobName();
            townAlliegence.updateAvailableWorkplacesInTown();
        }
    }
    public void lookForHome(){
        if(!isLookingForHouse){
            
        }
        setIsLookingForHousing(true);
        if(getTownCurrentlyInsideOf()){
            if(getTownCurrentlyInsideOf().getTownAttractivnes() > 20){
                if(getTownCurrentlyInsideOf().getAvailableResidentialBuildingsInTown().Count != 0){
                    setHouse(getTownCurrentlyInsideOf().getAvailableResidentialBuildingsInTown()[Random.Range(0, getTownCurrentlyInsideOf().getAvailableResidentialBuildingsInTown().Count)]);
                    setIsLookingForHousing(false);    
                    home.addResidentToBuilding(this);
                    setTownAlliegence(home.getTownBuildingIsApartOf());

                    townAlliegence.updateAvailableResidentialBuildingsInTown();
                    townAlliegence.updateHomelessInTown();
                } else {
                    //Debug.Log("No more housing");
                }
            }
        } else {
            gameManager.getTownCatalog().getNearestTown();
            // Get nearest town with attractivnes over 20?
        }
    }
    public void foundHome(){
        if(townAlliegence && isLookingForHouse){
            townAlliegence.updateAvailableResidentialBuildingsInTown();
            townAlliegence.updateHomelessInTown();
        }
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
    public void lookAt(GameObject lookAt){
        transform.LookAt(new Vector3(lookAt.transform.position.x, 0, lookAt.transform.position.z));
    }
    public void stopMovement(){
        citizenAgent.ResetPath();
        isMovingToBuilding = false;
    }
    public void teleportToPosition(){
        
    }
    public IEnumerator pickUpItem(){
        if(!pickingUpItem){
            pickingUpItem = true;
            ItemAttributes itemtoPickUp = itemsToPickUp.First();
            //Debug.Log("picking up items");

            while(pickingUpItem && itemsToPickUp.Count > 0){
            
                if(itemsToPickUp.Contains(itemtoPickUp)){
                    StartCoroutine(itemsToPickUp.First().walkingToItem(this));
                } else {
                    break;
                }
                    
                yield return null;
            }
            yield return new WaitForSeconds(1.5f);
            pickingUpItem = false;
            //Debug.Log("donw picking up item");
        }
    }

    public void moveItemToHouse(){

    }
    public void moveItemToWorkplace(){

    }

    // GETTERS
    public string getFirstName(){
        return citizenFirstName;
    }
    public string getLastName(){
        return citizenLastName;
    } 
    public BuildingAttributes getCitizenHouse(){
        return home;
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
    public Skills getSkills(){
        return skills;
    }
    public Inventory getInventory(){
        return inventory;
    }
    public bool getIsMovingToDestination(){
        return isMovingToDestination;
    }
    public int getAmountToPayInTax(){
        if(townAlliegence != null){
            return (int)((float)income * townAlliegence.getTaxPercentage()) + (int)(wealth * townAlliegence.getPropertyTaxPercentage());
        }
        return 0;
    }
    public string getGender(){
        return gender;
    }
    public int getAge(){
        return age;
    }
    public string getJob(){
        return job;
    }
    public int getHappiness(){
        return happiness;
    }
    public int getWealth(){
        return wealth;
    }
    public BuildingAttributes getWork(){
        return work;
    }
    public BuildingAttributes getHome(){
        return home;
    }
    public ResourceAttributes getResourceBeingMined(){
        return resourceBeingMined;
    }

    // SETTERS
    public void setHouse(BuildingAttributes building){
        home = building;
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
    public void setJob(string name){
        job = name;
    }
    public void setResourceBeingMined(ResourceAttributes resource){
        resourceBeingMined = resource;
    }
    public void addItemToItemsToPickUp(ItemAttributes item){
        if(!itemsToPickUp.Contains(item)){
            itemsToPickUp.Add(item);
        }
    }
    public void removeItemFromItemsToPickUp(ItemAttributes item){
        itemsToPickUp.Remove(item);
    }
    public void setCitizenIndicator(bool val){
        transform.Find("mob_indicator/green_indicator").gameObject.SetActive(val);
    }
    public int getIncome(){
        return income; 
    }
    public string getStatus(){
        return status;
    }
    public int getHealth(){
        return health;
    }
    public int getHunger(){
        return hunger;
    }
    public int getThrist(){
        return thirst;
    }
}
