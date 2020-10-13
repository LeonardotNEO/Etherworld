using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollViewRight: MonoBehaviour
{
    public GameObject element;
    public int numberToGenerate;
    void Start()
    {
        for(int i = 0; i < numberToGenerate; i++){
            GameObject elements = (GameObject)Instantiate(element, transform);
        }
    }
}
