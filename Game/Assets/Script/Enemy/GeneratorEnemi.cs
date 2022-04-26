using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorEnemi : MonoBehaviour
{
    public GameObject[] ennemi;
    private List<GameObject> allenemi;
    private Transform cam;
    public List<Enemy> alive;
    float spawntime;
    GameObject rndEnemi;
    Vector2 spawnPos;
    public int max;
    public int current;
    private int spawn = 0;
    float waitspawn;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        waitspawn = 2;
        spawnPos = new Vector2(2, 3);
        allenemi = new List<GameObject>();
        alive = new List<Enemy>();
        spawntime = Random.Range(0, 10);
        cam = GameObject.FindWithTag("MainCamera").transform;
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
        if (active && spawn < max)
        {
            if (spawntime > 0 && spawn <= current)
            {
                Debug.Log($"Spawn in {waitspawn}");
                if (waitspawn <= 0)
                {
                    var rnd = Random.Range(0, ennemi.Length - 1);
                    rndEnemi = ennemi[rnd];
                    var en = Enemy(rndEnemi.name);
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

        int i = 0;
        while (i < alive.Count)
        {
            var enemy = alive[i];
            i++;
            if (enemy.Health > 0) continue;
            alive.Remove(enemy);
            Destroy(enemy.gameObject);
            spawn--;
        }
    }

    private Enemy Enemy(string name)
    {
        var en = Instantiate(rndEnemi, cam);
        var coucou = en.GetComponent<Enemy>();
        

        var result = name switch
        {
            "Patrol" => coucou.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.3), 2),
            "Fantom_bleu" => coucou.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.3), 2),
            "Mickey_bleu" => coucou.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.3), 2),
            "Boo_argent" => coucou.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.3), 2),
            "Bones_bleu" => coucou.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.3), 2),
            "Skull_gris" => coucou.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.3), 2),

            "Turn" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Mickey_noir" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Fantome_orange" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Boo_gold" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Bones_orange" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Skull_noir" => coucou.Create(name, 10, 2, 5, 500, 1, 2),

            "Chase" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Skull_vert" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Fantome_rose" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Boo_jaune" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Mickey_vert" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Bones_vert" => coucou.Create(name, 10, 2, 5, 500, 1, 2),

            "Stay" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Skull_pourpre" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Bones_violet" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Mickey_marron" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Fantome_rouge" => coucou.Create(name, 10, 2, 5, 500, 1, 2),
            "Boo_violet" => coucou.Create(name, 10, 2, 5, 500, 1, 2),

            "Pacman"=> coucou.Create(name,10,2,5,500,1,2),
            "Boss_Thomas"=> coucou.Create(name,10,2,5,500,1,2),

            _ => throw new ArgumentException("invalid name of enemy")
        };

        return coucou;
    }

    public void CleanEnemy()
    {
        foreach (var enemy in alive)
        {
            Destroy(enemy.gameObject);
        }
    }
}