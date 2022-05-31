using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomsProperties : MonoBehaviour {
    private bool top;
    private bool bottom;
    private bool left;
    private bool right;
    private int x;
    private int y;
    private GameObject room;
    private bool isSpecial;
    private bool isPath;
    private bool isBoss;
    private bool isShop;
    private bool isItems;
    private bool isDeadEnd;
    private bool isStart;
    private bool hasBeenEntered;
    private bool isPlayerIn;

    public bool Top {
        get => top;
        set => top = value;
    }

    public bool Bottom {
        get => bottom;
        set => bottom = value;
    }

    public bool Left {
        get => left;
        set => left = value;
    }

    public bool Right {
        get => right;
        set => right = value;
    }


    public GameObject Room;
    public int X;
    public int Y;

    public bool IsSpecial;

    public bool IsPath;

    public bool IsBoss;

    public bool IsShop;

    public bool IsItems;

    public bool IsDeadEnd;

    public bool IsStart;
    public bool HasBeenEntered;
    public bool IsPLayerIn;

    public List<GameObject> spawnPoin = new();


    public RoomsProperties(bool top, bool bottom, bool left, bool right) {
        Top = top;
        Bottom = bottom;
        Right = right;
        Left = left;
        X = 0;
        Y = 0;
        Room = null;
        IsSpecial = false;
        IsPath = false;
        IsBoss = false;
        IsShop = false;
        IsItems = false;
        IsDeadEnd = false;
        IsStart = false;
        HasBeenEntered = false;
        IsPLayerIn = false;
        spawnPoin = new List<GameObject>();
    }
}