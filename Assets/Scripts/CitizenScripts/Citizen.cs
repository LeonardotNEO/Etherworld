using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{
    GameManager gameManager;
    NavMeshAgent citizenAgent;
    public string gender;
    public string citizenName;
    public int age;
    public List<Skill> skills;
    public Inventory inventory;
    public int health;
    public int hunger;
    public int thirst;
    public int happiness;
    public int houseID;
    public int workplaceID;
    public int movementSpeed;
    public string status;
    public string townAlliegence;
    public string citizensLord;
    public string job;
    public int wealth;
    public Vector3 position;
    public List<Citizen> relatives;
    //public List<Hobby> hobbies;
    //public List<Personalities> personalityTraits;
    //public List<Ambition> ambitions; 

    // BOOLS //
    public bool isMovingToDestination;
    public bool reachedDestination;
    public bool isMovingToWork;
    public bool movedToWork;
    public bool isMovingToHome;
    public bool movedToHome;
    public bool isResting;
    public bool isWorking;
    public bool isGatheringResources;
    public bool isSick;
    public bool isHungry;
    public bool isThirsty;
    public bool isSleepy;
    public bool isLookingForWork;
    public bool isLookingForPartner;
    public bool isDoingRandomAction;
    public bool isMating;
    public bool isInTheArmy;
    public bool isInsideBuilding;

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
            movedToHome = false;
            StartCoroutine(goToWork());
        }
        if(gameManager.getClock().getHours() == 4 && gameManager.getClock().getMinutes() == 0){
            movedToWork = false;
            StartCoroutine(goHome());
        }
        if(gameManager.getClock().getHours() == 6 && gameManager.getClock().getMinutes() == 0){
            movedToHome = false;
            StartCoroutine(goToWork());
        }
        if(gameManager.getClock().getHours() == 8 && gameManager.getClock().getMinutes() == 0){
            movedToWork = false;
            StartCoroutine(goHome());
        }
        if(gameManager.getClock().getHours() == 10 && gameManager.getClock().getMinutes() == 0){
            movedToHome = false;
            StartCoroutine(goToWork());
        }
        if(gameManager.getClock().getHours() == 12 && gameManager.getClock().getMinutes() == 0){
            movedToWork = false;
            StartCoroutine(goHome());
        }
        if(gameManager.getClock().getHours() == 14 && gameManager.getClock().getMinutes() == 0){
            movedToHome = false;
            StartCoroutine(goToWork());
        }
        if(gameManager.getClock().getHours() == 16 && gameManager.getClock().getMinutes() == 0){
            movedToWork = false;
            StartCoroutine(goHome());
        }
        if(gameManager.getClock().getHours() == 18 && gameManager.getClock().getMinutes() == 0){
            movedToHome = false;
            StartCoroutine(goToWork());
        }
        if(gameManager.getClock().getHours() == 20 && gameManager.getClock().getMinutes() == 0){
            movedToWork = false;
            StartCoroutine(goHome());
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

        if(houseID == 0){
            List<BuildingAttributes> houses = gameManager.getBuildingCatalog().getBuildingInWorldByTag("Residential");
            houseID = houses[Random.Range(1, houses.Count)].getBuildingID();
        }

        if(workplaceID == 0){
            List<BuildingAttributes> workplaces = gameManager.getBuildingCatalog().getBuildingInWorldByTag("Industrial");
            workplaceID = workplaces[Random.Range(1, workplaces.Count)].getBuildingID();
        }

        movementSpeed = Random.Range(5,12); 
        citizenAgent.speed = movementSpeed;

        status = null;
        townAlliegence = null;
        citizensLord = null;
        wealth = Random.Range(0,100);
        position = transform.position;
        relatives = null;
    }

    public void goToDestination(){

    }
    public IEnumerator goToWork(){
        if(!movedToWork){
            isMovingToWork = true;
            BuildingAttributes workPlace = gameManager.getBuildingCatalog().getBuildingInWorldByID(workplaceID);
            Vector3 positionWork = new Vector3(workPlace.getPositionX(), workPlace.getPositionY(), workPlace.getPositionZ());

            if(isInsideBuilding == true){
                leaveBuilding();
            }

            citizenAgent.SetDestination(positionWork);

        
            while(isMovingToWork){
                if(citizenAgent.hasPath){
                    if(citizenAgent.remainingDistance <= 0.4){
                        movedToWork = true;
                        citizenAgent.ResetPath();
                        goInsideBuilding();
                        break;
                    }
                }
                if(isMovingToHome){
                    break;
                }
                yield return null;
            }
            isMovingToWork = false;
        }
    }
    public IEnumerator goHome(){
        if(!movedToHome){
            isMovingToHome = true;
            BuildingAttributes home = gameManager.getBuildingCatalog().getBuildingInWorldByID(houseID);
            Vector3 positionHome = new Vector3(home.getPositionX(), home.getPositionY(), home.getPositionZ());

            if(isInsideBuilding == true){
                leaveBuilding();
            }

            citizenAgent.SetDestination(positionHome);

            while(isMovingToHome){
                if(citizenAgent.hasPath){
                    if(citizenAgent.remainingDistance <= 0.4){
                        movedToHome = true;
                        citizenAgent.ResetPath();
                        goInsideBuilding();
                        break;
                    }
                }
                if(isMovingToWork){
                    break;
                }
                yield return null;
            }
            isMovingToHome = false;
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
}
