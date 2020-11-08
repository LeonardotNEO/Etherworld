using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    public string item;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(delegate {GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetUI().selectItemButton(item); });
    }

    public void setItem(string itemos){
        item = itemos;
    }
    public string getItem(){
        return item;
    }
}