using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public float cameraSensitivity = 50;
    public float scrollSensitivity = 200;
    public float rotateSensitivity = 50;

    void Start()
    {
        
    }

    void Update()
    {

        if(Input.GetKey("w")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.forward * Time.deltaTime * cameraSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
        }
        if(Input.GetKey("s")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.back * Time.deltaTime * cameraSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
        }
        if(Input.GetKey("a")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.left * Time.deltaTime * cameraSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionZ = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
        }
        if(Input.GetKey("d")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.right * Time.deltaTime * cameraSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionZ = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
        }
        if(Input.GetKey("z")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.RotateAround(GameObject.FindGameObjectWithTag("MainCamera").transform.position, Vector3.up, +cameraSensitivity * Time.deltaTime * 10);
        }
        if(Input.GetKey("x")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.RotateAround(GameObject.FindGameObjectWithTag("MainCamera").transform.position, Vector3.up, -cameraSensitivity * Time.deltaTime * 10);
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && 50F > positionY && positionY > 0F){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.up * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            //WHEN ZOOMING OUT, THE CAMERA MOVES BACK FROM PIVOT POINT
            GameObject.FindGameObjectWithTag("MainCamera2").transform.Translate(Vector3.back * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionY = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;


        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && 60F > positionY && positionY > 5F){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.down * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            //WHEN ZOOMING IN, THE CAMERA MOVES FORWARD TO PIVOT POINT
            GameObject.FindGameObjectWithTag("MainCamera2").transform.Translate(Vector3.forward * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionY = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;
            
        }

    }
}
