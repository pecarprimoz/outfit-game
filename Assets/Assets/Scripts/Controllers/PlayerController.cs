﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speedRotate;
    public float speedMove;
    public float hoverSpeed;
    public float timeSinceShot = 0.0f;
    private float fireRate = 0.2f;
    private int playerHP;

    private OOBChecker PlayerOOB;
    private Rigidbody2D rb2D;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    private Transform bulletSpawn;

    private AudioSource audioSource;
    public AudioClip audioShoot;

    //If player collides with PickupDPS, reduce his fireRate
    public void reduceFireRate()
    {
        if(fireRate>0.1f)
            fireRate -= 0.1f;
    }
    public int getPlayerHP()
    {
        return playerHP;
    }
    public void reduceHP()
    {
        playerHP--;
    }
    public void setPlayerHP(int num)
    {
        playerHP = num;
    }
    void Start()
    {
        //Initialise the components
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        bulletSpawn = rb2D.transform;
        PlayerOOB = GetComponent<OOBChecker>();
    }
    //Every time frame, without delays update player the player position
    void FixedUpdate()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        //Controls
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * speedMove * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.forward * speedRotate);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.forward * (-1 * speedRotate));
        }
        if (Input.GetKey(KeyCode.S) && Time.time > timeSinceShot)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = Color.white;
            timeSinceShot = Time.time + fireRate;
            Fire();
        }
        if (hoverSpeed >= 0.1f)
        {
            hoverSpeed = 0.1f;
        }
        //We always float back because space
        transform.position -= transform.up * hoverSpeed * Time.deltaTime;
        //Preverim ali je igralec zapustil območje kamere
        PlayerOOB.checkIfOOB(posX, posY, posZ);

    }
    void Fire()
    {
        audioSource.PlayOneShot(audioShoot,1.0f);
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation
            );
        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * 6;
        Destroy(bullet, 3.0f);
    }

    //Collision detection between diffrent objects
    void OnCollisionEnter2D(Collision2D col)
    {
        GameController gc = GameController.instance;
        if (col.gameObject.tag == "Planet")
        {
            gc.PlayExplosionSound();
            Instantiate(explosionPrefab,this.transform.position,this.transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
            GameController.instance.HandlePlayerRespawn();
        }
        if(col.gameObject.tag == "ProjEnemy")
        {
            gc.PlayExplosionSound();
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
            GameController.instance.HandlePlayerRespawn();
        }
        if (col.gameObject.tag == "Enemy")
        {
            gc.PlayExplosionSound();
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
            GameController.instance.HandlePlayerRespawn();
        }
    }
}
