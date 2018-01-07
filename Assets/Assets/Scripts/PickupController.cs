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
        //Does not work, try to figure out why
        else if(col.gameObject.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

        if(col.gameObject.tag == "Player")
        {
            GameController gc = GameController.instance;
            gc.IncrementHP();
            Destroy(gameObject);
        }
    }
}
