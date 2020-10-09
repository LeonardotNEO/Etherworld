using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public NewBehaviourScript movement;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle"){
            Debug.Log("GODDAMN U HIT THE BALL");
            movement.enabled = false;
        }
    }
}
