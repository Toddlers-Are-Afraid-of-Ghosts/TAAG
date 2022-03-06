using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawn : MonoBehaviour
{
    public int openingDirection;

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;
    
    void Start()
    {
        Destroy	(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("ROOMS").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    
    void Spawn()
    {
        if (!spawned)
        {
            if (openingDirection == 1)
            {
                //bottom
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, quaternion.identity);
            }
            if (openingDirection == 2)
            {
                //top
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, quaternion.identity);
            }
            if (openingDirection == 3)
            {
                //left
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, quaternion.identity);
            }
            if (openingDirection == 4)
            {
                //right
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, quaternion.identity);
            }

            spawned = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawnpoint"))
        {
            if (other.GetComponent<RoomSpawn>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawned = true;
        }
    }
}
