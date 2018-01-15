using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public float scrollSpeed;

    void Start()
    {
    }
    void FixedUpdate()
    {
        Vector3 temp = transform.position;
        temp.x -= Time.deltaTime/3*scrollSpeed;
        transform.position = temp;
    }
}
