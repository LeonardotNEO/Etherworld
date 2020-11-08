using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    GameManager gameManager;
    Vector3 townCenter;
    Collider townCollider;
    
    public List<BuildingAttributes> buildingsInTown = new List<BuildingAttributes>();
    public List<BuildingAttributes> residentialBuildingsInTown = new List<BuildingAttributes>();
    public List<BuildingAttributes> availableResidentialBuildingsInTown = new List<BuildingAttributes>();
    public List<BuildingAttributes> productionBuildingsInTown = new List<BuildingAttributes>();
    public List<BuildingAttributes> storageBuildingsInTown = new List<BuildingAttributes>();
    public List<BuildingAttributes> boardingHousesInTown = new List<BuildingAttributes>();

    public List<Citizen> citizensInTown = new List<Citizen>();
    public List<Citizen> townCouncil = new List<Citizen>();
    public List<Citizen> availableWorkersInTown = new List<Citizen>();
    public List<Citizen> citizensWithoutHouse = new List<Citizen>();

    public List<Town> townTradingPartners = new List<Town>();
    public List<Town> townAllies = new List<Town>();
    public List<Town> townEnemies = new List<Town>();
    public List<Town> townAtWarWith = new List<Town>();
    public List<Town> townInAllianceWith = new List<Town>();

    public List<Inventory> townAllInventories = new List<Inventory>(); // adds all items innside collider to inventory

    public string townOwner;
    public string townName;
    public string townCurrentDisaster;

    public int townHappiness;
    public int townAttractivnes;
    public int townAmountOfHousing;
    public int townAmountOfHousingAvailable;
    public int townAmountOfEntertainment;
    public int townAmountOfJobs;
    public int townAmountOfJobsAvailable;
    public int townAmountOfCitizenWithoutWork;
    public float townTaxIncome;
    public float townExpenditure;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {

        List<string> townNames = new List<string>(){"Stardew", "Mountain Peak", "Rusty Valley", "Woodshore", "Brotown"};
        townName = townNames[Random.Range(0, townNames.Count)];
        gameObject.name = townName;
        
        townCenter = transform.GetComponent<BoxCollider>().bounds.center;
        townAttractivnes = 40;
        gameManager.GetTownCatalog().addTownToAllTown(this);

    }

    void Update()
    {

    }

    void OnDisable()
    {
        foreach(BuildingAttributes building in buildingsInTown){
            building.setTownBuildingIsApartOf(null);
        }
        foreach(Citizen citizen in citizensInTown){
            citizen.setTownAlliegence(null);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Buildings")){
            if(other.gameObject.GetComponent<BuildingAttributes>()){
                addBuildingToTown(other.gameObject.GetComponent<BuildingAttributes>());
                if(other.gameObject.GetComponent<BuildingAttributes>().getBuildingName() == "Boarding House"){
                    addBoardingHouseToTown(other.gameObject.GetComponent<BuildingAttributes>());
                }
                if(other.gameObject.GetComponent<BuildingAttributes>().getBuildingTag() == "Industrial"){
                    addProductionBuildingToTown(other.gameObject.GetComponent<BuildingAttributes>());
                }
                if(other.gameObject.GetComponent<BuildingAttributes>().getBuildingTag() == "Residential"){
                    addResidentialBuildingToTown(other.gameObject.GetComponent<BuildingAttributes>());
                }
            }
            if(other.gameObject.GetComponent<BuildingAttributes>().getTownBuildingIsApartOf() == this || other.gameObject.GetComponent<Citizen>().townAlliegence == this){
                if(other.gameObject.GetComponent<Inventory>()){
                    addInventoryToTown(other.gameObject.GetComponent<Inventory>());
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {

    }

    // GETTERS
    public string getTownName(){
        return townName;
    }
    public Vector3 getTownCenter(){
        return townCenter;
    }

    public int getAmountOfResourceInTown(string itemName){
        int amount = 0;
        foreach(Inventory inventory in townAllInventories){
            foreach(InventorySlot inventorySlot in inventory.getInventorySlots()){
                if(inventorySlot.getItemInSlot() == itemName){
                    amount += inventorySlot.getCurrentAmountInSlot();
                }
            }
        }
        return amount;
    }

    public List<BuildingAttributes> getBoardingHousesInTown(){
        return boardingHousesInTown;
    }

    public List<Citizen> getAvailableWorkersInTown(){
        return availableWorkersInTown;
    }

    public int getTownAttractivnes(){
        return townAttractivnes;
    }

    public List<BuildingAttributes> getAvailableResidentialBuildingsInTown(){
        List<BuildingAttributes> buildings = new List<BuildingAttributes>(); 
        foreach(BuildingAttributes building in residentialBuildingsInTown){
            if(building.getResidentsInBuilding().Count < building.getResidentialLimit()){
                buildings.Add(building);
            }
        }
        availableResidentialBuildingsInTown = buildings;
        return buildings;
    }

    public List<BuildingAttributes> getBuildingsInTown(){
        return buildingsInTown;
    }

    public List<Citizen> getCitizensInTown(){
        return citizensInTown;
    }
    public string getTownOwner(){
        return townOwner;
    }


    // SETTERS
    public void setTownName(string name){
        townName = name;
    }
    public void addBuildingToTown(BuildingAttributes building){
        buildingsInTown.Add(building);
    }
    public void removeBuildingFromTown(BuildingAttributes building){
        buildingsInTown.Remove(building);
    }
    public void addInventoryToTown(Inventory inventory){
        townAllInventories.Add(inventory);
    }
    public void removeInventoryFromTown(Inventory inventory){
        townAllInventories.Remove(inventory);
    }
    public void addCitizenToTown(Citizen citizen){
        citizensInTown.Add(citizen);
    }
    public void removeCitizenFromTown(Citizen citizen){
        citizensInTown.Remove(citizen);
    }
    public void addAvailableWorkerToTown(Citizen citizen){
        availableWorkersInTown.Add(citizen);
    }
    public void removeAvailableWorkerFromTown(Citizen citizen){
        availableWorkersInTown.Remove(citizen);
    }
    public void addBoardingHouseToTown(BuildingAttributes building){
        boardingHousesInTown.Add(building);
    }
    public void removeBoardingHouseFromTown(BuildingAttributes building){
        boardingHousesInTown.Remove(building);
    }
    public void addProductionBuildingToTown(BuildingAttributes building){
        productionBuildingsInTown.Add(building);
    }
    public void removeProductionBuildingFromTown(BuildingAttributes building){
        productionBuildingsInTown.Remove(building);
    }
    public void addResidentialBuildingToTown(BuildingAttributes building){
        residentialBuildingsInTown.Add(building);
    }
    public void removeResidentialBuildingFromTown(BuildingAttributes building){
        residentialBuildingsInTown.Remove(building);
    }
    public void setTownOwner(string name){
        townOwner = name;
    }
}
