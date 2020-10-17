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
    RaycastHit hitGround;
    RaycastHit resource;
    RaycastHit hitItemResource;
    RaycastHit hitItemResourceSaved;
    Vector3 playerPosition;
    void Start()
    {
        hitGround.point = transform.position;
    }
    
    void Update()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // PLAYER MOVEMENT
        // Move player to new position when pressing mouse click
        if (Input.GetMouseButtonDown(0) && !isMouseOverUI() && !gameManager.getPlacingBuilding()){
            Ray ray = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitGround, Mathf.Infinity, LayerMask.GetMask("Ground"));
        }  
        if(!reachedDestination){
            // Moves the player
            transform.position = Vector3.MoveTowards(transform.position, hitGround.point, playerSpeed * Time.deltaTime);
            transform.LookAt(hitGround.point);
            // Player moves slower is there is a obstacle (So that the player doesnt accidentaly clip through)
            if(touchingObstacle == true){
                transform.Translate(new Vector3(0f,0f,-0.25f), transform);
                hitGround.point = transform.position;
            }
        } 
        // check if player has reached new destination
        if(transform.position == hitGround.point){
            isMovingToDestination = false;
            reachedDestination = true;
            GetComponent<Animator>().SetBool("isMoving", false);
        } else {
            isMovingToDestination = true;
            reachedDestination = false;
            GetComponent<Animator>().SetBool("isMoving" , true);
        }
    


        // PLAYER HOVER OVER ITEMS, fix problem that one cant Outline ???
        Ray hoverRay = GameObject.FindGameObjectWithTag("MainCamera2").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(hoverRay, out hitItemResource, Mathf.Infinity, LayerMask.GetMask("ResourcesMesh", "ItemsMesh", "Buildings"))){
            hitItemResourceSaved = hitItemResource;
            mouseOnItemResource = true;
        } else {
            mouseOnItemResource = false;
        }
        
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

    // Triggers when player collides with other objects; sets collider bool to true if it collides with these objects
    void OnTriggerStay(Collider collider){
        colliderInfo = collider;
        if(
            colliderInfo.gameObject.layer == 12  || /*Layer 12 is BUILDINGS*/
            colliderInfo.gameObject.layer == 13  || /*Layer 13 is RESOURCESMESH*/
            colliderInfo.gameObject.layer == 14     /*Layer 14 is ITEMSMESH*/
            ){touchingObstacle = true;
        }
    }
    // no collider detected if player has moved out of collider
    void OnTriggerExit(Collider collider){
        colliderInfo = null;
        touchingObstacle = false;
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
    public bool getTouchingObstacle(){
        return touchingObstacle;
    }
    public void setTouchingObstacle(bool newBool){
        touchingObstacle = newBool;
    }
    public bool getMouseOnItemResource(){
        return mouseOnItemResource;
    }
    public RaycastHit getPlayerRaycastHit(){
        return hitGround;
    }
    public void setPlayerRaycastHitVector(Vector3 vector){
        hitGround.point = vector;
    }
    public Vector3 getPlayerPosition(){
        return transform.position;
    }
    public void setPlayerPosition(Vector3 newPosition){
        playerPosition = newPosition;
    }   
}
