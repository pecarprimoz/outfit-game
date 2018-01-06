using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;

    // Use this for initialization
    void Awake()
    {
        asteroid = GameObject.Find("Asteroid");
        asteroid2 = GameObject.Find("Asteroid2");
        asteroid3 = GameObject.Find("Asteroid3");

        for (int i = 0; i < 1; i++)
        {
            Instantiate(asteroid);
        }
        for (int i = 0; i < 1; i++)
        {
            Instantiate(asteroid2);
        }
        for (int i = 0; i < 1; i++)
        {
            Instantiate(asteroid3);
        }

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }    
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
