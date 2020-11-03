using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    GameManager gameManager;
    public int buttonId;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buttonId = gameManager.getCraftingSystem().getCraftingButtonIDCounter();
        gameManager.getCraftingSystem().updateCraftingButtonIDCounter();
    }

    public int getButtonId(){
        return buttonId;
    }
}
