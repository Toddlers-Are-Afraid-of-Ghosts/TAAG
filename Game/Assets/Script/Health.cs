using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Player player;
    public Image haeltBbarImage;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        haeltBbarImage.fillAmount = player.Health / player.maxHealth;
    }
}