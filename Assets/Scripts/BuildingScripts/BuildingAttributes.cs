using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingAttributes : MonoBehaviour
{
    GameManager gameManager;
    GameObject buildingMenu;
    GameObject thisBuilding;
    public bool isOwnedByPlayer;
    //public List<NPC> npcsAssignedHere;
    public bool isRented;
    public bool buildingIsProducing;
    public string buildingTag;
    private int buildingValue;
    public int buildingID;
    public string buildingName;
    public string buildingDescription;
    public int storageCapacity;
    public float productionProgress;
    public Item itemCurrentlyProduced;
    public bool playerEnteredBuilding;

    public Dictionary<string, int> buildingUpKeep;
    public Inventory buildingInventory;
    public Dictionary<string, int> itemsProducedInBuilding;
    public Dictionary<string, int> itemsNeededForProduction;

    public bool playerInBoundsBuilding;
    public float positionX;
    public float positionY;
    public float positionZ;
    public bool collidingWithOtherObject;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
    }
    void Start()
    {
        gameManager.getBuildingCatalog().increaseAmountOfBuildingsInGame(1);
        gameManager.getBuildingCatalog().addBuildingToWorld(this);

        buildingTag = gameObject.tag;
        buildingValue = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getInitialValue();
        buildingID = gameManager.getBuildingCatalog().getAmountOfBuildingsInGame();
        transform.name += " " + buildingID;
        buildingName = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getNameOfBuilding();
        buildingDescription = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getDescriptionOfBulding();
        storageCapacity = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getStorageCapacity();

        buildingInventory = transform.GetComponent<Inventory>();
        buildingUpKeep = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getBuildingUpKeep();
        itemsNeededForProduction = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getNeededForProduction();
        itemsProducedInBuilding = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getBuildingProduction();
        storageCapacity = gameManager.getBuildingCatalog().getBuildingByName(transform.name).getStorageCapacity();

        if(GetComponent<Collider>()){
            positionX = GetComponent<Collider>().bounds.center.x;
            positionY = GetComponent<Collider>().bounds.center.y;
            positionZ = GetComponent<Collider>().bounds.center.z;
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

    void OnTriggerStay(Collider other)
    {
        if(
            other.gameObject.layer == 12  || /*Layer 12 is BUILDINGS*/
            other.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            other.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
        ){setCollidingWithOtherObject(true);} 
        if(
            other.gameObject.layer == 15      /*Layer 15 is PLAYER*/

        ){setPlayerInBoundsBuilding(true);} 
    }
    void OnTriggerExit(Collider other)
    {
        setCollidingWithOtherObject(false);
        setPlayerInBoundsBuilding(false);
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
    public void setPositionX(int newPositionX){
        positionX = newPositionX;
    }
    public void setPositionY(int newPositionY){
        positionY = newPositionY;
    }
    public void setPositionZ(int newPositionZ){
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
}
