using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private Transform player;
    public Transform camera;
    public Player playerStats;
    public levelloader loader;

    public static bool isIn;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag is "Player")
        {
            loader.Loadlevel("World");
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        if (isIn)
        {
            PlayerPrefs.SetFloat("health", playerStats.health);
            loader.Loadlevel(2);
            PlayerPrefs.SetFloat("attack", playerStats.attack);
            PlayerPrefs.SetFloat("speed", playerStats.speed);
            PlayerPrefs.SetFloat("shotspeed", playerStats.shotSpeed);
            PlayerPrefs.SetFloat("bonushealth", playerStats.bonusHealth);
            PlayerPrefs.SetFloat("cooldown", playerStats.cooldown);
            PlayerPrefs.SetFloat("attackrange", playerStats.attackRange);
        }
    }

    
}