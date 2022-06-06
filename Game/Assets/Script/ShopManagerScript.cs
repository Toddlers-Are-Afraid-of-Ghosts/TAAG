using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    //private Player playerStats;
    public int[,] ShopItems=new int[10,10];
    public Text CoinsTXT;
    

    public int coins;


    void Start()
    {
    
        CoinsTXT.text= "Coins:"+ coins.ToString();
        //ID's
        ShopItems[1,0]=1;
        ShopItems[1,1]=2;
        ShopItems[1,2]=3;
        ShopItems[1,3]=4;
        ShopItems[1,4]=5;
        ShopItems[1,5]=6;
        ShopItems[1,6]=7;
        ShopItems[1,7]=8;
        ShopItems[1,8]=9;



        //Prices
        ShopItems[2,0]=9;
        ShopItems[2,1]=9;
        ShopItems[2,2]=4;
        ShopItems[2,3]=4;
        ShopItems[2,4]=4;
        ShopItems[2,5]=3;
        ShopItems[2,6]=3;
        ShopItems[2,7]=4;
        ShopItems[2,8]=4;
    

        //Quantities
        ShopItems[3,0]=1;
        ShopItems[3,1]=1;
        ShopItems[3,2]=1;
        ShopItems[3,3]=1;
        ShopItems[3,4]=1;
        ShopItems[3,5]=1;
        ShopItems[3,6]=1;
        ShopItems[3,7]=1;
        ShopItems[3,8]=1;

    }

    public void Buy()
    {
        //playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject ButtonRef= GameObject.FindGameObjectWithTag("Menu").GetComponent<EventSystem>().currentSelectedGameObject;
        if (coins>=ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID] && ShopItems[3,ButtonRef.GetComponent<buttoninfo>().ItemID]>0)
        {
            coins -=ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID];
            /* switch (ShopItems[0,ButtonRef.GetComponent<buttoninfo>().ItemID])
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
            } */
            ShopItems[3,ButtonRef.GetComponent<buttoninfo>().ItemID] --;
            CoinsTXT.text= "Coins:"+ coins.ToString();
            ButtonRef.GetComponent<buttoninfo>().Quantity.text=ShopItems[3,ButtonRef.GetComponent<buttoninfo>().ItemID].ToString();
        }
    }

    void FixedUpdate()
    {
        //playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        CoinsTXT.text= "Coins:"+ coins.ToString();    
    }
    /* private Player playerStats;
    public Text CoinsTXT;
    private int coins;
    void Start()
    {
    }

    public void BuyIem1(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.speed+=15;
            playerStats.attack+=3;
            playerStats.shotSpeed+=600;               
            playerStats.cooldown-=(float)0.15;
            playerStats.attackRange+=3;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }

    public void BuyIem2(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.cooldown-=(float)0.1;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }

    public void BuyIem3(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.attackRange+=2;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }
    public void BuyIem4(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.shotSpeed+=400;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }
    public void BuyIem5(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.bonusHealth+=2;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }
    public void BuyIem6(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.health+=2;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }
    public void BuyIem7(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.speed+=10;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }
    public void BuyIem8(int cost)
    {
        if(playerStats.gold>=cost)
        {
            playerStats.gold-=cost;
            playerStats.attack+=2;
            coins=playerStats.gold;
            CoinsTXT.text= "Coins:"+ coins.ToString();
        }
    }

     void FixedUpdate()
    {
        playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        coins=playerStats.gold;
        CoinsTXT.text= "Coins:"+ coins.ToString();
    } */
}
