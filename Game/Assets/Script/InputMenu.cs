using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
    Transform inputPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingKey;

    void Start()
    {
        inputPanel = this.transform.Find("InputSettings");
        inputPanel.gameObject.SetActive(false);
        waitingKey = false;
        for (int i = 0; i < 18; i++)
        {
            if (inputPanel.GetChild(i).name=="MoveUpKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.moveUp.ToString();
            }
            else if (inputPanel.GetChild(i).name=="MoveLeftKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.moveLeft.ToString();
            }
            else if (inputPanel.GetChild(i).name=="MoveRightKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.moveRight.ToString();
            }
            else if (inputPanel.GetChild(i).name=="MoveDownKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.moveDown.ToString();
            }
            else if (inputPanel.GetChild(i).name=="FireUpKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.fireUp.ToString();
            }
            else if (inputPanel.GetChild(i).name=="FireLeftKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.fireLeft.ToString();
            }
            else if (inputPanel.GetChild(i).name=="FireRightKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.fireRight.ToString();
            }
            else if (inputPanel.GetChild(i).name=="FireDownKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.fireDown.ToString();
            }
            else if (inputPanel.GetChild(i).name=="openShopKey")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.openShop.ToString();
            }
        }
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingKey)
        {
            newKey = keyEvent.keyCode;
            waitingKey=false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        Debug.Log("1 : SendText");
        buttonText = text;
    }

    IEnumerator WaitKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingKey = true;

        yield return WaitKey();

        switch (keyName)
        {
            case "moveUp":
                InputManager.IM.moveUp = newKey;
                buttonText.text = InputManager.IM.moveUp.ToString();
                PlayerPrefs.SetString("moveUpKey",InputManager.IM.moveUp.ToString());
                break;
            case "moveLeft":
                InputManager.IM.moveLeft = newKey;
                buttonText.text = InputManager.IM.moveLeft.ToString();
                PlayerPrefs.SetString("moveLeftKey",InputManager.IM.moveLeft.ToString());
                break;
            case "moveRight":
                InputManager.IM.moveRight = newKey;
                buttonText.text = InputManager.IM.moveRight.ToString();
                PlayerPrefs.SetString("moveRightKey",InputManager.IM.moveRight.ToString());
                break;
            case "moveDown":
                InputManager.IM.moveDown = newKey;
                buttonText.text = InputManager.IM.moveDown.ToString();
                PlayerPrefs.SetString("moveDownKey",InputManager.IM.moveDown.ToString());
                break;
            case "fireUp":
                InputManager.IM.fireUp = newKey;
                buttonText.text = InputManager.IM.fireUp.ToString();
                PlayerPrefs.SetString("fireUpKey",InputManager.IM.fireUp.ToString());
                break;
            case "fireLeft":
                InputManager.IM.fireLeft = newKey;
                buttonText.text = InputManager.IM.fireLeft.ToString();
                PlayerPrefs.SetString("fireLeftKey",InputManager.IM.fireLeft.ToString());
                break;
            case "fireRight":
                InputManager.IM.fireRight = newKey;
                buttonText.text = InputManager.IM.fireRight.ToString();
                PlayerPrefs.SetString("fireRightKey",InputManager.IM.fireRight.ToString());
                break;
            case "fireDown":
                InputManager.IM.fireDown = newKey;
                buttonText.text = InputManager.IM.fireDown.ToString();
                PlayerPrefs.SetString("fireDownKey",InputManager.IM.fireDown.ToString());
                break;
            case "openShop":
                InputManager.IM.openShop = newKey;
                buttonText.text = InputManager.IM.openShop.ToString();
                PlayerPrefs.SetString("openShopKey",InputManager.IM.openShop.ToString());
                break;
        }
        yield return null;

    }
}
