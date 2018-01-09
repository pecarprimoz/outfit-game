using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    //AsteroidExplode defines how many times the asteroid will separate
    //based on its size
    private int asteroidExplode;

    //OutOfBounds checker reference to the script
    private OOBChecker asteroidOOB;

    //Asteroid speed,ridgid body and size
    private float asteroidSpeed;
    public Rigidbody2D rb2D;
    private float asteroidSize;

    //Start function, defines the asteroid size and it's parameters
    void Start () {
        asteroidSize = Random.Range(0.3f, 1);
        if(asteroidSize>0.8 && asteroidSize <= 1)
        {
            asteroidExplode = 2;
        }
        else if (asteroidSize > 0.7 && asteroidSize <= 0.8)
        {
            asteroidExplode = 1;
        }
        else if (asteroidSize <= 0.7)
        {
            asteroidExplode = 0;
        }
        transform.Rotate(transform.forward * Random.Range(0,360));
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
        asteroidSpeed = Random.Range(3, 4);
        rb2D = GetComponent<Rigidbody2D>();
        asteroidOOB = GetComponent<OOBChecker>();
    }

    //Every frame update asteroid position, float in space
	void Update () {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;
        transform.position += transform.up * asteroidSpeed * Time.deltaTime;
        asteroidOOB.checkIfOOB(posX, posY, posZ);
    }

    //If asteroid gets hit, handle the seperation here
    public void HandleAsteroidExplosion()
    {
        this.asteroidExplode--;
        this.asteroidSize -= 0.2f;
        transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);
        //Can explode to max 2 diffrent asteroids
        if (this.asteroidExplode > 0)
        {
            Instantiate(this);
            Instantiate(this);
        }
    }
}
