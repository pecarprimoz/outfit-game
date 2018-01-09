using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {

    public GameObject explosionPrefab;

    //Collision detection between bullets and diffrent objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            GameObject planet = col.gameObject;
            AsteroidController ac = planet.GetComponent<AsteroidController>();
            ac.HandleAsteroidExplosion();
            GameController gc = GameController.instance;
            gc.IncrementScore();
            gc.PlayAsteroidExplosionSound();
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Enemy")
        {
            GameController gc = GameController.instance;
            AIController enemyC = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AIController>();
            gc.IncrementScore();
            if (enemyC.getAIHP() > 0)
            {
                enemyC.decrementHP();
            }
            else {
                Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
                gc.PlayExplosionSound();
                Destroy(col.gameObject);
            }
            Destroy(gameObject);
        }
    }   
}
