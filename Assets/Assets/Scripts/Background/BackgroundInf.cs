using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInf : MonoBehaviour {
    private Vector2 dimensions;
    private SpriteRenderer sr;
    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        dimensions = sr.size;
    }
	
	void Update () {
        if(transform.position.x < -dimensions.x)
        {
            Vector3 tmp = transform.position;
            tmp.x = -tmp.x;
            transform.position = tmp;
        }
	}
}
