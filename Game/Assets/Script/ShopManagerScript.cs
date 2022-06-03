using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    private Player player;
    public int[,] ShopItems=new int[10,10];
    public Text CoinsTXT;

    private int coins;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        coins=player.gold;
        CoinsTXT.text= "Coins:"+ coins.ToString();

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
        ShopItems[1,0]=10;
        ShopItems[1,1]=20;
        ShopItems[1,2]=30;
        ShopItems[1,3]=40;
        ShopItems[1,4]=50;
        ShopItems[1,5]=60;
        ShopItems[1,6]=70;
        ShopItems[1,7]=80;
        ShopItems[1,8]=90;
    

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
        GameObject ButtonRef= GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if ( ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID]>0 && coins>=ShopItems[1,ButtonRef.GetComponent<buttoninfo>().ItemID])
        {
            coins -=ShopItems[1,ButtonRef.GetComponent<buttoninfo>().ItemID];
            switch (ShopItems[0,ButtonRef.GetComponent<buttoninfo>().ItemID])
            {
                case 1:
                {
                    player.speed+=15;
                    player.attack+=3;
                    player.shotSpeed+=600;
                    player.cooldown-=(float)0.15;
                    player.attackRange+=3;
                    break;
                }
                case 2:
                {
                    player.cooldown-=(float)0.1;
                    break;
                }
                case 3:
                {
                    player.attackRange+=2;
                    break;
                }
                case 4:
                {
                    player.shotSpeed+=400;;
                    break;
                }
                case 5:
                {
                    player.bonusHealth+=2;
                    break;
                }
                case 6:
                {
                    player.health+=2;
                    break;
                }
                case 7:
                {
                    player.speed+=10;
                    break;
                }
                case 8:
                {
                    player.attack+=2;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        coins=player.gold;  
    }
}
