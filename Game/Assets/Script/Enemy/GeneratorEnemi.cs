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
    public GameObject winPanel;
    public GameObject[] ennemi;
    private List<GameObject> allenemi;
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

    // Start is called before the first frame update
    void Start()
    {
        waitspawn = 2;
        spawnPos = new Vector2(2, 3);
        allenemi = new List<GameObject>();
        alive = new List<Enemy>();
        spawntime = Random.Range(0, 10);
        cam = GameObject.FindWithTag("MainCamera").transform;
        DuplicateList();
    }

    void DuplicateList()
    {
        for (int i = 0; i < 10; i++)
        {
            foreach (var objet in ennemi)
            {
                allenemi.Add(objet);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var alld = GameObject.FindGameObjectsWithTag("Ennemy");
        if (active && spawn < max)
        {
            if (spawntime > 0)
            {
                // Debug.Log($"Spawn in {waitspawn}");
                if (waitspawn <= 0)
                {
                    var rnd = Random.Range(0, allenemi.Count - 1);
                    rndEnemi = allenemi[rnd];
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
            if (enemy.name is "Pacman")
            {
                win = true;
            }

        }

        foreach (var fGameObject in alld)
        {
            var sc = fGameObject.GetComponents<Enemy>()[0];
            if (sc.name is "Pacman(Clone)" && sc.Health<0)
            {
                win = true;
            }
        }
    }

    public Enemy CreateEnemy(string name)
    {
        var en = Instantiate(rndEnemi, cam);
        var ComptEn = en.GetComponent<Enemy>();


        var result = name switch
        {
            "Patrol" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
            "Turn" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2),
            "Chase" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2),
            "Stay" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2),
            "Pacman" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2),
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