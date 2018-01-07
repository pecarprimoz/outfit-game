using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text playerHPText;
    public Text waveText;
    public static GameController instance;
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject player;
    private int GCPlayerHP;
    private int GCWave;
    
    

    void Awake()
    {
        playerHPText = GameObject.Find("HPInt").GetComponent<Text>();
        waveText = GameObject.Find("WaveInt").GetComponent<Text>();
        GCWave = 1;
        GCPlayerHP = 3;

        playerHPText.text = player.GetComponent<PlayerController>().getPlayerHP().ToString();

        player.GetComponent<PlayerController>().setPlayerHP(GCPlayerHP);
        playerHPText.text = GCPlayerHP.ToString();
        waveText.text = GCWave.ToString();
        for (int i = 0; i < 4; i++)
        {
            Instantiate(asteroid);
            //Instantiate(asteroid2);
            //Instantiate(asteroid3);
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
            Instantiate(player, new Vector3(0, 0, -2), player.transform.rotation);
        }
    }
	

}
