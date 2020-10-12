using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAttributes : MonoBehaviour
{
    public int amountLeft = 6;
    public int progress = 0;
    public GameObject resourceMined;
    public bool firstTriggered = false;
    public bool secondTriggered = false;
    public bool thirdTriggered = false;
    public bool fourthTriggered = false;
    public bool fifthTriggered = false;
    public bool sixtTriggered = false; 
    void Update()
    {
        
    }

    public GameObject getResourceMined(){
        return resourceMined;
    }

    public void decreaseAmountLeft(){
        amountLeft--;
    }

    public void increaseProgress(int addProgress){
        progress += addProgress;
    }

}
