using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingHouse : MonoBehaviour
{
    public List<Citizen> availableWorkers;
    public int boardingHouseCapacity = 20;
    Vector3 boardingHousePosition;
    void Start()
    {
        boardingHousePosition = GetComponent<BoxCollider>().bounds.center;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Citizens") && other.gameObject.GetComponent<Citizen>().getIsLookingForWork()){
            availableWorkers.Add(other.gameObject.GetComponent<Citizen>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Citizens") && other.gameObject.GetComponent<Citizen>().getIsLookingForWork()){
            availableWorkers.Remove(other.gameObject.GetComponent<Citizen>());
        }
    }

    // GETTERS
    public List<Citizen> getAvailableWorkers(){
        return availableWorkers;
    }
    public int getBoardingHouseCapacity(){
        return boardingHouseCapacity;
    }
    public int getAmountOfAvailableWorkers(){
        return availableWorkers.Count;
    }
    public Vector3 getBoardingHousePosition(){
        return boardingHousePosition;
    }

    // SETTERS
    public void addAvailableWorker(Citizen citizen){
        availableWorkers.Add(citizen);
    }
    public void removeAvailableWorker(Citizen citizen){
        availableWorkers.Remove(citizen);
    }
}
