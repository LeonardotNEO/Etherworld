using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    float healthStartWidth;
    int startHealth = 0;


    void Awake()
    {
        healthStartWidth = this.transform.Find("Health").transform.GetComponent<RectTransform>().sizeDelta.x;
    }
    void Start()
    {
        startHealth = this.transform.GetComponentInParent<EnemyAttributes>().getHealth();
    }
    void Update()
    {
        this.transform.Find("Background").LookAt(GameObject.FindGameObjectWithTag("MainCamera2").transform.position);
        this.transform.Find("Health").LookAt(GameObject.FindGameObjectWithTag("MainCamera2").transform.position);
    }

    public void updateHealthBar(){
        if(transform.gameObject.activeSelf){
            if(this.transform.GetComponentInParent<EnemyAttributes>().getHealth() != 0 && startHealth == 0){
                startHealth = this.transform.GetComponentInParent<EnemyAttributes>().getHealth();
            }

            if(startHealth != 0){

                float currentWidth = (float)(healthStartWidth/startHealth) * this.transform.GetComponentInParent<EnemyAttributes>().getHealth();
                this.transform.Find("Health").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(currentWidth, 0.1524451f);
            }
        }
    }
}
