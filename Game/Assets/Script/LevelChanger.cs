using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour {
    private Transform player;
    public Transform camera;
    public Player playerStats;

    public static bool isIn;
    // Start is called before the first frame update
    void Start() {
        isIn = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        if (isIn) {
            camera.transform.position = new Vector3(0,0, -10);
            player.transform.position = Vector3.zero;
            PlayerPrefs.SetFloat("health", playerStats.health);
            PlayerPrefs.SetFloat("attack", playerStats.attack);
            PlayerPrefs.SetFloat("speed", playerStats.speed);
            PlayerPrefs.SetFloat("shotspeed", playerStats.shotSpeed);
            PlayerPrefs.SetFloat("bonushealth", playerStats.bonusHealth);
            PlayerPrefs.SetFloat("cooldown", playerStats.cooldown);
            PlayerPrefs.SetFloat("attackrange", playerStats.attackRange);
            SceneManager.LoadScene("World");
            SceneManager.UnloadSceneAsync("World");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) isIn = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) isIn = false;
    }
}
