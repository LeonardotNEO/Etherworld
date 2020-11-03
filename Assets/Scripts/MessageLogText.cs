using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MessageLogText : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void addMessageToLog(string message){
        DateTime timeNow = DateTime.Now;

        GetComponent<Text>().text += timeNow.Hour + "." + timeNow.Minute + "." + timeNow.Second + ": " + message + "\n";
    }
}
