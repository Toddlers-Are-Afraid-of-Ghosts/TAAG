using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Random = System.Random;

public class ChaseMultiplayer : EnemyMultiplayer
{
    // Start is called before the first frame update
    public GameObject player;
    private Transform cam;
    public Transform moveSpots;
    public GameObject bullet;
    private Transform spot;
    public Animator animator;

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "AllyBullet":
            {
                var compt = other.gameObject.GetComponent<AllyBulletMultiplayer>();
                this.health -= compt.Attack;
                break;
            }
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        var pl = GameObject.FindGameObjectsWithTag("Player");

        player = pl[new Random().Next(0, pl.Length - 1)];
        spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isServer) return;
        Chased();
        if (actualcooldown <= 0)
            Attack();
        else
        {
            actualcooldown -= Time.deltaTime;
        }

        if (health <= 0)
        {
            // ManagerSfx.PlaySound("ghost");
            NetworkServer.Destroy(this.gameObject);
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);

        //animation
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Speed", transform.position.magnitude);
    }

    private void Chased()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }

    new void Attack()
    {
        actualcooldown = cooldown;
        Vector2 d = new Vector2(player.transform.position.x - this.transform.position.x,
            player.transform.position.y - this.transform.position.y);
        if (d.x * d.x > d.y * d.y)
        {
            if (d.x >= 0)
            {
                Vector3 direction = Vector2.right;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBulletMultiplayer>();
                tir.Setup(attack, shotspeed, attackrange, direction);
                NetworkServer.Spawn(bul);
            }
            else
            {
                Vector3 direction = Vector2.left;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBulletMultiplayer>();
                tir.Setup(attack, shotspeed, attackrange, direction);
                NetworkServer.Spawn(bul);
            }
        }
        else
        {
            if (d.y >= 0)
            {
                Vector3 direction = Vector2.up;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBulletMultiplayer>();
                tir.Setup(attack, shotspeed, attackrange, direction);
                NetworkServer.Spawn(bul);
            }
            else
            {
                Vector3 direction = Vector2.down;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBulletMultiplayer>();
                tir.Setup(attack, shotspeed, attackrange, direction);
                ;
                NetworkServer.Spawn(bul);
            }
        }
    }
}