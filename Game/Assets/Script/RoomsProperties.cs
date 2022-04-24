using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsProperties : MonoBehaviour {

    private bool top;
    private bool bottom;
    private bool left;
    private bool right;
    private int x;
    private int y;
    private GameObject room;
    
    public bool Top {
        get => this.top;
        set {this.top = value;}
    }

    public bool Bottom {
        get => this.bottom;
        set { this.bottom = value; }
    }
    public bool Left {
        get => this.left;
        set { this.left = value; }
    }
    public bool Right {
        get => this.right;
        set { this.right = value; }
    }
    
    public GameObject Room { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public RoomsProperties(bool top, bool bottom, bool left, bool right, int x, int y, GameObject room) {
        Top = top;
        Bottom = bottom;
        Right = right;
        Left = left;
        X = x;
        Y = y;
        Room = room;
    }

    public static bool IsClosed(RoomsProperties room) {
        if (room.Top == false && room.Bottom == false && room.Left == false && room.Right == false) {
            return true;
        }

        return false;
    }
}
