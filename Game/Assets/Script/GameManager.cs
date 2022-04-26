using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int numberOfPlayer;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    private GeneratorEnemi generator;

    void Start()
    {
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        generator = this.GetComponent<GeneratorEnemi>();
        
    }

    // Update is called once per frame
    void Update()
    {
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayer <= 0)
        {
            generator.CleanEnemy();
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (generator.win)
        {
            generator.CleanEnemy();
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }

    public void TryAgain()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}