using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vdoor : MonoBehaviour
{
    private Vector3 dest;
    public int movementDirection;
    public Transform mc;
    private bool isLerp = false;
    private float Speed = 1f;

    
    private void Start()
    {
        mc = transform;
    }

    private void Update()
    {
        if (isLerp)
        {
            PositionChanging();
        }
    }

    void PositionChanging()
    {
        if (movementDirection == 1)
        {
            //bottom
            dest = new Vector3(mc.position.x, mc.position.y - 13, mc.position.z);
            mc.transform.position = Vector3.Lerp(mc.position, dest, Time.deltaTime * Speed);
        }
        else if (movementDirection == 2)
        {
            //top
            dest = new Vector3(mc.position.x, mc.position.y + 13, mc.position.z);
            mc.transform.position = Vector3.Lerp(mc.position, dest, Time.deltaTime * Speed);
        }
        else if (movementDirection == 3)
        {
            //left
            dest = new Vector3(mc.position.x - 27, mc.position.y , mc.position.z);
            mc.transform.position = Vector3.Lerp(mc.position, dest, Time.deltaTime * Speed);
        }
        else if (movementDirection == 4)
        {
            //right
            dest = new Vector3(mc.position.x + 27, mc.position.y, mc.position.z);
            mc.transform.position = Vector3.Lerp(mc.position, dest, Time.deltaTime * Speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isLerp = true;
        }
    }
}
