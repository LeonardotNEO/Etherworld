using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCategoriesToMenu : MonoBehaviour
{
    public GameObject ObjectInView;

    List<string> buttonNames = new List<string>();
    
    void Awake()
    {
        addToList("Buildings");
        addToList("Items");

        addToList("Residential");
        addToList("Educational");
        addToList("Institutional");
        addToList("Health");
        addToList("Entertainment");
        addToList("Road");
        addToList("Mercantile");
        addToList("Industrial");
        addToList("Storage");
        addToList("Military");
        addToList("Farming");
        addToList("Religion");
        addToList("Misc");
        addToList("Fortificiation");



        GameObject elements;

        for(int i = 0; i < buttonNames.Count; i++){
            elements = (GameObject)Instantiate(ObjectInView, transform);
            elements.GetComponentInChildren<Text>().text = buttonNames[i];
        }
    }

    public void addToList(string NameOfButton){
        buttonNames.Add(NameOfButton);
    }
}
