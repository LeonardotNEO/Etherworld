using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
    GameManager gameManager;
    public float positionX;
    public float positionY;
    public float positionZ;
    public float cameraSensitivity = 75;
    public float scrollSensitivity = 350;
    public float rotateSensitivity = 50;
    public float heightconstant = 0.02f;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(Input.GetKey("w")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.forward * cameraSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
        }
        if(Input.GetKey("s")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.back * cameraSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
        }
        if(Input.GetKey("a")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.left * cameraSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionZ = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
        }
        if(Input.GetKey("d")){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.right * cameraSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionZ = GameObject.FindGameObjectWithTag("MainCamera").transform.position.z;
        }
        if(Input.GetKey("space")){
            //IMPROVE THIS!
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(gameManager.getPlayerBehavior().getPlayerPosition().x, gameManager.getPlayerBehavior().getPlayerPosition().y + 8, gameManager.getPlayerBehavior().getPlayerPosition().z);
        }
        if(!gameManager.getCraftingSystem().getIsCrafting()){
            if(Input.GetKey("q")){
                GameObject.FindGameObjectWithTag("MainCamera").transform.RotateAround(GameObject.FindGameObjectWithTag("MainCamera").transform.position, Vector3.up, +rotateSensitivity);
            }
            if(Input.GetKey("e")){
                GameObject.FindGameObjectWithTag("MainCamera").transform.RotateAround(GameObject.FindGameObjectWithTag("MainCamera").transform.position, Vector3.up, -rotateSensitivity);
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && 50F > positionY && positionY > 0F && !gameManager.GetUI().getIsMouseOverUI()){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.up * scrollSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            //WHEN ZOOMING OUT, THE CAMERA MOVES BACK FROM PIVOT POINT
            GameObject.FindGameObjectWithTag("MainCamera2").transform.Translate(Vector3.back * scrollSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionY = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && 60F > positionY && positionY > 8F && !gameManager.GetUI().getIsMouseOverUI()){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.down * scrollSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            //WHEN ZOOMING IN, THE CAMERA MOVES FORWARD TO PIVOT POINT
            GameObject.FindGameObjectWithTag("MainCamera2").transform.Translate(Vector3.forward * scrollSensitivity * positionY * heightconstant, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionY = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;               
        }
    }
}
