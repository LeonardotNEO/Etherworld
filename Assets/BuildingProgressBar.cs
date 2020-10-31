using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProgressBar : MonoBehaviour
{
    RectTransform progressBar;
    GameManager gameManager;
    BuildingAttributes buildingAttributes;
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        if(gameManager.getBuildingCatalog().getBuildingLastClicked() != null){
            buildingAttributes = gameManager.getBuildingCatalog().getBuildingLastClicked().GetComponent<BuildingAttributes>();
        }
    }
    void Update()
    {
        // Setting up progressbar, and that it will update each frame
        progressBar = GetComponent<RectTransform>();

        if(gameManager.getBuildingCatalog().getBuildingLastClicked() != null){
            progressBar.sizeDelta = new Vector2(buildingAttributes.getProductionProgress(), 26.4F);

        } 
    }
    public RectTransform getProgressBar(){
        return progressBar;
    }
    public void updateProgressBar(float progressNumber){
        progressBar.sizeDelta = new Vector2(progressNumber, 26.4F);
    }
    public void resetProgressBar(){
        progressBar.sizeDelta = new Vector2(0, 26F);
    }
}
