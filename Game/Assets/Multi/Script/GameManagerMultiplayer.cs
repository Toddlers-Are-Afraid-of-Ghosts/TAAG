using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class GameManagerMultiplayer : NetworkBehaviour
{
    private List<GameObject> spawnSpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnSpoint = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;
    }
}
