using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public int itemAmount;
    public string itemName;

    void Awake()
    {
        itemName = this.tag;
    }

    public int getItemAmount(){
        return itemAmount;
    }
    public string getItemName(){
        return itemName;
    }
    
}
