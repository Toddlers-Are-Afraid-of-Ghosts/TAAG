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
    
    public bool Top { get; }
    public bool Bottom { get; }
    public bool Left { get; }
    public bool Right { get; }
    public int X { get; }
    public int Y { get; }

    public RoomsProperties(bool top, bool bottom, bool right, bool left, int x, int y) {
        Top = top;
        Bottom = bottom;
        Right = right;
        Left = left;
        X = x;
        Y = y;
    }

    public bool isClosed() {
        if (this.Top == false && this.Bottom == false && this.Left == false && this.Right == false) {
            return true;
        }

        return false;
    }
}
