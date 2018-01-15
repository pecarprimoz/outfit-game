using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSController : MonoBehaviour
{

    //Variables for enemy
    public GameObject projectileEnemy;
    private int AIHP;
    private Vector3 destPosition;
    private float moveSpeed;
    private Transform bulletSpawn;

    //Variables for player
    private GameObject playerClone;
    private Vector3 playerDirection;

    //Enemy audio source and shoot sound
    public AudioClip enemyShoot;
    private AudioSource audioSource;

    //Enemy can get hit 3 times, getter for HP
    public int getAIHP()
    {
        return AIHP;
    }

    //Decrement hp if enemy gets hit
    public void decrementHP()
    {
        AIHP--;
    }

    //Initialiser
    void Start()
    {
        //BOSS can get hit 3 times before exploding
        AIHP = 5;

        //Get player reference, set temp player direction
        playerClone = GameObject.FindGameObjectWithTag("Player");
        playerDirection = new Vector3(-1, -1, -2);

        //Set the temporary destination for enemy to follow, set enemy
        //speed, set bulletSpawn position
        destPosition = new Vector3(-1, -1, -2);
        moveSpeed = 2.0f;
        bulletSpawn = GetComponent<Rigidbody2D>().transform;

        //Get audio source reference
        audioSource = GetComponent<AudioSource>();

        //Generate the first destenatin
        GenerateNewDest();
    }

    //Enemy moves to the point picked in GenerateNewDest
    //when it reaches the point it shoots a projectile thorwards player
    void Update()
    {
        MoveToPoint();
        if (transform.position.Equals(destPosition) && playerClone != null)
        {
            playerDirection = (playerClone.transform.position - this.transform.position).normalized;
            Shoot();
            GenerateNewDest();
        }
    }

    //Create a new bullet, play the shoot sound and bullet ignores
    //enemys collision
    void Shoot()
    {
        audioSource.PlayOneShot(enemyShoot, 1.0f);
        var bullet = (GameObject)Instantiate(
            projectileEnemy,
            bulletSpawn.position,
            bulletSpawn.rotation
            );
        Physics2D.IgnoreCollision(bullet.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = playerDirection * 6;
        Destroy(bullet, 3.0f);
    }

    //Generate a new destination based on game screen
    void GenerateNewDest()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize / 2;
        float width = height * cam.aspect;
        float destX = Random.Range(0.33f, width);
        float destY = Random.Range(0.33f, height);
        destPosition = new Vector3(destX, destY, -2);
    }

    //Move to the generated point
    void MoveToPoint()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destPosition, step);
    }

    //Updated player reference if the player dies
    public void UpdatePlayerObject()
    {
        playerClone = GameObject.FindGameObjectWithTag("Player");
    }
}
