using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCatalog : MonoBehaviour
{
    public List<Town> allTowns = new List<Town>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Town> getAllTowns(){
        return allTowns;
    }

    public void addTownToAllTown(Town town){
        allTowns.Add(town);
    }
    public void removeTownFromAllTown(Town town){
        allTowns.Remove(town);
    }


}
