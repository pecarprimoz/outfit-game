using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInf : MonoBehaviour {

    private BoxCollider2D backCollider;
    private float backHorizontalLength;
	void Start () {
        backCollider = GetComponent<BoxCollider2D>();
        backHorizontalLength = backCollider.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -backHorizontalLength)
        {
            RepositionBackground();
        }
	}

    private void RepositionBackground()
    {
        Vector2 backOffset = new Vector2(backHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + backOffset;
    }

}
