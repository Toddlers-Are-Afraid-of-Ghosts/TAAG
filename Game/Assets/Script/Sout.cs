using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sout : MonoBehaviour
{
    public GameObject patrol, turner, chase, spawner;
    List<GameObject> enemi;
    Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        enemi = new List<GameObject>();
    }

    // Update is called once per frame
    
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            spawnPos = new Vector2(-8, 3);
            enemi.Add(Instantiate(patrol, spawnPos, Quaternion.identity));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector2 spawnPos = new Vector2(8, 3);
            enemi.Add(Instantiate(turner, spawnPos, Quaternion.identity));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            spawnPos = new Vector2(8, -3);
            enemi.Add(Instantiate(chase, spawnPos, Quaternion.identity));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            spawnPos = new Vector2(-8, -3);
            enemi.Add(Instantiate(spawner, spawnPos, Quaternion.identity));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (GameObject sup in enemi)
            {
                Destroy(sup);
            }
            enemi.Clear();
        }

    }
}