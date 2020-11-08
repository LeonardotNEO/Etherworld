using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingAttributes : MonoBehaviour
{
    GameManager gameManager;
    public GameObject buildingMenu;
    public GameObject thisBuilding;
    public Town apartOfTown;
    public List<Citizen> citizensWorkingInBuilding  = new List<Citizen>();
    public List<Citizen> citizensLivingInBuilding  = new List<Citizen>();
    public List<Citizen> citizensInBuilding = new List<Citizen>();
    public Inventory buildingInventory;
    public Dictionary<string, int> buildingUpKeep;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForProduction;
    public Item itemCurrentlyProduced;
    
    public string buildingTag;
    public string buildingName;
    public string buildingDescription;
    
    private int buildingValue;
    public int buildingID;
    public int storageCapacity;
    public float productionProgress;
    public int residentialLimit;
    public int workerLimit;

    public float positionX;
    public float positionY;
    public float positionZ;

    public bool isOwnedByPlayer;
    public bool isRented;
    public bool buildingIsProducing;
    public bool playerEnteredBuilding;
    public bool playerInBoundsBuilding;
    public bool collidingWithOtherObject;
    

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
            citizen.goToBuilding(null);
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
        if(
            other.gameObject.layer == 12  || /*Layer 12 is BUILDINGS*/
            other.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            other.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
        ){setCollidingWithOtherObject(true);} 
        if(
            other.gameObject.layer == 15      /*Layer 15 is PLAYER*/

        ){setPlayerInBoundsBuilding(true);} 

        if(other.gameObject.GetComponent<Town>()){
            setTownBuildingIsApartOf(other.gameObject.GetComponent<Town>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        setCollidingWithOtherObject(false);
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
    }

    void OnMouseExit()
    {
        if(!gameManager.GetUI().getIsMouseOverUI()){
            gameManager.GetUI().closeBuildingUI();
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
    }
    public void removeWorkerFromBuilding(Citizen citizen){
        citizensWorkingInBuilding.Remove(citizen);
        citizen.setWork(null);
    }
    public void addResidentToBuilding(Citizen citizen){
        citizensLivingInBuilding.Add(citizen);
        citizen.setHouse(this);
    }
    public void removeResidentFromBuilding(Citizen citizen){
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
}
