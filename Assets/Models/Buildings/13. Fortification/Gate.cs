using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool gateOpen = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool getGateOpen(){
        return gateOpen;
    }
    public void setGateOpen(bool val){
        gateOpen = val;
    }
}
