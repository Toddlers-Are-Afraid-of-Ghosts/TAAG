using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Vdoor : MonoBehaviour
{
    private Vector3 dest;
    private Vector3 playerTP;
    public int movementDirection;
    public Transform mc;
    public Transform player;
    private bool isLerp = false;
    private float Speed = 3f;
    public float Delay = 0f;
    

    
    private void Start()
    {
        mc = GameObject.FindGameObjectWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            for (int i = 0; i < 13; i++)
            {
                mc.transform.position += Vector3.down;
            }
            player.transform.position += Vector3.down;
        }
        else if (movementDirection == 2)
        {
            //top
            for (int i = 0; i < 13; i++)
            {
                mc.transform.position += Vector3.up;
            }
            player.transform.position += Vector3.up;
        }
        else if (movementDirection == 3)
        {
            //left
            for (int i = 0; i < 13; i++)
            {
                mc.transform.position += Vector3.left;
            }
            player.transform.position += Vector3.left;
            
        }
        else if (movementDirection == 4)
        {
            //right
            for (int i = 0; i < 13; i++)
            {
                mc.transform.position += Vector3.right;
            }
            player.transform.position += Vector3.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLerp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLerp = false;
        }
    }
}
