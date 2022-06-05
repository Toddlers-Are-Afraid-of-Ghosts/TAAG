using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager IM;

    public KeyCode moveUp {get;set;}
    public KeyCode moveLeft {get;set;}
    public KeyCode moveRight {get;set;}
    public KeyCode moveDown {get;set;}
    public KeyCode fireUp {get;set;}
    public KeyCode fireLeft {get;set;}
    public KeyCode fireRight {get;set;}
    public KeyCode fireDown {get;set;}
    public KeyCode openShop {get;set;}
    public KeyCode pause {get;set;}

    void Awake()
    {
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }
        moveUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveUpKey","Z"));
        moveLeft = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveLeftKey","Q"));
        moveRight = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveRightKey","D"));
        moveDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("moveDownKey","S"));
        fireUp = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireUpKey","UpArrow"));
        fireLeft = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireLeftKey","LeftArrow"));
        fireRight = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireRightKey","RightArrow"));
        fireDown = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireDownKey","DownArrow"));
        openShop = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("openShop","Space"));
        pause = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pause","Escape"));

    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
