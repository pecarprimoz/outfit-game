using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour {


    public SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
void OnMouseEnter()
    {
        rend.material.color = Color.red;
    }
    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }
    void OnMouseDown()
    {
        SceneManager.LoadScene(2);
    }
}
