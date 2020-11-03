using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public int clockSpeed = 1000;
    public float years;
    public float months;
    public float days;
    public float hours;
    public float minutes; 
    public float seconds;
    public float rawTime;   
    public float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rawTime = Time.time;
        time = rawTime * clockSpeed;
        years = time / 31104000;
        months = years % 1 * 12;
        days = months % 1 * 30;
        hours = days % 1 * 24;
        minutes = hours % 1 * 60;
        seconds = minutes % 1 * 60;
    }

    public void setClockSpeed(int multiplier){
        clockSpeed = multiplier;
    }
    public int getYears(){
        return (int)years;
    }
    public int getMonths(){
        return (int)months;
    }
    public int getDays(){
        return (int)days;
    }
    public int getHours(){
        return (int)hours;
    }
    public int getMinutes(){
        return (int)minutes;
    }
    public int getSeconds(){
        return (int)seconds;
    }
}
