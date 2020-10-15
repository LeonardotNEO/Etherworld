using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCatalog : MonoBehaviour
{
    public List<Inventory> inventoryCatalog;

    void Awake()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown("k")){
            Debug.Log(inventoryCatalogToString());
        }
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

    public string inventoryCatalogToString(){
        string inventoryCatalogToString = "";
        foreach(var inventory in inventoryCatalog){
            inventoryCatalogToString += 
            "------------INVENTORY------------- " 
            + "\nINVENTORY SIZE:\n" + inventory.getInventorySize() + "\n";
        }
        return inventoryCatalogToString;
    }

    public int getAmountOfInventoriesInCatalog(){
        return inventoryCatalog.Count;
    }

    public Dictionary<string, int> getInventoryByIndex(int index){
        return inventoryCatalog[index].getInventory();
    }
}
