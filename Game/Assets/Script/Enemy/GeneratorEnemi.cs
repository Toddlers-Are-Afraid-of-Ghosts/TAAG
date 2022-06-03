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

    private Transform cam;

    public List<Enemy> alive;
    private GameObject rndEnemi;

    public GameObject RndEnemi
    {
        set { rndEnemi = value; }
    }

    public GameObject turn, spawnG, patrol, follower;


    private int spawn = 0;

    public bool active = false;

    public bool win = false;

    private GameObject spawnPoint;
    private RoomsProperties[,] grid;

    private int[] pos;

    private List<GameObject> allspawnpoint;
    private List<GameObject> listPoint;

    // Start is called before the first frame update
    void Start()
    {
        alive = new List<Enemy>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        pos = Vdoor.pos;
        grid = RoomTemplates.grid;
        allspawnpoint = CollectSpawnPoint();
    }


    // Update is called once per frame
    void Update()
    {
        pos = Vdoor.pos;
        if (grid[pos[0], pos[1]].HasBeenEntered && grid[pos[0], pos[1]].IsPLayerIn && !grid[pos[0], pos[1]].IsBoss)
        {
            ClearSpawnPoint(CollectWhoIame());
            return;
        }

        if (active)
        {
            listPoint = CollectWhoIame();
            while (listPoint.Count > 0)
            {
                listPoint = CollectWhoIame();

                rndEnemi = ennemi[Random.Range(0, ennemi.Length - 1)];
                while (rndEnemi.name != "Skull_gris")
                    rndEnemi = ennemi[Random.Range(0, ennemi.Length - 1)];
                var en = CreateEnemy(rndEnemi.name);
                alive.Add(en);

                spawn++;
            }
        }

        if ((grid[pos[0], pos[1]].IsBoss) && alive.Count <= 0)
        {
            if (grid[pos[0], pos[1]].IsBoss)
            {
                var b = boss[Random.Range(0, boss.Length - 1)];
                var listPoint = CollectWhoIame();
                if (listPoint.Count <= 0)
                {
                    return;
                }

                rndEnemi = b;
                alive.Add(CreateEnemy(b.name));
            }

            grid[pos[0], pos[1]].HasBeenEntered = true;
            spawn = 0;
            ClearSpawnPoint(CollectWhoIame());
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

    private List<GameObject> CollectSpawnPoint()
    {
        List<GameObject> allspawn = new List<GameObject>();

        foreach (RoomsProperties Rooms in grid)
        {
            if (Rooms.Room == null) continue;
            var test = Rooms.spawnPoin;
            for (int i = 0; i < Rooms.spawnPoin.Count; i++)
            {
                allspawn.Add(Rooms.spawnPoin[i]);
                // Rooms.spawnPoin.Remove(Rooms.spawnPoin[i]);
            }
        }

        return allspawn;
    }

    public List<GameObject> CollectWhoIame()
    {
        var listPoint = new List<GameObject>();
        foreach (var spawnpoint in allspawnpoint)
        {
            var script = spawnpoint.GetComponent<SpawnSpointProperties>();
            var (x, y) = script.Pos;
            if (pos[0] == x && pos[1] == y)
            {
                listPoint.Add(spawnpoint);
            }
        }

        return listPoint;
    }

    public void ClearSpawnPoint(List<GameObject> spawnPoint)
    {
        foreach (var gameObject in spawnPoint)
        {
            allspawnpoint.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public Enemy CreateEnemy(string name, GameObject position = null)
    {
        if (position == null)
        {
            spawnPoint = listPoint[Random.Range(0, listPoint.Count - 1)];
            allspawnpoint.Remove(spawnPoint);
            this.listPoint.Remove(spawnPoint);
            Destroy(spawnPoint);
        }
        else
        {
            spawnPoint = position;
            spawnPoint.transform.position += Vector3.up;
        }


        var en = Instantiate(rndEnemi, spawnPoint.transform.position, Quaternion.identity, cam);
        var ComptEn = en.GetComponent<Enemy>();


        var result = name switch
        {
            "Patrol" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),

            "Fantom_bleu" => ComptEn.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.6), 2),
            "Fantome_orange" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2),
            "Fantome_rose" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2),
            "Fantome_rouge" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2),

            "Boo_argent" => ComptEn.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.6), 2),
            "Boo_gold" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2),
            "Boo_jaune" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2),
            "Boo_violet" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2),


            "Turn" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),

            "Mickey_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 2, 2),
            "Mickey_noir" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2),
            "Mickey_vert" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2),
            "Mickey_marron" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2),

            "Chase" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),

            "Bones_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Bones_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Bones_orange" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Bones_violet" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),

            "Stay" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),

            "Skull_noir" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Skull_gris" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Skull_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),
            "Skull_pourpre" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2),


            //Section Boss
            "Pacman" => ComptEn.Create(name, 50, 2, 5, 500, 1, 2),
            "Boss_Thomas" => ComptEn.Create(name, 50, 2, 5, 500, 1, 2),


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