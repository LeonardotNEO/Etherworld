using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT WILL SIT ON CITIZEN

public class Soldier : MonoBehaviour
{
    public Citizen citizen;
    public int positionInCohort;
    public bool inPosition;

    void Awake()
    {
        citizen = GetComponent<Citizen>();
    }
}
