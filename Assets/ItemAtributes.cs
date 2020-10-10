using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAtributes : MonoBehaviour
{
    public int amountLeft = 6;
    public int progress = 0;
    public bool firstTriggered = false;
    public bool secondTriggered = false;
    public bool thirdTriggered = false;
    public bool fourthTriggered = false;
    public bool fifthTriggered = false;
    public bool sixtTriggered = false; 
    void Update()
    {
        
    }

    public void decreaseAmountLeft(){
        amountLeft--;
    }

}
