using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
        if (col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }
        //Does not work, try to figure out why
        if(col.gameObject.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

        if(col.gameObject.tag == "Player")
        {
            GameController gc = GameController.instance;
            gc.PlayPickupSound();
            if(gameObject.tag == "HP")
            {
                gc.IncrementHP();
            }
            else if (gameObject.tag == "FireRate")
            {
                gc.ReduceFireRate();
            }
            Destroy(gameObject);
        }
    }
}
