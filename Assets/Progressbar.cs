using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progressbar : MonoBehaviour
{
    RectTransform progressBar;
    void Awake()
    {
        
    }

    void Update()
    {
        // Setting up progressbar, and that it will update each frame
        progressBar = GetComponent<RectTransform>();
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
