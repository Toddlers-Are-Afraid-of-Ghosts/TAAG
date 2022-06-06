using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public GameObject settingsWindows;
    public GameObject musicSetting;
    public GameObject playPanel;
    public GameObject inputSetting;
    bool flag = true;
    bool music= true;
    bool input = true;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

 

    public void PanelPlay()
    {
        playPanel.SetActive(flag);
        flag = !flag;
    }

    public void SettingBoutton()
    {
        settingsWindows.SetActive(flag);
        flag = !flag;
    }

    public void InputSetting()
    {
        inputSetting.SetActive(input);
        input = !input;
    }

    public void MusicSetting()
    {
        musicSetting.SetActive(music);
        music = !music;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}