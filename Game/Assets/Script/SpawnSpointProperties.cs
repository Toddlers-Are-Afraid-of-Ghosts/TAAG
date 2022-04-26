using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpointProperties : MonoBehaviour
{
    protected Tuple<int,int> pos;

    public Tuple<int, int> Pos => pos;
    public void Setup(int x, int y)
    {
        pos = new Tuple<int, int>(x, y);
    }
}
