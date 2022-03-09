using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    public List<GameObject> rooms;
    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if (waitTime <= 0 && (rooms.Count < 8 || rooms.Count > 32))
        {
            SceneManager.LoadScene("World");
        }
            
        // fonction qui apres un certain defini la derniere salles de la liste rooms comme etant la salle de boss
        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
