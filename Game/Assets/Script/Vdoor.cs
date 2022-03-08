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
            var position = mc.position;
            var tp = player.position;
            dest = new Vector3(position.x, - 13, position.z);
           // playerTP = new Vector3(tp.x, - 1, tp.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
            //player.transform.position = Vector3.Lerp(tp, playerTP, 100000000);
            Delay = 10f;
            isLerp = false;
        }
        else if (movementDirection == 2)
        {
            //top
            var position = mc.position;
            var tp = player.position;
            dest = new Vector3(position.x, 13, position.z);
           // player.transform.position = new Vector3(tp.x, 1, tp.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
            //player.transform.position = Vector3.Lerp(tp, playerTP, 100000000);
            Delay = 10f;
            isLerp = false;
        }
        else if (movementDirection == 3)
        {
            //left
            var position = mc.position;
            var tp = player.position;
            dest = new Vector3(- 27, position.y , position.z);
           // player.transform.position = new Vector3(-1, tp.y, tp.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
            //player.transform.position = Vector3.Lerp(tp, playerTP, 100000000);
            Delay = 10f;
            isLerp = false;
        }
        else if (movementDirection == 4)
        {
            //right
            var position = mc.position;
            var tp = player.position;
            dest = new Vector3(27, position.y, position.z);
            //player.transform.position = new Vector3(1, tp.y, tp.z);
            mc.transform.position = Vector3.Lerp(position, dest, Time.deltaTime * Speed);
            //player.transform.position = Vector3.Lerp(tp, playerTP, 100000000);
            Delay = 10f;
            isLerp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isLerp = true;
        }
        else
        {
            isLerp = false;
        }
    }
}
