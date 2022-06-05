using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using UnityEngine.UI;

public class shop_owner : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject shopaccess;
    public GameObject player;
    private bool inShop;

    void Start()
    {
        shopaccess=GameObject.FindWithTag("Shop");
        player = GameObject.FindWithTag("Player");
        shopaccess.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        inShop = GeneratorEnemi.inShop;
        //ouvre scene shop lorsque spacebar est pressée et joueur est dans zone de collision
        if (!inShop) return;
        if (Input.GetKeyDown(InputManager.IM.openShop))
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
    }


    void Paused() //active le menu pause et arrete le temps.
    {
        shopaccess.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
        shopaccess.SetActive(false);
        Time.timeScale = 1;
    }
}