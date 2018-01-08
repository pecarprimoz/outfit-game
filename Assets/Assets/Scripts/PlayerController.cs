using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private OOBChecker PlayerOOB;
    public float speedRotate;
    public float speedMove;
    public float hoverSpeed;
    private Rigidbody2D rb2D;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public AudioClip audioShoot;
    private AudioSource audioSource;
    private Transform bulletSpawn;
    private float fireRate = 0.01f;
    public float timeSinceShot = 0.0f;
    private int playerHP;
    
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
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        bulletSpawn = rb2D.transform;
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
        //V vsakem primeru se premikam malo nazaj cuz space
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

