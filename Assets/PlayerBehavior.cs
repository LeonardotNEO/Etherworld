using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    //public Vector3 offset = new Vector3(3.6F, 0.2225568F, 0.3F);
    public float playerSpeed = 2;
    RaycastHit hit;
    public bool isMoving = false;
    void Start()
    {
        hit.point = transform.position;
    }
    
    void Update()
    {

        // CASTING A RAY FROM CAMERA TO COLLIDER WITH TAG GROUND
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ground"){
                isMoving = true;
            } else {
                isMoving = false;
            }
            
        }

        
        if(GetComponent<InventorySystem>().gatheringsResourcesRunning == true){
            GetComponent<Animator>().SetBool("isGatheringResources" , true);
        } else {
            GetComponent<Animator>().SetBool("isGatheringResources" , false);
        }

        // CHECK IF PLAYER HAS REACHED HIT.POINT, IF IT HAS, SET TO WALKING TO FALSE
        if(transform.position == hit.point){
            isMoving = false;
        }

        // MOVES THE PLAYER TO POSITION
        if(isMoving == true){
            transform.position = Vector3.MoveTowards(transform.position, hit.point /*+ offset*/, playerSpeed * Time.deltaTime);
            GetComponent<Animator>().SetBool("isMoving" , true);
            transform.LookAt(hit.point);
        } else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }
}
