using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    private Text playerHPText;
    private Text waveText;
    private Text scoreText;
    private Text lostText;
    private int GCPlayerHP;
    private int GCWave;
    private int GCScore;
    private int numberOfAsteroids;
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject player;
    public GameObject PickupHP;
    private GameObject temporaryPickup;
    private int pickupExists;

    public void IncrementHP()
    {
        //To bi bilo dobro prepisati v increment/decrement
        player.GetComponent<PlayerController>()
            .setPlayerHP(player.GetComponent<PlayerController>()
            .getPlayerHP() + 1);
        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();
    }
    public void IncrementScore()
    {
        GCScore += 10;
        scoreText.text = GCScore.ToString();
    }
    public int getNumberOfAsteroids()
    {
        return numberOfAsteroids;
    }
    public void setNumberOfAsteroids(int n)
    {
        numberOfAsteroids += n;
    }


    void Awake()
    {
        temporaryPickup = PickupHP;
        playerHPText = GameObject.Find("HPInt").GetComponent<Text>();
        waveText = GameObject.Find("WaveInt").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreInt").GetComponent<Text>();
        lostText = GameObject.Find("Lost").GetComponent<Text>();
        GCWave = 1;
        GCPlayerHP = 3;
        GCScore = 0;
        pickupExists = -1;

        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();
        scoreText.text = GCScore.ToString();
        player.GetComponent<PlayerController>().setPlayerHP(GCPlayerHP);
        playerHPText.text = GCPlayerHP.ToString();
        waveText.text = GCWave.ToString();
        for (int i = 0; i < GCWave; i++)
        {
            MakeANewRock();
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
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Planet").Length == 0)
        {
            CreateNewWave();
        }
        if (player.GetComponent<PlayerController>().getPlayerHP() == 0)
        {
            FinishGame();
        }
        if (GCWave % 2 == 0 && pickupExists==-1)
        {
            GeneratePickup();
        }
    }
    void CreateNewWave()
    {
        pickupExists = -1;
        GCWave++;
        for (int i = 0; i < GCWave; i++)
        {
            MakeANewRock();
        }
        waveText.text = GCWave.ToString();
    }
    void FinishGame()
    {
        lostText.text = "YOU LOST";
    }
    public void HandlePlayerRespawn()
    {
        player.GetComponent<PlayerController>().reduceHP();
        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();
        Invoke("Respawn", 2.0f);
    }
    void Respawn()
    {
        if (player.GetComponent<PlayerController>().getPlayerHP() > 0)
        {
            GameObject pc = Instantiate(player, new Vector3(0, 0, -2), player.transform.rotation);
            pc.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    
    void MakeANewRock()
    {
        int r = Random.Range(1, 4);
        switch (r)
        {
            case 1:
                Instantiate(asteroid);
                numberOfAsteroids++;
                break;
            case 2:
                Instantiate(asteroid2);
                numberOfAsteroids++;
                break;
            case 3:
                Instantiate(asteroid3);
                numberOfAsteroids++;
                break;
            default:
                Instantiate(asteroid);
                numberOfAsteroids++;
                break;
        }
    }
    void GeneratePickup()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize / 2;
        float width = height * cam.aspect / 2;
        
        float xPos = Random.Range(0.33f, width-0.33f);
        float yPos = Random.Range(0.33f, height-0.33f);

        int typeOfPickup = Random.Range(1, 1);

        if (typeOfPickup == 1) {
            PickupHP = Instantiate(temporaryPickup, new Vector3(xPos, yPos, -2), temporaryPickup.transform.rotation);
            pickupExists = 1;
        }
        Invoke("DestroyPickup", 5.0f);
    }
    void DestroyPickup() {
        if (pickupExists == 1)
        {
            Destroy(PickupHP);
        }
    }
}
