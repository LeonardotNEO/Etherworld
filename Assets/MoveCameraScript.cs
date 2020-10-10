using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
    public float positionX = 0;
    public float positionY = 0;
    public float positionZ = 0;
    public float cameraSensitivity = 50;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w")){
            positionX += cameraSensitivity * Time.deltaTime;
        }
        if(Input.GetKey("s")){
            positionX -= cameraSensitivity * Time.deltaTime;
        }
        if(Input.GetKey("a")){
            positionZ += cameraSensitivity * Time.deltaTime;
        }
        if(Input.GetKey("d")){
            positionZ -= cameraSensitivity * Time.deltaTime;
        }
        if(Input.GetKey("space")  && 50 > positionY && positionY > 3.5F){
            positionY += cameraSensitivity * Time.deltaTime;
        }
        if(Input.GetKey("left shift") && 60 > positionY && positionY > 4){
            positionY -= cameraSensitivity * Time.deltaTime;
        }

        transform.position = new Vector3(positionX, positionY, positionZ);
    }
}
