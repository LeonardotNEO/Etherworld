using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    GameManager gameManager;
    float speed;
    float light1noShadowIntensity;
    float light1increment;
    float light2Intensity;
    float light2increment;

    //set rotation to 0 and activate script
    
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        speed = 0.4f;



        light1noShadowIntensity = transform.Find("Directional Light No Shadow").GetComponent<Light>().intensity;
        light2Intensity = transform.Find("Directional Light").GetComponent<Light>().intensity;

        light1increment = light1noShadowIntensity/230f;
        light2increment = light2Intensity/230f;

        transform.Find("Directional Light No Shadow").GetComponent<Light>().intensity = 0.0f;
        transform.Find("Directional Light").GetComponent<Light>().intensity = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speed);
        if(gameManager.getClock().getHours() >= 6){
            if(gameManager.getClock().getHours() <= 24){
                transform.Rotate (speed * Time.deltaTime,0,0);

                if(gameManager.getClock().getHours() >= 6 && gameManager.getClock().getHours() <= 12){
                    if(transform.Find("Directional Light No Shadow").GetComponent<Light>().intensity <= 0.5f){
                        transform.Find("Directional Light No Shadow").GetComponent<Light>().intensity += light1increment  * Time.deltaTime;
                    }
                    if(transform.Find("Directional Light").GetComponent<Light>().intensity <= 0.8f){
                        transform.Find("Directional Light").GetComponent<Light>().intensity += light2increment * Time.deltaTime;
                    }
                } 
                if(gameManager.getClock().getHours() >= 18 && gameManager.getClock().getHours() <= 24){
                    transform.Find("Directional Light No Shadow").GetComponent<Light>().intensity -= light1increment * 1.3f * Time.deltaTime;
                    transform.Find("Directional Light").GetComponent<Light>().intensity -= light2increment * 1.3f  * Time.deltaTime;
                }
                
            }
        } else {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }
}
