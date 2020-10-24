using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaountNPC : MonoBehaviour
{
    // Start is called before the first frame update
    int number;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("g")){
            number+=10;
            this.GetComponent<Text>().text = number.ToString();
        }
    }
}
