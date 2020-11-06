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
    //public List<Hobby> hobbies;
    //public List<Personalities> personalityTraits;
    //public List<Ambition> ambitions; 

    // BOOLS //
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


    public bool isResting;
    public bool isWorking;
    public bool isGatheringResources;
    public bool isSick;
    public bool isHungry;
    public bool isThirsty;
    public bool isSleepy;
    public bool isLookingForPartner;
    public bool isDoingRandomAction;
    public bool isMating;
    public bool isInTheArmy;

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
        //make switch statement
        if(gameManager.getClock().getHours() == 2 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(work));
        }
        if(gameManager.getClock().getHours() == 4 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(house));
        }
        if(gameManager.getClock().getHours() == 6 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(work));
        }
        if(gameManager.getClock().getHours() == 8 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(house));
        }
        if(gameManager.getClock().getHours() == 10 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(work));
        }
        if(gameManager.getClock().getHours() == 12 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(house));
        }
        if(gameManager.getClock().getHours() == 14 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(work));
        }
        if(gameManager.getClock().getHours() == 16 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(house));
        }
        if(gameManager.getClock().getHours() == 18 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(work));
        }
        if(gameManager.getClock().getHours() == 20 && gameManager.getClock().getMinutes() == 0){
            StartCoroutine(goToBuilding(house));
        }

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

        if(work == null){ //Checks for time because npc needs time to load startwork and starthome
            lookForWork();
        }
        if(house == null){
            lookForHousing();
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
    }

    public IEnumerator initialisingCitizen(){
        yield return new WaitForSeconds(0.1f);

        List<string> genders = new List<string>{"Male", "Female"};
        gender = genders[Random.Range(0,genders.Count)];

        List<string> maleNames = new List<string>{"Hans","Peter","Ole","Leonard","Fredrik","Markus","Chad","Rick","Morty","Erlend","James","Bertil"};
        List<string> femaleNames = new List<string>{"Siri","Grete","Karen","Martha","Marte","Monica"};
        if(gender == "Male"){
            citizenName = maleNames[Random.Range(0, maleNames.Count)];
        } else {
            citizenName = femaleNames[Random.Range(0, femaleNames.Count)];
        }

        age = Random.Range(16,60);
        //skills
        inventory = transform.GetComponent<Inventory>();
        health = 100;
        hunger = 100;
        thirst = 100;
        happiness = 100;

        movementSpeed = Random.Range(5,12); 
        citizenAgent.speed = movementSpeed;

        status = null;
        if(house == null){
            townAlliegence = null;
        } else {
            if(house.getTownBuildingIsApartOf()){
                townAlliegence = house.getTownBuildingIsApartOf();
            }
        }
        citizensLord = null;
        wealth = Random.Range(0,100);
        position = transform.position;
        relatives = null;

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

    public void goToDestination(){

    }
    public IEnumerator goToBuilding(BuildingAttributes building){
        if(building != null){
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
                        goInsideBuilding();
                        break;
                    }
                }
                yield return null;
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

    public void goInsideBuilding(){
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<NavMeshAgent>().enabled = false;
        isInsideBuilding = true;
    }
    public void leaveBuilding(){
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<NavMeshAgent>().enabled = true;
        isInsideBuilding = false;
    }
    public void goToHangoutDestination(){

    }
    public void gatherResources(){

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
    
}
