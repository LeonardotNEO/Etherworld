using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    GameManager gameManager;
    public float playerSpeed = 2;
    public bool isMovingToDestination;
    public bool reachedDestination;
    public bool touchingObstacle;
    public bool mouseOnItemResource;
    bool pingPong;
    public Collider colliderInfo;
    public RaycastHit hitGround;
    void Start()
    {
        stopPlayer();
    }
    
    void Update()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // PLAYER MOVEMENT
        // Move player to new position when pressing mouse click
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI() && !gameManager.getPlacingBuilding()){
            RaycastHit hit;
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);
            if(hit.collider.tag == "Ground"){
                hitGround.point = hit.point;
            }
            transform.LookAt(hitGround.point);
        } 
        // check if player has reached new destination
        if(transform.position == getHitGroundPosition()){
            isMovingToDestination = false;
            reachedDestination = true;
            GetComponent<Animator>().SetBool("isMoving", false);
        } else {
            GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, hitGround.point, playerSpeed * Time.deltaTime));
            isMovingToDestination = true;
            reachedDestination = false;
            GetComponent<Animator>().SetBool("isMoving" , true);
        }
        // PUT THIS SCRIPT ON ITEMS THAT WANT TO BE OUTLINED LIKE BUILDINGS; RESOURCE; ITEMS ETC  
        /*
        if(mouseOnItemResource){
            hitItemResource.collider.GetComponentInChildren<Outline>().eraseRenderer = false;

            Color c = GameObject.FindWithTag("MainCamera2").GetComponent<OutlineEffect>().lineColor0;
            if(pingPong)
            {
                c.a += Time.deltaTime;
                if(c.a >= 1)
                    pingPong = false;
            } else {
                c.a -= Time.deltaTime;
                if(c.a <= 0.4)
                    pingPong = true;
            }
            c.a = Mathf.Clamp01(c.a);
            GameObject.FindWithTag("MainCamera2").GetComponent<OutlineEffect>().lineColor0 = c;
            GameObject.FindWithTag("MainCamera2").GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        } else {
            if(hitItemResourceSaved.collider){
                hitItemResourceSaved.collider.GetComponentInChildren<Outline>().eraseRenderer = true;
            }
        }
        */
    }

    void OnTriggerStay(Collider colliderInfo){
        
    }
    // no collider detected if player has moved out of collider
    void OnTriggerExit(Collider collider){
        colliderInfo = null;
    }

    public bool isMouseOverUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }
    public float getPlayerSpeed(){
        return playerSpeed;
    }
    public void setPlayerSpeed(int newspeed){
        playerSpeed = newspeed;
    }
    public bool getIsMovingToDestination(){
        return isMovingToDestination;
    }
    public void setIsMovingToDestination(bool newBool){
        isMovingToDestination = newBool;
    }
    public bool getReachedDestination(){
        return reachedDestination;
    }
    public void setReachedDestination(bool newBool){
        reachedDestination = newBool;
    }
    public Vector3 getHitGroundPosition(){
        return hitGround.point;
    }
    public void setHitGroundPostion(Vector3 newPosition){
        hitGround.point = newPosition;
    }   
    public void stopPlayer(){
        setHitGroundPostion(transform.position);
    }
}
