using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MessageLogText : MonoBehaviour
{
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void addMessageToLog(string message){
        int hours = gameManager.getClock().getHours();
        int minutes = gameManager.getClock().getMinutes();
        int seconds = gameManager.getClock().getSeconds();
        string stringHours = hours.ToString();
        string stringMinutes = minutes.ToString();
        string stringSeconds = seconds.ToString();

        if(hours < 10){
            stringHours = "0" + stringHours;
        }
        if(minutes < 10){
            stringMinutes = "0" + stringMinutes;
        }
        if(seconds < 10){
            stringSeconds = "0" + stringSeconds;
        }

        GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background/Scroll View/Viewport/Content/Text").GetComponent<Text>().text += stringHours + "." + stringMinutes + "." + stringSeconds + ": " + message + "\n";
        GameObject.FindGameObjectWithTag("MessageLogBarUI").transform.Find("Background/Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 0;
    }
}
