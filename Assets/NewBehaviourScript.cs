using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Rigidbody rb; 
    public int forceX;
    public int forceY;
    public int forceZ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() //when we do physics stuff, use FixedUpdate instead of Update
    {

        if(Input.GetKey("w")){
            rb.AddForce(1000 * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("d")){
            rb.AddForce(0, 0, -1000 * Time.deltaTime);
        }
        if(Input.GetKey("s")){
            rb.AddForce(-1000 * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("a")){
            rb.AddForce(0, 0, 1000 * Time.deltaTime);
        }
    }

}
