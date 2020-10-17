using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    public int buttonId = 0;

    void Awake()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buttonId = gameManager.getCraftingButtonID();
        gameManager.newCraftingButtonID();
    }

    public int getButtonId(){
        return buttonId;
    }
}
