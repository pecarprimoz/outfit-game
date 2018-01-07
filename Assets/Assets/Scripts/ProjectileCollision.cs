using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {

	// Use this for initialization
	
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            GameObject planet = col.gameObject;
            AsteroidController ac = planet.GetComponent<AsteroidController>();
            ac.HandleAsteroidExplosion();
            GameController gc = GameController.instance;
            gc.IncrementScore();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
    
        
}
