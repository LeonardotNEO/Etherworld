using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingBotton : MonoBehaviour
{
    BuildingAttributes building;
    public void setBuilding(BuildingAttributes building){
        this.building = building;
    }
    public BuildingAttributes getBuilding(){
        return building;
    }

    public void moveCameraToBuilding(){
        GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<MoveCameraScript>().moveCameraToPosition(building.transform.position);
    }
}
