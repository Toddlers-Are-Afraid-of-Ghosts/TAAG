using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemysoutenance : MonoBehaviour
{
    public GameObject Patrol,Turner,Chase,Spawner;
    Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("S"))
       {
           Vector2 spawnPos=new Vector2(-8,3);
           Instantiate(Patrol, spawnPos, Quaternion.identity);
       }
    }
}
