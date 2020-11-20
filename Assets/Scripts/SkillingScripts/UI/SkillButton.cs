using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void openUI(){
        transform.Find("Experience Panel").transform.gameObject.SetActive(true);
    }

    public void closeUI(){
        transform.Find("Experience Panel").transform.gameObject.SetActive(false);
    }
}
