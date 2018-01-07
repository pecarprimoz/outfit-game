using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    private int asteroidExplode;
    private OOBChecker asteroidOOB;
    private float asteroidSpeed;
    public Rigidbody2D rb2D;
    private float asteroidSize;

    void Start () {
        asteroidSize = Random.Range(0.3f, 1);
        if(asteroidSize>0.7 && asteroidSize <= 1)
        {
            asteroidExplode = 2;
        }
        else if (asteroidSize > 0.5 && asteroidSize <= 0.7)
        {
            asteroidExplode = 1;
        }
        else if (asteroidSize <= 0.5)
        {
            asteroidExplode = 0;
        }
        transform.Rotate(transform.forward * Random.Range(0,360));
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
        asteroidSpeed = Random.Range(3, 4);
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
    public void HandleAsteroidExplosion()
    {
        this.asteroidExplode--;
        this.asteroidSize -= 0.2f;
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
        if (this.asteroidExplode > 0)
        {
            Instantiate(this);
            Instantiate(this);
        }
    }
}
