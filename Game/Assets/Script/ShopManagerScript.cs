using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] ShopItems=new int[10,10];
    public float coins;
    public Text CoinsTXT;


    void Start()
    {
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
            ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID] --;
            CoinsTXT.text= "Coins:"+ coins.ToString();
            ButtonRef.GetComponent<buttoninfo>().Quantity.text=ShopItems[2,ButtonRef.GetComponent<buttoninfo>().ItemID].ToString();
        }
    }
}
