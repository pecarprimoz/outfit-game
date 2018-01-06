using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    private OOBChecker asteroidOOB;
    private float asteroidSpeed;
    public Rigidbody2D rb2D;
    private float asteroidSize;
    void Start () {
        asteroidSize = Random.Range(0.5f, 1);
        transform.Rotate(transform.forward * Random.Range(0,360));
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
        asteroidSpeed = Random.Range(3, 6);
        rb2D = GetComponent<Rigidbody2D>();
        asteroidOOB = GetComponent<OOBChecker>();
    }
	
	// Update is called once per frame
	void Update () {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        transform.position += transform.up * asteroidSpeed * Time.deltaTime;

        asteroidOOB.checkIfOOB(posX, posY, posZ);
    }
}
