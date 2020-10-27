using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCatalog : MonoBehaviour
{
    public List<Inventory> inventoryCatalog;

    public int amountOfInventoriesInCatalog;

    void Update()
    {
        amountOfInventoriesInCatalog = getAmountOfInventoriesInCatalog();
    }

    public void addInventoryToCatalog(Inventory inventory){
        inventoryCatalog.Add(inventory);
    }

    public void removeInventoryFromCatalog(Inventory inventory){
        inventoryCatalog.Remove(inventory);
    }

    public List<Inventory> getInventoryCatalog(){
        return inventoryCatalog;
    }

    public int getAmountOfInventoriesInCatalog(){
        return inventoryCatalog.Count;
    }

    public Inventory getInventoryByIndex(int index){
        return inventoryCatalog[index];
    }
    
    public Inventory getMainInventory(){
        return inventoryCatalog[0];
    }


    // TO STRING
    public string getListOfItemsToString(Dictionary<string, int> list){
        string listOfItems = "";
        if(list != null){
            foreach(var item in list){
                listOfItems += item.Value + " " + item.Key + "\n";
            }
        } else {
            listOfItems += "None";
        }
        return listOfItems;
    }
    public string inventoryCatalogToString(){
        string inventoryCatalogToString = "";
        foreach(var inventory in inventoryCatalog){
            inventoryCatalogToString += 
            "------------INVENTORY------------- " 
            + "\nINVENTORY SIZE:\n" + inventory.getInventoryCapacity() + "\n";
        }
        return inventoryCatalogToString;
    }
}
