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
        for (int i = 0; i < 2; i++)
        {
            Instantiate(asteroid);
            Instantiate(asteroid2);
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
