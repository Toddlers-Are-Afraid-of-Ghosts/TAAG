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
    public float Delay = 10f;
    

    
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
            mc.transform.position += new Vector3(0, (float) -13, 0);

            player.transform.position += Vector3.up;
            player.transform.position += Vector3.right*2;
            player.transform.position += Vector3.down*3;
            player.transform.position += Vector3.left*2;
            isLerp = false;
        }
        else if (movementDirection == 2)
        {
            //top
            mc.transform.position += new Vector3(0, (float) 13, 0);
            
            player.transform.position += Vector3.down;
            player.transform.position += Vector3.right*2;
            player.transform.position += Vector3.up*3;
            player.transform.position += Vector3.left*2;
            isLerp = false;
        }
        else if (movementDirection == 3)
        {
            //left
            mc.transform.position += new Vector3((float) -27, 0, 0);

            player.transform.position += Vector3.right;
            player.transform.position += Vector3.up*2;
            player.transform.position += Vector3.left*3;
            player.transform.position += Vector3.down*2;
            isLerp = false;
        }
        else if (movementDirection == 4)
        {
            //right
            mc.transform.position += new Vector3((float) 27, 0, 0);
            player.transform.position += Vector3.left;
            player.transform.position += Vector3.up*2;
            player.transform.position += Vector3.right*3;
            player.transform.position += Vector3.down*2;
            isLerp = false;
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
