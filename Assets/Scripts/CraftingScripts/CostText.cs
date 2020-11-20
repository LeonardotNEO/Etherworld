using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostText : MonoBehaviour
{
    string itemName;
    int amount;


    public void setItemName(string name){
        itemName = name;
    }
    public void setAmount(int val){
        amount = val;
    }

    public string getItemName(){
        return itemName;
    }
    public int getAmount(){
        return amount;
    }
}
