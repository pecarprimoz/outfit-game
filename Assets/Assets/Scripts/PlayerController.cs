using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour {
    private OOBChecker PlayerOOB;
    public float speedRotate;
    public float speedMove;
    public float hoverSpeed;
    public Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        PlayerOOB = GetComponent<OOBChecker>();
    }
    void FixedUpdate()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;
        //Kontrole
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * speedMove * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.forward * speedRotate);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.forward * (-1*speedRotate));
        }
        if (hoverSpeed >= 0.1f)
        {
            hoverSpeed = 0.1f;
        }
        //V vsakem primeru se premikam malo nazaj cuz space
        transform.position -= transform.up * hoverSpeed * Time.deltaTime;
        //Preverim ali je igralec zapustil območje kamere
        PlayerOOB.checkIfOOB(posX, posY, posZ);
    }
}

