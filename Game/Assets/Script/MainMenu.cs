using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public GameObject settingsWindows;
    public GameObject playPanel;
    bool flag=true;
    public void PlayGame()
    {
        SceneManager.LoadScene("World");
    }
    public void PanelPlay()
    {

        playPanel.SetActive(flag);
        flag=!flag;
    }
    public void SettingBoutton()
    {
        settingsWindows.SetActive(flag);
        flag=!flag;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
