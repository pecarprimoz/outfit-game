using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    private GameObject playerClone;
    public GameObject projectileEnemy;
    public AudioClip enemyShoot;
    private Vector3 destPosition;
    private float moveSpeed;
    private Transform bulletSpawn;
    private Vector3 playerDirection;
    private int AIHP;
    private AudioSource audioSource;

    public int getAIHP()
    {
        return AIHP;
    }
    public void decrementHP()
    {
        AIHP--;
    }

    void Start () {
        AIHP = 3;
        playerClone = GameObject.FindGameObjectWithTag("Player");
        destPosition = new Vector3(-1, -1, -2);
        moveSpeed = 2.0f;
        playerDirection = new Vector3(-1,-1,-2);
        bulletSpawn = GetComponent<Rigidbody2D>().transform;
        audioSource = GetComponent<AudioSource>();
        GenerateNewDest();
    }
	
	void Update () {
        MoveToPoint();

        if(transform.position.Equals(destPosition) && playerClone!=null)
        {
            playerDirection = (playerClone.transform.position - this.transform.position).normalized;
            Shoot();
            GenerateNewDest();
        }
    }
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
    
    void GenerateNewDest()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize / 2;
        float width = height * cam.aspect;
        float destX = Random.Range(0.33f, width);
        float destY = Random.Range(0.33f, height);
        destPosition = new Vector3(destX, destY, -2);

    }
    void MoveToPoint()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destPosition, step);   
    }
    public void UpdatePlayerObject()
    {
        playerClone = GameObject.FindGameObjectWithTag("Player");
    }
}
