using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    
    Inventory mainInventory;
    void Start()
    {
        mainInventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();

    }


    void Update()
    {
        
    }
}
