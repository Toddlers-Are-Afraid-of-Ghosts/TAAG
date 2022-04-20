using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsProperties : MonoBehaviour {
    private bool _top;
    private bool _bottom;
    private bool _left;
    private bool _right;
    
    public bool Top { get; }
    public bool Bottom { get; }
    public bool Left { get; }
    public bool Right { get; }

    public RoomsProperties(bool top, bool bottom, bool right, bool left) {
        Top = top;
        Bottom = bottom;
        Right = right;
        Left = left;
    }

    public bool isClosed() {
        if (this.Top == false && this.Bottom == false && this.Left == false && this.Right == false) {
            return true;
        }

        return false;
    }
}
