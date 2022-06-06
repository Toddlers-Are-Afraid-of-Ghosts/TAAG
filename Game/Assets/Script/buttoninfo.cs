using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttoninfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceTag;
    public Text Quantity;
    public GameObject ShopManager;

    void Update()
    {
        PriceTag.text="Price: $"+ShopManager.GetComponent<ShopManagerScript>().ShopItems[2,ItemID].ToString();
        Quantity.text=ShopManager.GetComponent<ShopManagerScript>().ShopItems[3,ItemID].ToString();
    }
}
