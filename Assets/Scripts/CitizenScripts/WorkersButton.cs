using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkersButton : MonoBehaviour
{
    public int citizenID;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(delegate {GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetUI().selectWorkerButton(citizenID); });
    }

    public void setCitizenID(int ID){
        citizenID = ID;
    }
    public int getCitizenID(){
        return citizenID;
    }
}
