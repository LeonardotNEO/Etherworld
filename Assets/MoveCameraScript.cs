using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{
    GameManager gameManager;
    public float positionX;
    public float positionY;
    public float positionZ;
    public float cameraSensitivity = 50;
    public float scrollSensitivity = 200;
    public float rotateSensitivity = 50;

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
        if(Input.GetKey("space")){
            //IMPROVE THIS!
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(gameManager.getPlayerBehavior().getPlayerPosition().x, gameManager.getPlayerBehavior().getPlayerPosition().y + 8, gameManager.getPlayerBehavior().getPlayerPosition().z);
        }
        if(!gameManager.getCraftingSystem().getIsCrafting()){
            if(Input.GetKey("q")){
                GameObject.FindGameObjectWithTag("MainCamera").transform.RotateAround(GameObject.FindGameObjectWithTag("MainCamera").transform.position, Vector3.up, +cameraSensitivity * Time.deltaTime * 10);
            }
            if(Input.GetKey("e")){
                GameObject.FindGameObjectWithTag("MainCamera").transform.RotateAround(GameObject.FindGameObjectWithTag("MainCamera").transform.position, Vector3.up, -cameraSensitivity * Time.deltaTime * 10);
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && 50F > positionY && positionY > 0F && !gameManager.getIsMouseOverUI()){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.up * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            //WHEN ZOOMING OUT, THE CAMERA MOVES BACK FROM PIVOT POINT
            GameObject.FindGameObjectWithTag("MainCamera2").transform.Translate(Vector3.back * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionY = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && 60F > positionY && positionY > 8F && !gameManager.getIsMouseOverUI()){
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(Vector3.down * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            //WHEN ZOOMING IN, THE CAMERA MOVES FORWARD TO PIVOT POINT
            GameObject.FindGameObjectWithTag("MainCamera2").transform.Translate(Vector3.forward * Time.deltaTime * scrollSensitivity, GameObject.FindGameObjectWithTag("MainCamera").transform);
            positionY = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;               
        }
    }
}
