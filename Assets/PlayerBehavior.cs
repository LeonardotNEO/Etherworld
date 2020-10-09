using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public Vector3 offset = new Vector3(3.6F, 0.2225568F, 0.3F);
    public float playerSpeed = 2;
    RaycastHit hit;
    bool isMoving = false;
    void Start()
    {
        
    }
    
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ground") {
                isMoving = true;
            } else {
                isMoving = false;
            }
            
        }

        if(isMoving == true){
            transform.position = Vector3.MoveTowards(transform.position, hit.point + offset, playerSpeed * Time.deltaTime);
        }
    }

}
