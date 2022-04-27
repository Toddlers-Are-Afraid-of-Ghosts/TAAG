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

    // Start is called before the first frame update
    void Start()
    {
        waitspawn = 2;
        spawnPos = new Vector2(2, 3);
        // allenemi = new List<GameObject>();
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
                // allenemi.Add(objet);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var alld = GameObject.FindGameObjectsWithTag("Boss"); 
        if (active && spawn < max)
        {
            if (spawntime > 0)
            {
                // Debug.Log($"Spawn in {waitspawn}");
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
            var rnd = Random.Range(0, boss.Length - 1);
            rndEnemi = boss[rnd];
            var en = CreateEnemy(rndEnemi.name);
            
            alive.Add(en);
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
            if (sc.tag is "Boss" && sc.Health <= 0)
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