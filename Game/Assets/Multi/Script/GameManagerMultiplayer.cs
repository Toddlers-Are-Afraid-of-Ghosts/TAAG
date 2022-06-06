using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameManagerMultiplayer : NetworkBehaviour
{
    private List<GameObject> spawnSpoint;

    [SerializeField] private List<GameObject> prefabEnemy;
    private float cooldown, actualCD;
    public bool active = true;


    // Start is called before the first frame update
    void Start()
    {
        cooldown = 5;
        actualCD = cooldown;
        spawnSpoint = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;
        var g = GameObject.FindGameObjectsWithTag("Player").Length;
        if (g <= 0) SceneManager.LoadScene("Menu");
        if (!active) return;
        if (actualCD <= 0)
        {
            Enemy();
            actualCD = cooldown;
        }
        else
        {
            actualCD -= Time.deltaTime;
        }
    }

    public void Enemy()
    {
        var rng = new Random();
        var enemy = prefabEnemy[rng.Next(0, prefabEnemy.Count - 1)];
        // var enemy = prefabEnemy[20];
        var name = enemy.name;

        var en = Instantiate(enemy, spawnSpoint[rng.Next(0, spawnSpoint.Count - 1)].transform.position,
            Quaternion.identity);
        NetworkServer.Spawn(en);

        var ComptEn = en.GetComponent<EnemyMultiplayer>();
        var result = name switch
        {
            "Patrol" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2, "ghost"),

            "Fantom_bleu" => ComptEn.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.6), 2, "ghost"),
            "Fantome_orange" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2, "ghost"),
            "Fantome_rose" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2, "ghost"),
            "Fantome_rouge" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2, "ghost"),

            "Boo_argent" => ComptEn.Create(name, 10, 2, 5, 500, Convert.ToSingle(0.6), 2, "ghost"),
            "Boo_gold" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2, "ghost"),
            "Boo_jaune" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2, "ghost"),
            "Boo_violet" => ComptEn.Create(name, 10, 2, 5, 400, Convert.ToSingle(0.6), 2, "ghost"),


            "Turn" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "mickey"),

            "Mickey_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 2, 2, "mickey"),
            "Mickey_noir" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2, "mickey"),
            "Mickey_vert" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2, "mickey"),
            "Mickey_marron" => ComptEn.Create(name, 10, 2, 5, 400, 2, 2, "mickey"),

            "Chase" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "stick"),

            "Bones_bleu" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2, "stick"),
            "Bones_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "stick"),
            "Bones_orange" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "stick"),
            "Bones_violet" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "stick"),

            "Stay" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "dreap"),

            "Skull_noir" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "dreap"),
            "Skull_gris" => ComptEn.Create(name, 10, 2, 5, 500, 1, 2, "dreap"),
            "Skull_vert" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "dreap"),
            "Skull_pourpre" => ComptEn.Create(name, 10, 2, 5, 400, 1, 2, "dreap"),


            //Section Boss
            "Pacman" => ComptEn.Create(name, 50, 3, 5, 500, 1, 2, "boss"),
            "Boss_Thomas" => ComptEn.Create(name, 50, 3, 5, 500, 1, 2, "boss"),
            "Hitler" => ComptEn.Create(name, 50, 3, 5, 500, 1, 2, "boss"),


            _ => throw new ArgumentException("invalid name of enemy")
        };
    }
}