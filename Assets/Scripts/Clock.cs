using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public int clockSpeed;
    public float years;
    public float months;
    public float days;
    public float hours;
    public float minutes; 
    public float seconds;
    public float rawTime;   
    public float time;

    public bool hour0;
    public bool hour1;
    public bool hour2;
    public bool hour3;
    public bool hour4;
    public bool hour5;
    public bool hour6;
    public bool hour7;
    public bool hour8;
    public bool hour9;
    public bool hour10;
    public bool hour11;
    public bool hour12;
    public bool hour13;
    public bool hour14;
    public bool hour15;
    public bool hour16;
    public bool hour17;
    public bool hour18;
    public bool hour19;
    public bool hour20;
    public bool hour21;
    public bool hour22;
    public bool hour23;

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
    public float getYearsFloat(){
        return years;
    }

    public int getMonths(){
        return (int)months;
    }
    public float getMonthsFloat(){
        return months;
    }

    public int getDays(){
        return (int)days;
    }
    public float getDaysFloat(){
        return days;
    }

    public int getHours(){
        return (int)hours;
    }
    public float getHoursFloat(){
        return hours;
    }

    public int getMinutes(){
        return (int)minutes;
    }
    public float getMinutesFloat(){
        return minutes;
    }

    public int getSeconds(){
        return (int)seconds;
    }
    public float getSecondsFloat(){
        return seconds;
    }

    public void resetHourTriggers(){
        hour0 = false;
        hour1 = false;
        hour2 = false;
        hour3 = false;
        hour4 = false;
        hour5 = false;
        hour6 = false;
        hour7 = false;
        hour8 = false;
        hour9 = false;
        hour10 = false;
        hour11 = false;
        hour12 = false;
        hour13 = false;
        hour14 = false;
        hour15 = false;
        hour16 = false;
        hour17 = false;
        hour18 = false;
        hour19 = false;
        hour20 = false;
        hour21 = false;
        hour22 = false;
        hour23 = false;
    }

    public void setHourTrigger(int hour, bool val){
        switch(hour){
            case 0:
                hour0 = val;
                break;
            case 1:
                hour1 = val;
                break;
            case 2:
                hour2 = val;
                break;
            case 3:
                hour3 = val;
                break;
            case 4:
                hour4 = val;
                break;
            case 5:
                hour5 = val;
                break;
            case 6:
                hour6 = val;
                break;
            case 7:
                hour7 = val;
                break;
            case 8:
                hour8 = val;
                break;
            case 9:
                hour9 = val;
                break;
            case 10:
                hour10 = val;
                break;
            case 11:
                hour11 = val;
                break;
            case 12:
                hour12 = val;
                break;
            case 13:
                hour13 = val;
                break;
            case 14:
                hour14 = val;
                break;
            case 15:
                hour15 = val;
                break;
            case 16:
                hour16 = val;
                break;
            case 17:
                hour17 = val;
                break;
            case 18:
                hour18 = val;
                break;
            case 19:
                hour19 = val;
                break;
            case 20:
                hour20 = val;
                break;
            case 21:
                hour21 = val;
                break;
            case 22:
                hour22 = val;
                break;
            case 23:
                hour23 = val;
                break;
            default:
                break;
        } 
    }

    public bool getHourTrigger(int hour){
        switch(hour){
            case 0:
                return hour0;
            case 1:
                return hour1;
            case 2:
                return hour2;
            case 3:
                return hour3;
            case 4:
                return hour4;
            case 5:
                return hour5;
            case 6:
                return hour6;
            case 7:
                return hour7;
            case 8:
                return hour8;
            case 9:
                return hour9;
            case 10:
                return hour10;
            case 11:
                return hour11;
            case 12:
                return hour12;
            case 13:
                return hour13;
            case 14:
                return hour14;
            case 15:
                return hour15;
            case 16:
                return hour16;
            case 17:
                return hour17;
            case 18:
                return hour18;
            case 19:
                return hour19;
            case 20:
                return hour20;
            case 21:
                return hour21;
            case 22:
                return hour22;
            case 23:
                return hour23;
            default:
                return false;
        } 
    }
}
