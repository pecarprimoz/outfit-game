using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    private Rigidbody2D rb2D;
    public float scrollSpeed;
    void FixedUpdate()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(scrollSpeed, 0);
    }

    void Update()
    {
    }
}
