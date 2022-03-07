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
    private float Speed = 3f;

    
    private void Start()
    {
        mc = GameObject.FindGameObjectWithTag("MainCamera").transform;
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
            var position = mc.position;
            dest = new Vector3(position.x, - 13, position.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
        }
        else if (movementDirection == 2)
        {
            //top
            var position = mc.position;
            dest = new Vector3(position.x, 13, position.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
        }
        else if (movementDirection == 3)
        {
            //left
            var position = mc.position;
            dest = new Vector3(- 27, position.y , position.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
        }
        else if (movementDirection == 4)
        {
            //right
            var position = mc.position;
            dest = new Vector3(27, position.y, position.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLerp = true;
        }
    }
}
