using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudNetwork : MonoBehaviour
{
    // Start is called before the first frame update
    public NetworkManager networkManager;
    public GameManagerMultiplayer GameManagerMultiplayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Host()
    {
        networkManager.StartHost();
        this.gameObject.SetActive(false);
    }
    public void Client()
    {
        networkManager.StartClient();
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
