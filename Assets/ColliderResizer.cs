using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResizer : MonoBehaviour
{
    void Start()
    {
        GetComponent<BoxCollider>().size = new Vector3(transform.Find("Green indicator").GetComponent<SpriteRenderer>().size.x, this.transform.position.y, transform.Find("Green indicator").GetComponent<SpriteRenderer>().size.y);
    }
}
