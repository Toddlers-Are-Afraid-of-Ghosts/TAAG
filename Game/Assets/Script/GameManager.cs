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

    void Start()
    {
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayer <= 0)
        {
            var componentGenerator = this.GetComponent<GeneratorEnemi>();
            componentGenerator.CleanEnemy();
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public void TryAgain()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("World");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}