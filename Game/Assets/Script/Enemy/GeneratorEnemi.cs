using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEnemi : MonoBehaviour
{
    public GameObject[] allenemi;
    private List<GameObject> alive;
    float spawntime;
    GameObject rndEnemi;
    Vector2 spawnPos;
    public int max;
    public int current;
    int spawn = 1;
    float waitspawn;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        waitspawn = 2;
        spawnPos = new Vector2(2, 3);
        alive = new List<GameObject>();
        spawntime = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && spawn<max)
        {

            if (spawntime > 0 && alive.Count <= current)
            {
                Debug.Log($"Spawn in {waitspawn}");
                if (waitspawn < 1)
                {
                    rndEnemi = allenemi[Random.Range(1, alive.Count)];
                    alive.Add(Instantiate(rndEnemi, spawnPos, Quaternion.identity));
                    spawntime -= Time.deltaTime;
                    spawn++;
                    waitspawn = 5;
                }
                waitspawn -= Time.deltaTime;
            }

            if (alive.Count <= 0)
            {
                spawntime = Random.Range(0, 10);
            }
        }
    }
    
}
