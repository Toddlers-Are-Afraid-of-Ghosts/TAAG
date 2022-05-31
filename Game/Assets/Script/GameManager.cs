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
    public CharacterDatabase CharacterDB;


    private int SelectedOption = 0;
    public List<GameObject> players = new List<GameObject>();

    void Start()
    {
        if (!PlayerPrefs.HasKey("SelectedOption"))
        {
            SelectedOption = 0;
        }
        else
        {
            Load();
        }
        
        Instantiate(players[SelectedOption % 2]);
        generator = this.GetComponent<GeneratorEnemi>();
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
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
        SceneManager.LoadScene("World");
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    //function copied from charactermanager
    private void Load()
    {
        SelectedOption = PlayerPrefs.GetInt("SelectedOption");
    }
}