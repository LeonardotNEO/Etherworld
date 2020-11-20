using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    List<ResourceAttributes> stoneDepots = new List<ResourceAttributes>();

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.name.Contains("StoneDepot")){
            Debug.Log("added");
            stoneDepots.Add(other.GetComponent<ResourceAttributes>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.name.Contains("StoneDepot")){
            Debug.Log("removed");
            stoneDepots.Remove(other.GetComponent<ResourceAttributes>());
        }
    }
}
