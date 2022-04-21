using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsProperties : MonoBehaviour {

    private bool _top;
    private bool _bottom;
    private bool _left;
    private bool _right;
    private int _x;
    private int _y;
    private GameObject _room;
    
    public bool Top {
        get => this._top;
        set {this._top = value;}
    }

    public bool Bottom {
        get => this._bottom;
        set { this._bottom = value; }
    }
    public bool Left {
        get => this._left;
        set { this._left = value; }
    }
    public bool Right {
        get => this._right;
        set { this._right = value; }
    }
    
    public GameObject Room { get; }
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

    public bool IsClosed() {
        if (this.Top == false && this.Bottom == false && this.Left == false && this.Right == false) {
            return true;
        }

        return false;
    }
}
