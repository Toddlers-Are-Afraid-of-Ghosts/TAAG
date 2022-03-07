using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public GameObject settingsWindows;
    public void PlayGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
    public void SettingBoutton()
    {
        settingsWindows.SetActive(true);
    }
    public void CloseSettingsWindows()
     {
         settingsWindows.SetActive(false);
     }
    public void QuitGame()
    {
        Application.Quit();
    }
}
