using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class BuildingAttributes : MonoBehaviour
{
    GameManager gameManager;
    public GameObject buildingMenu;
    public GameObject thisBuilding;
    public Town apartOfTown;
    public List<Citizen> citizensWorkingInBuilding  = new List<Citizen>();
    public List<Citizen> citizensLivingInBuilding  = new List<Citizen>();
    public List<Citizen> citizensInBuilding = new List<Citizen>();
    public List<Citizen> citizensGatheringResources = new List<Citizen>();
    public List<BuildingAttributes> buildingscollidingWith = new List<BuildingAttributes>();
    public Inventory buildingInventory;
    public Dictionary<string, int> buildingUpKeep;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForProduction;
    public Item itemCurrentlyProduced;
    
    public string buildingTag;
    public string buildingName;
    public string buildingDescription;
    public string jobName;
    
    private int buildingValue;
    public int buildingID;
    public int storageCapacity;
    public int residentialLimit;
    public int workerLimit;
    public int gatherResourcesThreshold = 10;
    public int putItemInStorageThreshold = 20;
    public int amountToTransfer = 10;
    public int taxIncome;
    public int buildingAgeHours; 
    public int buildingMaxHealth;
    public int buildingHealth;

    public float productionProgress;
    public float positionX;
    public float positionY;
    public float positionZ;

    public bool isOwnedByPlayer;
    public bool isRented;
    public bool buildingIsProducing;
    public bool someoneIsMovingFromWorkToStorage;
    public bool playerEnteredBuilding;
    public bool playerInBoundsBuilding;
    public bool collidingWithOtherObject;
    public bool buildingOutsideOfTown;
    

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
    }
    void Start()
    {
        gameManager.getBuildingCatalog().increaseAmountOfBuildingsInGame(1);
        gameManager.getBuildingCatalog().addBuildingToWorld(this);
        buildingInventory = transform.GetComponent<Inventory>();
        buildingTag = gameObject.tag;

        //setTownBuildingIsApartOf(null);
        setBuildingValue(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getInitialValue());
        setBuildingID(gameManager.getBuildingCatalog().getAmountOfBuildingsInGame());
        transform.name += " " + buildingID;
        setBuildingName(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getNameOfBuilding());
        setBuildingDescription(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getDescriptionOfBulding());
        setBuildingStorageCapacity(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getStorageCapacity());
        setResidentialLimit(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getResidentialLimit());
        setWorkerLimit(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getWorkerLimit());
        setBuildingUpKeep(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getBuildingUpKeep());
        setItemsNeededForBuildingProduction(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getNeededForProduction());
        setItemsProducedInBuilding(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getBuildingProduction());
        setJobName(gameManager.getBuildingCatalog().getBuildingByName(transform.name).getJobName());

        // SET OBJECT INDICATOR TO PROPER WITH AND DEPTH
        if(transform.Find("Object Indicator")){
            if(GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>()){
                transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size = new Vector2(GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.x, GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.z);
                transform.Find("Object Indicator/Red indicator").GetComponent<SpriteRenderer>().size = new Vector2(GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.x, GetComponentInChildren<MeshRenderer>().transform.parent.GetComponent<BoxCollider>().size.z);
            } else {
                transform.Find("Object Indicator/Green indicator").GetComponent<SpriteRenderer>().size = new Vector2(GetComponentInChildren<NavMeshObstacle>().size.x * GetComponentInChildren<NavMeshObstacle>().transform.localScale.x, GetComponentInChildren<NavMeshObstacle>().size.z  * GetComponentInChildren<NavMeshObstacle>().transform.localScale.z);
                transform.Find("Object Indicator/Red indicator").GetComponent<SpriteRenderer>().size = new Vector2(GetComponentInChildren<NavMeshObstacle>().size.x * GetComponentInChildren<NavMeshObstacle>().transform.localScale.x, GetComponentInChildren<NavMeshObstacle>().size.z  * GetComponentInChildren<NavMeshObstacle>().transform.localScale.z);
            }
            
            buildingIndicator(false);
        }

        // POSITION
        if(GetComponent<BoxCollider>()){
            positionX = GetComponent<BoxCollider>().bounds.center.x;
            positionY = GetComponent<BoxCollider>().bounds.center.y;
            positionZ = GetComponent<BoxCollider>().bounds.center.z;
        } else {
            positionX = gameObject.transform.position.x;
            positionY = gameObject.transform.position.y;
            positionZ = gameObject.transform.position.z;
        }
        
        thisBuilding = this.gameObject;

    }

    void Update()
    {
        
    }

    void OnDisable()
    {
        foreach(Citizen citizen in citizensLivingInBuilding){
            citizen.goToBuilding(null);
            citizen.setHouse(null);
            citizen.setTownAlliegence(null);
        }
        foreach(Citizen citizen in citizensWorkingInBuilding){
            citizen.leaveBuilding();
            citizen.goToTownCenter();
            citizen.setWork(null);
        }
        if(apartOfTown){
            apartOfTown.removeBuildingFromTown(this);
            apartOfTown.removeBoardingHouseFromTown(this);
            apartOfTown.removeResidentialBuildingFromTown(this);
            apartOfTown.removeResidentialBuildingFromTown(this);
            apartOfTown.removeInventoryFromTown(buildingInventory);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Town>()){
            setTownBuildingIsApartOf(other.gameObject.GetComponent<Town>());
        }

        if(
            other.gameObject.layer == 12  || /*Layer 12 is BUILDINGSMESH*/
            other.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            other.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
        )
        {
            if(other.transform.gameObject.GetComponentInParent<BuildingAttributes>()){
                buildingscollidingWith.Add(other.transform.GetComponentInParent<BuildingAttributes>());
            }
            if(buildingscollidingWith.Count > 0){
                setCollidingWithOtherObject(true);
            }
        } 
        if(
            other.gameObject.layer == 15      /*Layer 15 is PLAYER*/

        ){setPlayerInBoundsBuilding(true);} 
    }

    void OnTriggerExit(Collider other)
    {
        if(
            other.gameObject.layer == 12  || /*Layer 12 is BUILDINGSMESH*/
            other.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            other.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
        )
        {
            if(other.transform.gameObject.GetComponentInParent<BuildingAttributes>()){
                buildingscollidingWith.Remove(other.transform.GetComponentInParent<BuildingAttributes>());
                if(buildingscollidingWith.Count == 0){
                    setCollidingWithOtherObject(false);
                }
            }
        }

        setPlayerInBoundsBuilding(false);
        if(other.gameObject.GetComponent<Town>()){
            apartOfTown = null;
        }
    }

    void OnMouseOver()
    {
        if(!gameManager.GetUI().getIsMouseOverUI() && Input.GetMouseButtonDown(1)){
            gameManager.getBuildingCatalog().setBuildingLastClicked(thisBuilding);
            gameManager.GetUI().openBuildingUI(this.gameObject);
        }
        if(!gameManager.GetUI().getIsMouseOverUI() && Input.GetMouseButtonDown(0)){
            gameManager.getBuildingCatalog().setBuildingLastClicked(thisBuilding);

            if(gameManager.getBuildingCatalog().getBuildingLastClickedAttributes().getIsOwnedByPlayer()){
                gameManager.GetUI().buildingOpenClose();
                gameManager.GetUI().buildingOpen();
            }
        }

       if(collidingWithOtherObject){
            buildingIndicator(true);
            buildingIndicatorMode(false);
        } else {
            buildingIndicator(true);
            buildingIndicatorMode(true);
        }
    }

    void OnMouseExit()
    {
        if(!gameManager.GetUI().getIsMouseOverUI()){
            gameManager.GetUI().closeBuildingUI();
        }
        /*
        buildingIndicator(false);*/
        if(!collidingWithOtherObject){
            buildingIndicator(false);
            buildingIndicatorMode(true);
        }
    }

    //GETTERS
    public Dictionary<string, int> getBuildingUpKeep(){
        return buildingUpKeep;
    }
    public Inventory getBuildingInventory()
    {
        return buildingInventory;
    }
    public Dictionary<string, int> getItemsProducedInBuilding()
    {
        return itemsProducedInBuilding;
    }
    public Dictionary<string, int> getItemsNeededForBuildingProduction()
    {
        return itemsNeededForProduction;
    }
    public bool getIsOwnedByPlayer(){
        return isOwnedByPlayer;
    }
    public string getBuildingTag(){
        return buildingTag;
    }
    public int getBuildingValue(){
        return buildingValue;
    }
    public int getBuildingID()
    {
        return buildingID;
    }
    public string getBuildingName(){
        return buildingName;
    }
    public string getBuildingDescription(){
        return buildingDescription;
    }

    public int getStorageCapacity(){
        return storageCapacity;
    }
    public float getPositionX()
    {
        return positionX;
    }
    public float getPositionY()
    {
        return positionY;
    }
    public float getPositionZ()
    {
        return positionZ;
    }
    public bool getCollidingWithOtherObject(){
        return collidingWithOtherObject;
    }
    public GameObject getThisBuilding(){
        return thisBuilding;
    }
    public bool getPlayerInBoundsBuilding(){
        return playerInBoundsBuilding;
    }
    public bool getIsRented(){
        return isRented;
    }
    public bool getBuildingIsProducing(){
        return buildingIsProducing;
    }
    public float getProductionProgress(){
        return productionProgress;
    }
    public Item getItemCurrentlyProduced(){
        return itemCurrentlyProduced;
    }
    public bool getPlayerEnteredBuilding(){
        return playerEnteredBuilding;
    }
    public float getBuildingPositionX(){
        return positionX;
    }
    public float getBuildingPositionY(){
        return positionY;
    }
    public float getBuildingPositionZ(){
        return positionZ;
    }
    public BuildingAttributes getBuildingAttributes(){
        return this;
    }
    public List<Citizen> getWorkersInBuilding(){
        return citizensWorkingInBuilding;
    }
    public Town getTownBuildingIsApartOf(){
        return apartOfTown;
    }
    public List<Citizen> getResidentsInBuilding(){
        return citizensLivingInBuilding;
    }
    public int getResidentialLimit(){
        return residentialLimit;
    }
    public int getWorkerLimit(){
        return workerLimit;
    }
    public List<Citizen> getCitizensInsideBuilding(){
        return citizensInBuilding;
    }
    public int getGatherResourcesThreshold(){
        return gatherResourcesThreshold;
    }
    public int getPutItemInStorageThreshold(){
        return putItemInStorageThreshold;
    }
    public int getAmountToTransfer(){
        return amountToTransfer;
    }
    public List<Citizen> getCitizensGatheringResources(){
        return citizensGatheringResources;
    }
    public bool getSomeoneIsMovingFromWorkToStorage(){
        return someoneIsMovingFromWorkToStorage;
    }
    public int getBuildingTotalTaxPaymentDaily(){
        int totalAmount = 0;
        foreach(Citizen citizen in getResidentsInBuilding()){
            totalAmount += citizen.getAmountToPayInTax();
        }
        return totalAmount;
    }
    public string getJobName(){
        return jobName;
    }
    public bool getBuildingOutsideOfTown(){
        return buildingOutsideOfTown;
    }

    //SETTERS
    public void setItemsProducedInBuilding(Dictionary<string, int> newItemsProduced)
    {
        itemsProducedInBuilding = newItemsProduced;
    }
    public void setItemsNeededForBuildingProduction(Dictionary<string, int> newItemsNeededForProduction)
    {
        itemsNeededForProduction = newItemsNeededForProduction;
    }
    public void setBuildingID(int newBuildingID){
        buildingID = newBuildingID;
    }
    public void setPositionX(float newPositionX){
        positionX = newPositionX;
    }
    public void setPositionY(float newPositionY){
        positionY = newPositionY;
    }
    public void setPositionZ(float newPositionZ){
        positionZ = newPositionZ;
    }
    public void setCollidingWithOtherObject(bool set){
        collidingWithOtherObject = set;
        if(collidingWithOtherObject){
            buildingIndicator(true);
            buildingIndicatorMode(false);
        } else {
            buildingIndicator(false);
            buildingIndicatorMode(true);
        }
    }
    public void setIsOwnedByPlayer(bool value){
        isOwnedByPlayer = value;
    }
    public void setBuildingName(string name){
        buildingName = name;
    }
    public void setPlayerInBoundsBuilding(bool val){
        playerInBoundsBuilding = val;
    }
    public void setIsRented(bool val){
        isRented = val;
    }
    public void setBuildingIsProducing(bool val){
        buildingIsProducing = val;
    }
    public void setIncreaseProductionProgress(float progress){
        productionProgress += progress;
    }
    public void setDecreaseProductionProgress(float progress){
        productionProgress -= progress;
    }
    public void setResetProductionProgress(){
        productionProgress = 0;
    }
    public void setItemCurrentlyProduced(Item item){
        itemCurrentlyProduced = item;
    }
    public void setPlayerEnteredBuilding(bool val){
        playerEnteredBuilding = val;
    }
    public void addWorkerToBuilding(Citizen citizen){
        citizensWorkingInBuilding.Add(citizen);
        citizen.setWork(this);
        if(gameManager.GetUI().getBuildingOpenOpen()){
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Workers Here/Scroll View/Viewport/Content").GetComponent<ShowWorkersInBuilding>().updateWorkersInBuildingList();
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Available workers/Scroll View/Viewport/Content").GetComponent<ShowAvailableWorkers>().updateAvailableWorkersList();
        }
    }
    public void removeWorkerFromBuilding(Citizen citizen){
        if(citizen.getBuildingInsideOf()){
            if(citizen.getBuildingInsideOf().Equals(this)){
                citizen.leaveBuilding();
                citizen.goToTownCenter();
            }
        }

        citizensWorkingInBuilding.Remove(citizen);
        citizen.setWork(null);
        if(gameManager.GetUI().getBuildingOpenOpen()){
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Workers Here/Scroll View/Viewport/Content").GetComponent<ShowWorkersInBuilding>().updateWorkersInBuildingList();
            GameObject.FindGameObjectWithTag("BuildingOpenUI").transform.Find("Background/Workers/Available workers/Scroll View/Viewport/Content").GetComponent<ShowAvailableWorkers>().updateAvailableWorkersList();
        }
    }
    public void addResidentToBuilding(Citizen citizen){
        citizensLivingInBuilding.Add(citizen);
        citizen.setHouse(this);
    }
    public void removeResidentFromBuilding(Citizen citizen){
        if(citizen.getBuildingInsideOf()){
            if(citizen.getBuildingInsideOf().Equals(this)){
                citizen.leaveBuilding();
            }
        }

        citizensLivingInBuilding.Remove(citizen);
        citizen.setHouse(null);
    }
    public void setTownBuildingIsApartOf(Town town){
        apartOfTown = town;
    }
    public void setBuildingValue(int val){
        buildingValue = val;
    }
    public void setBuildingDescription(string description){
        buildingDescription = description;
    }
    public void setBuildingStorageCapacity(int cap){
        storageCapacity = cap;
    }
    public void setBuildingUpKeep(Dictionary<string, int> items){
        buildingUpKeep = items;
    }
    public void setBuildingTag(string tag){
        buildingTag = tag;
    }
    public void addCitizenToInsideBuilding(Citizen citizen){
        citizensInBuilding.Add(citizen);
    }
    public void removeCitizenFromInsideBuilding(Citizen citizen){
        citizensInBuilding.Remove(citizen);
    }
    public void setResidentialLimit(int limit){
        residentialLimit = limit;
    }
    public void setWorkerLimit(int limit){
        workerLimit = limit;
    }
    public void addCitizenToGatheringResources(Citizen citizen){
        citizensGatheringResources.Add(citizen);
    }
    public void removeCitizenFromGatheringResources(Citizen citizen){
        citizensGatheringResources.Remove(citizen);
    }
    public void setSomeoneIsMovingFromWorkToStorage(bool val){
        someoneIsMovingFromWorkToStorage = val;
    }
    public void setJobName(string name){
        jobName = name;
    }
    public void buildingIndicator(bool val){
        if(transform.Find("Object Indicator")){
            transform.Find("Object Indicator").transform.gameObject.SetActive(val);
        }
    }
    public void buildingIndicatorMode(bool val){
        if(transform.Find("Object Indicator")){
            if(val == false){
                transform.Find("Object Indicator/Red indicator").transform.gameObject.SetActive(true);
                transform.Find("Object Indicator/Green indicator").transform.gameObject.SetActive(false);
            } else {
                transform.Find("Object Indicator/Red indicator").transform.gameObject.SetActive(false);
                transform.Find("Object Indicator/Green indicator").transform.gameObject.SetActive(true);
            }
        }
    }
    public void setBuildingOutsideOfTown(bool val){
        buildingOutsideOfTown = val;
    }
}
