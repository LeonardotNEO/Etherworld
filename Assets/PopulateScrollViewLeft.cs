using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollViewLeft : MonoBehaviour
{
    public GameObject ObjectInView;
    int numberToGenerate = 7;
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

        for(int i = 0; i < numberToGenerate; i++){

            elements = (GameObject)Instantiate(ObjectInView, transform);
            elements.GetComponentInChildren<Text>().text = buttonNames[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToList(string NameOfButton){
        buttonNames.Add(NameOfButton);
    }
}
