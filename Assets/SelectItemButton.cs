using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    public string item;

    void Start()
    {
        item = GetComponentInChildren<Text>().text;
        transform.GetComponent<Button>().onClick.AddListener(delegate {GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetUI().selectItemButton(item); });
    }
}