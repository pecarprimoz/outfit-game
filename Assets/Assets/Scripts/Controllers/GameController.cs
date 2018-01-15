using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //GC instance to use in diffrent controllers
    public static GameController instance;

    //Variables for UI/scoring elements
    public GameObject pauseMenu;
    private Text playerHPText;
    private Text waveText;
    private Text scoreText;
    private Text lostText;
    private int GCPlayerHP;
    private int GCWave;
    private int GCScore;
    private int numberOfAsteroids;
    private GameObject inputField;
    private GameObject buttonEnter;
    private bool isMenuOpen;

    //Data controller to save score 
    private DataController dataController;

    //Prefabs
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject player;
    public GameObject enemy;
    public GameObject boss;

    //Variables for pickups
    public GameObject PickupHP;
    public GameObject PickupFireRate;
    private GameObject temporaryPickupHP;
    private GameObject temporaryPickupDPS;
    private int pickupExists;

    //Variables for audio
    private AudioSource audioSource;
    public AudioClip soundExplode;
    public AudioClip soundPickup;
    public AudioClip soundAsteroidExplode;
    public AudioClip soundPlayMusic;

    //Increments HP when player picks up PickupHP
    public void IncrementHP()
    {
        player.GetComponent<PlayerController>()
            .setPlayerHP(player.GetComponent<PlayerController>()
            .getPlayerHP() + 1);
        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();
    }

    //Reduces fire rate when player picks up PickupDPS
    public void ReduceFireRate()
    {
        player.GetComponent<PlayerController>()
            .reduceFireRate();
    }

    //Increment score when an asteroid explodes/enemy dies
    public void IncrementScore()
    {
        GCScore += 10;
        scoreText.text = GCScore.ToString();
    }

    //Get current number of asteroids on game screen
    public int getNumberOfAsteroids()
    {
        return numberOfAsteroids;
    }

    //Increment numberOfAsteroids by n
    public void setNumberOfAsteroids(int n)
    {
        numberOfAsteroids += n;
    }

    //Awake function, used to set needed variables
    void Awake()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetString("sound").Equals("off"))
        {
            AudioListener.volume = 0.0f;
        }
        else
        {
            AudioListener.volume = 1.0f;
        }
        //Make reference on pickups, need because we destroy the pickup
        //when the player collision occures
        temporaryPickupHP = PickupHP;
        temporaryPickupDPS = PickupFireRate;

        //Get references of UI elements
        playerHPText = GameObject.Find("HPInt").GetComponent<Text>();
        waveText = GameObject.Find("WaveInt").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreInt").GetComponent<Text>();
        lostText = GameObject.Find("Lost").GetComponent<Text>();
        inputField = GameObject.Find("InputField");
        buttonEnter = GameObject.Find("EnterHS");
        isMenuOpen = false;

        //Turn off input field and button, used to get name of player
        //and save his score localy
        buttonEnter.SetActive(false);
        buttonEnter.GetComponent<Button>().onClick.AddListener(HandleData);
        inputField.SetActive(false);

        dataController = GetComponent<DataController>();

        //We start on wave 1, playerHP is 3, score is 0,
        //no pickup exists on start
        GCWave = 1;
        GCPlayerHP = 3;
        GCScore = 0;
        pickupExists = -1;

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundPlayMusic, 1.0f);
        //Set the initial values of UI elements, based on GCVars
        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();
        scoreText.text = GCScore.ToString();
        player.GetComponent<PlayerController>().setPlayerHP(GCPlayerHP);
        playerHPText.text = GCPlayerHP.ToString();
        waveText.text = GCWave.ToString();

        //First wave has only 1 rock
        MakeANewRock();

        //Instance reference, needed to get current GameController
        //between multiple Controllers
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    //HandleData is called when Enter score button is clicked
    void HandleData()
    {
        Destroy(buttonEnter);
        dataController.WriteScores(inputField.GetComponent<InputField>().text, GCScore);
    }

    //Update function, check for planets, if game has ended,
    //if pickup needs to be created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOpen)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isMenuOpen = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                isMenuOpen = false;
            }
        }

        if (GameObject.FindGameObjectsWithTag("Planet").Length == 0)
        {
            CreateNewWave();
        }
        if (player.GetComponent<PlayerController>().getPlayerHP() == 0)
        {
            FinishGame();
        }
        if (GCWave % 2 == 0 && pickupExists == -1)
        {
            GeneratePickup();
        }
    }

    //Create new wave when all rocks exploded, generate number of rocks
    //based on Wave number, generate a new enemy if wave%3==0
    void CreateNewWave()
    {
        pickupExists = -1;
        GCWave++;
        waveText.text = GCWave.ToString();
        if (GCWave % 5 == 0 && GCWave % 10 !=0)
        {
            for(int i=0; i< GCWave / 2 ; i++)
            {
                MakeANewRock();
                GenerateNewEnemy();
            }
        }
        else if (GCWave % 10 == 0)
        {
            MakeANewRock();
            GenerateNewBoss();
        }
        else if(GCWave % 5 !=0){ 
            for (int i = 0; i < GCWave; i++)
            {
                MakeANewRock();
            }
            if (GCWave % 3 == 0)
            {
                GenerateNewEnemy();
            }
        }
    }

    //Generate enemy, set starting position randomly
    void GenerateNewEnemy()
    {
        Vector3 randomStart = enemy.transform.position;
        int r = Random.Range(1, 3);
        if (r == 1)
            randomStart.x = randomStart.x * -1.0f;
        Instantiate(enemy, randomStart, enemy.transform.rotation);
    }
    void GenerateNewBoss()
    {
        Vector3 randomStart = boss.transform.position;
        int r = Random.Range(1, 3);
        if (r == 1)
            randomStart.x = randomStart.x * -1.0f;
        Instantiate(boss, randomStart, boss.transform.rotation);
    }

    //Finish game when playerHP == 0, display UI info
    void FinishGame()
    {
        lostText.text = "YOU LOST";
        inputField.SetActive(true);
        if (buttonEnter != null)
            buttonEnter.SetActive(true);
    }

    //If player gets hit, handle his respawn
    public void HandlePlayerRespawn()
    {
        player.GetComponent<PlayerController>().reduceHP();
        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();
        Invoke("Respawn", 2.0f);
    }

    //As long as the player has HP, respawn him, apply a "shield"
    //change the color to indicate the shield, update enemy reference
    //on the new player
    void Respawn()
    {
        if (player.GetComponent<PlayerController>().getPlayerHP() > 0)
        {
            GameObject pc = Instantiate(player, new Vector3(0, 0, -2), player.transform.rotation);
            pc.GetComponent<BoxCollider2D>().enabled = false;
            pc.GetComponent<SpriteRenderer>().color = Color.magenta;
            if (GameObject.FindGameObjectWithTag("Enemy") != null) {
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                for(int i=0; i < enemys.Length; i++)
                {
                    if(enemys[i].GetComponent<AIController>()  != null)
                        enemys[i].GetComponent<AIController>().UpdatePlayerObject();
                    else
                        enemys[i].GetComponent<BOSSController>().UpdatePlayerObject();
                }
            }
        }
    }

    //Choose a random rock
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

    //Generates pickup based on gameScreen size
    void GeneratePickup()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize / 2;
        float width = height * cam.aspect / 2;

        float xPos = Random.Range(0.33f, width - 0.33f);
        float yPos = Random.Range(0.33f, height - 0.33f);

        int typeOfPickup = Random.Range(1, 3);
        if (typeOfPickup == 1) {
            PickupHP = Instantiate(temporaryPickupHP, new Vector3(xPos, yPos, -2), temporaryPickupHP.transform.rotation);
            pickupExists = 1;
            Invoke("DestroyPickup", 5.0f);
        }
        else if (typeOfPickup == 2)
        {
            PickupFireRate = Instantiate(temporaryPickupDPS, new Vector3(xPos, yPos, -2), temporaryPickupDPS.transform.rotation);
            pickupExists = 2;
            Invoke("DestroyPickup", 5.0f);
        }
    }

    //Destroy the pickup if player isn't fast enough
    void DestroyPickup() {
        if (pickupExists == 1)
        {
            Destroy(PickupHP);
        }
        else if (pickupExists == 2)
        {
            Destroy(PickupFireRate);
        }
    }

    //Functions for playing sounds
    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(soundExplode, 1.0f);
    }
    public void PlayPickupSound()
    {
        audioSource.PlayOneShot(soundPickup, 1.0f);
    }
    public void PlayAsteroidExplosionSound()
    {
        audioSource.PlayOneShot(soundAsteroidExplode, 1.0f);
    }
}
