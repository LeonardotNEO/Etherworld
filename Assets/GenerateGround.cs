﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGround : MonoBehaviour
{
    public GameObject planePrefab;
    public GameObject treePrefab;
    public GameObject stonePrefab;
    public int amountTreesToSpawn;
    public int actualSpawnedTrees;
    public int amountStoneToSpawn;
    public int actualSpawnedStone;

   void Awake()
   {
        amountTreesToSpawn = 0;
        actualSpawnedTrees = 0;
        amountStoneToSpawn = 0;
        actualSpawnedStone = 0;
        foreach(Transform spawnedItem in gameObject.transform){
            GameObject.Destroy(spawnedItem.gameObject);
        }
   }
    void Start()
    {
        amountTreesToSpawn = Random.Range(500, 1500);
        for(int i = 0; i < amountTreesToSpawn; i++){
            
            GameObject spawn = Instantiate(treePrefab, new Vector3(Random.Range(transform.position.x + 150, transform.position.x - 150), transform.position.y, Random.Range(transform.position.z + 150, transform.position.z - 150)), transform.rotation);
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(spawn.transform.position.x, spawn.transform.position.y + 4.5f, spawn.transform.position.z), 2f);
            spawn.transform.parent = gameObject.transform;
            if(intersecting.Length != 4){
                Destroy(spawn);
                actualSpawnedTrees--;
            }
            actualSpawnedTrees++;
        }

        amountStoneToSpawn = Random.Range(100, 200);
        for(int i = 0; i < amountStoneToSpawn; i++){
            
            GameObject spawn = Instantiate(stonePrefab, new Vector3(Random.Range(transform.position.x + 150, transform.position.x - 150), transform.position.y, Random.Range(transform.position.z + 150, transform.position.z - 150)), transform.rotation);
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z), 0.5f);
            spawn.transform.parent = gameObject.transform;
            //for(int y = 0; y < intersecting.Length; y++){
            //    Debug.Log(intersecting[y].name);
            //}
            if(intersecting.Length != 4){
                Destroy(spawn);
                actualSpawnedStone--;
            }
            actualSpawnedStone++;
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player"){
            //Checking if colliders are inside a sphere at this vector position
            Collider[] intersectingFront = Physics.OverlapSphere(new Vector3(transform.position.x + 300, transform.position.y, transform.position.z), 10f);
            Collider[] intersectingFrontRight = Physics.OverlapSphere(new Vector3(transform.position.x + 300, transform.position.y, transform.position.z - 300), 10f);
            Collider[] intersectingFrontLeft = Physics.OverlapSphere(new Vector3(transform.position.x + 300, transform.position.y, transform.position.z + 300), 10f);
            Collider[] intersectingBack = Physics.OverlapSphere(new Vector3(transform.position.x - 300, transform.position.y, transform.position.z), 10f);
            Collider[] intersectingBackRight = Physics.OverlapSphere(new Vector3(transform.position.x - 300, transform.position.y, transform.position.z - 300), 10f);
            Collider[] intersectingBackLeft = Physics.OverlapSphere(new Vector3(transform.position.x - 300, transform.position.y, transform.position.z + 300), 10f);
            Collider[] intersectingRight = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - 300), 10f);
            Collider[] intersectingLeft = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 300), 10f);

            Debug.Log(intersectingFront.Length);
            //If there are no colliders inside the sphere (0), instansiate a new plane, since there are none
            if(intersectingBack.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x - 300, transform.position.y, transform.position.z), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Back intersecting");
            }
            if(intersectingBackRight.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x - 300, transform.position.y, transform.position.z - 300), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Backright intersecting");
            }
            if(intersectingBackLeft.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x - 300, transform.position.y, transform.position.z + 300), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Backleft intersecting");
            }
            if(intersectingFront.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x + 300, transform.position.y, transform.position.z), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Front intersecting");
            }
            if(intersectingFrontRight.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x + 300, transform.position.y, transform.position.z - 300), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Frontright intersecting");
            }
            if(intersectingFrontLeft.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x + 300, transform.position.y, transform.position.z + 300), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Frontleft intersecting");
            }
            if(intersectingRight.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 300), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Right intersecting");
            }
            if(intersectingLeft.Length == 0){
                GameObject plane = Instantiate(planePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + 300), transform.rotation);
                plane.transform.parent = GameObject.Find("Ground").transform;
                Debug.Log("Left intersecting");
            }
        }
    }
}