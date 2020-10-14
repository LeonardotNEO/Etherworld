using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCategoriesToMenu : MonoBehaviour
{
    public GameObject ObjectInView;

    List<string> buttonNames = new List<string>();
    
    void Start()
    {
        addToList("Houses");
        addToList("Farming");
        addToList("Shops");
        addToList("Tools");
        addToList("Weapons");
        addToList("Armor");
        addToList("Misc");

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
