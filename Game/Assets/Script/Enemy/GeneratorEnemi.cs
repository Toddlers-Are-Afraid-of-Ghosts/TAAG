using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class GeneratorEnemi : MonoBehaviour
{
    public GameObject[] ennemi;

    public GameObject[] boss;

    // private List<GameObject> allenemi;
    private Transform cam;
    public List<Enemy> alive;
    float spawntime;
    GameObject rndEnemi;
    Vector2 spawnPos;
    public int max;
    private int spawn = 0;
    float waitspawn;
    public bool active = false;
    public bool win = false;
    private Transform spawnPoint;
    private RoomsProperties[,] grid;
    private int[] pos;
    private int size;

    private List<Transform> allspawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        waitspawn = 2;
        spawnPos = new Vector2(2, 3);
        alive = new List<Enemy>();
        spawntime = Random.Range(0, 10);
        cam = GameObject.FindWithTag("MainCamera").transform;
        grid = RoomTemplates.grid;
        pos = Vdoor.pos;
        size = RoomTemplates.size;
        allspawnpoint = CollectSpawnPoint();
    }


    // Update is called once per frame
    void Update()
    {
        pos = Vdoor.pos;
        if (grid[pos[0], pos[1]].HasBeenEntered && grid[pos[0], pos[1]].IsPLayerIn)
            return;

        if (active && spawn < max)
        {
            if (spawntime > 0)
            {
                if (waitspawn <= 0)
                {
                    var rnd = Random.Range(0, ennemi.Length - 1);
                    rndEnemi = ennemi[rnd];
                    var en = CreateEnemy(rndEnemi.name);
                    alive.Add(en);
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

        if (spawn == max && alive.Count <= 0)
        {
            grid[pos[0], pos[1]].HasBeenEntered = true;
            spawn = 0;
            CleanSpot();
        }

        int i = 0;
        while (i < alive.Count)
        {
            var enemy = alive[i];
            i++;
            if (enemy.Health > 0) continue;
            alive.Remove(enemy);
            Destroy(enemy.gameObject);
            if (enemy.tag is "Boss")
            {
                win = true;
            }
        }
    }

    private List<Transform> CollectSpawnPoint()
    {
        List<Transform> allspawn = new List<Transform>();

        foreach (var Rooms in grid)
        {
            if (Rooms == null) continue;
            foreach (var point in Rooms.spawnPoint)
            {
                allspawn.Add(point);
            }
        }

        return allspawn;
    }

    public Enemy CreateEnemy(string name)
    {
        var listPoint = new List<Transform>();
        foreach (var spawnpoint in allspawnpoint)
        {
            var script = spawnpoint.GetComponent<SpawnSpointProperties>();
            var (x, y) = script.Pos;
            if (pos[0] == x && pos[1] == y)
            {
                listPoint.Add(spawnpoint);
            }
        }

        spawnPoint = listPoint[Random.Range(0, listPoint.Count - 1)].transform;
        var en = Instantiate(rndEnemi, spawnPoint.position, Quaternion.identity, cam);
        var ComptEn = en.GetComponent<Enemy>();


        var result = name switch
        {
            "Patrol" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Fantom_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Mickey_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Boo_argent" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Bones_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Skull_gris" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Turn" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Mickey_noir" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Fantome_orange" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Boo_gold" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Bones_orange" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Skull_noir" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Chase" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Skull_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Fantome_rose" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Boo_jaune" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Mickey_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Bones_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Stay" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Skull_pourpre" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Bones_violet" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Mickey_marron" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Fantome_rouge" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Boo_violet" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Pacman" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Boss_Thomas" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),


            _ => throw new ArgumentException("invalid name of enemy")
        };

        return ComptEn;
    }

    public void CleanEnemy()
    {
        foreach (var enemy in alive)
        {
            Destroy(enemy.gameObject);
        }
    }

    void CleanSpot()
    {
        var allSpot = GameObject.FindGameObjectsWithTag("MoveSpot");
        foreach (var spot in allSpot)
        {
            Destroy(spot.gameObject);
        }
    }
}