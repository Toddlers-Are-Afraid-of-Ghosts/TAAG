using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] items;

    private int[] pos;
    
    private RoomsProperties[,] grid;
    private int size;
    private GameObject item;
    public GameObject coin;
    public GameObject health;
    private Random rng;
    
    
    void Start() {
        rng = new Random();
        item = items[rng.Next(0, items.Length)];
        grid = RoomTemplates.grid;
        size = RoomTemplates.size;
    }

    // Update is called once per frame
    void Update() {
        int middle = size / 2;
        pos = Vdoor.pos;

        if (grid[pos[0], pos[1]].IsItems && !grid[pos[0], pos[1]].ItemSpawned) {
            Vector3 coords = new Vector3((pos[0] - middle) * 39, (pos[1] - middle) * 19, 0);
            Instantiate(item, coords, Quaternion.identity);
            grid[pos[0], pos[1]].ItemSpawned = true;
        }

        if (grid[pos[0], pos[1]].IsPLayerIn && GeneratorEnemi.alive.Count <= 0 && !grid[pos[0], pos[1]].ItemSpawned) {
            int pif = rng.Next(0, 4);
            if (pif == 0) {
                Vector3 coords = new Vector3((pos[0] - middle) * 39, (pos[1] - middle) * 19, 0);
                Instantiate(coin, coords, Quaternion.identity);
            }
            else if (pif == 1) {
                Vector3 coords = new Vector3((pos[0] - middle) * 39, (pos[1] - middle) * 19, 0);
                Instantiate(health, coords, Quaternion.identity);
            }
            grid[pos[0], pos[1]].ItemSpawned = true;
        }
    }
}
