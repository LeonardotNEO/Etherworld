using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> mainInventory;
    

    public Inventory(){

    }

    public void updateInventory(Item item){
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Item itemComponent = player.GetComponent<Item>();
        mainInventory.Add(item);
    }

    public string getInventory(){
        string getInventoryString = "";
        foreach(var item in mainInventory){
            getInventoryString += item.getAmount().ToString() + " " + item.getName() + " "; 
        }
        Debug.Log(mainInventory.Count);
        return getInventoryString;
    }

}
