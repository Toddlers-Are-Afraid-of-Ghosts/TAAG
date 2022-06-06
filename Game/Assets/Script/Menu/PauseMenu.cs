using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public  bool isPaused = false;
    public  bool isOptions = false;
    public GameObject settingsMenu,pauseMenu,vie;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(InputManager.IM.pause)) return;
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

        pauseMenu.SetActive(true);
        vie.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Resume()
    {
        isPaused = false;
        vie.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("Menu");

    }
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
         
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
       
    }
}
