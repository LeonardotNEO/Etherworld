using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCatalog : MonoBehaviour
{
    [SerializeField] public List<Town> allTowns = new List<Town>();

    public List<Town> getAllTowns(){
        return allTowns;
    }

    public void addTownToAllTown(Town town){
        allTowns.Add(town);
    }
    public void removeTownFromAllTown(Town town){
        allTowns.Remove(town);
    }
    public Town getNearestTown(){
        Town nearestTown = null;
        // ALGORYTME FOR Å FINNE NÆRMESTE BY
        return nearestTown;
    }

}
