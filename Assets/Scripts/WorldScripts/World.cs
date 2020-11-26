using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class World : MonoBehaviour
{
    public List<ResourceAttributes> stoneDepots = new List<ResourceAttributes>();
    public List<ResourceAttributes> trees = new List<ResourceAttributes>();

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
            //Debug.Log("added");
            stoneDepots.Add(other.GetComponent<ResourceAttributes>());
        }
        if(other.gameObject.name.Contains("Tree")){
            //Debug.Log("added");
            trees.Add(other.GetComponent<ResourceAttributes>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.name.Contains("StoneDepot")){
            //Debug.Log("removed");
            stoneDepots.Remove(other.GetComponent<ResourceAttributes>());
        }
        if(other.gameObject.name.Contains("Tree")){
            //Debug.Log("removed");
            trees.Remove(other.GetComponent<ResourceAttributes>());
        }
    }

    public List<ResourceAttributes> getStoneDepots(){
        return stoneDepots;
    }
    public List<ResourceAttributes> getTrees(){
        return trees;
    }

    public ResourceAttributes findClosestResource(List<ResourceAttributes> resourceList, Vector3 fromPosition){

        List<ResourceAttributes> resourceArrangeAfterDistance = new List<ResourceAttributes>();
        Dictionary<ResourceAttributes, float> resourceAfterDistance = new Dictionary<ResourceAttributes, float>();

        foreach(ResourceAttributes resource in resourceList){
            if(resource != null){
                resourceAfterDistance.Add(resource, Vector3.Distance(fromPosition, resource.transform.position));
            }
        }

        foreach(KeyValuePair<ResourceAttributes, float> resource in resourceAfterDistance.OrderBy(key => key.Value)){
            resourceArrangeAfterDistance.Add(resource.Key);
        }

        if(resourceArrangeAfterDistance.Count > 0){
            return resourceArrangeAfterDistance[0];
        } else {
            return null;
        }
    }
}
