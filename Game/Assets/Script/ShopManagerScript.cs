using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    private Player playerStats;
    public int[,] ShopItems=new int[10,10];
    public Text CoinsTXT;
    GameObject ButtonRef;

    private int coins;


    void Start()
    {
        
        //ID's
        ShopItems[0,0]=1;
        ShopItems[0,1]=2;
        ShopItems[0,2]=3;
        ShopItems[0,3]=4;
        ShopItems[0,4]=5;
        ShopItems[0,5]=6;
        ShopItems[0,6]=7;
        ShopItems[0,7]=8;
        ShopItems[0,8]=9;



        //Prices
        ShopItems[1,0]=9;
        ShopItems[1,1]=9;
        ShopItems[1,2]=4;
        ShopItems[1,3]=4;
        ShopItems[1,4]=4;
        ShopItems[1,5]=3;
        ShopItems[1,6]=3;
        ShopItems[1,7]=4;
        ShopItems[1,8]=4;
    

        //Quantities
        ShopItems[2,0]=1;
        ShopItems[2,1]=1;
        ShopItems[2,2]=1;
        ShopItems[2,3]=1;
        ShopItems[2,4]=1;
        ShopItems[2,5]=1;
        ShopItems[2,6]=1;
        ShopItems[2,7]=1;
        ShopItems[2,8]=1;

    }

    public void Buy()
    {
        playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        coins=playerStats.gold;
        ButtonRef= GameObject.FindGameObjectWithTag("Menu").GetComponent<EventSystem>().currentSelectedGameObject;
        if (ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID]>0 && coins>=ShopItems[1,ButtonRef.GetComponent<buttoninfo>().ItemID])
        {
            coins -=ShopItems[1,ButtonRef.GetComponent<buttoninfo>().ItemID];
            switch (ShopItems[0,ButtonRef.GetComponent<buttoninfo>().ItemID])
            {
                case 1:
                {
                    playerStats.speed+=15;
                    playerStats.attack+=3;
                    playerStats.shotSpeed+=600;
                    playerStats.cooldown-=(float)0.15;
                    playerStats.attackRange+=3;
                    break;
                }
                case 2:
                {
                    playerStats.cooldown-=(float)0.1;
                    break;
                }
                case 3:
                {
                    playerStats.attackRange+=2;
                    break;
                }
                case 4:
                {
                    playerStats.shotSpeed+=400;;
                    break;
                }
                case 5:
                {
                    playerStats.bonusHealth+=2;
                    break;
                }
                case 6:
                {
                    playerStats.health+=2;
                    break;
                }
                case 7:
                {
                    playerStats.speed+=10;
                    break;
                }
                case 8:
                {
                    playerStats.attack+=2;
                    break;
                }
            }

            ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID] --;
            CoinsTXT.text= "Coins:"+ coins.ToString();
            ButtonRef.GetComponent<buttoninfo>().Quantity.text=ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID].ToString();
        }
    }

    void FixedUpdate()
    {
        playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        coins=playerStats.gold;

        CoinsTXT.text= "Coins:"+ coins.ToString();
        
    
    }

}
