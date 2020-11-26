using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenCatalog : MonoBehaviour
{
    public List<Citizen> allCitizens = new List<Citizen>();
    public Citizen selectedCitizen;

    public void addCitizenToAllCitizens(Citizen citizen){
        allCitizens.Add(citizen);
    }
    public void removeCitizenFromAllCitizens(Citizen citizen){
        allCitizens.Remove(citizen);
    }
    public int getAmountOfCitizens(){
        return allCitizens.Count;
    }
    public void setSelectedCitizen(Citizen citizen){
        selectedCitizen = citizen;
    }
    public void resetSelectedCitizen(){
        selectedCitizen = null;
    }
    public Citizen getSelectedCitizen(){
        return selectedCitizen;
    }
    public Citizen getCitizenByID(int ID){
        Citizen returnCitizen = null;
        foreach(Citizen citizen in allCitizens){
            if(citizen.getCitizenID() == ID){
                returnCitizen = citizen;
            }
        }
        return returnCitizen;
    }
    
}
