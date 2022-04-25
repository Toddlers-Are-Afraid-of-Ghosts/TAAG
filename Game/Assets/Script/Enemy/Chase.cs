using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Enemy
{
    public GameObject player;
    private Transform cam;
    public Transform moveSpots;
    public GameObject bullet;
    private Transform spot;

    // Enemy enemy = new Enemy("Chaser", 10, 0, 5, 5, 5, 10, 10);
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "AllyBullet":
            {
                var compt = other.gameObject.GetComponent<AllyBullet>();
                this.health -= compt.Attack;
                break;
            }
        }
    }

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        player = GameObject.FindWithTag("Player");
        spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Chased();
        if (actualcooldown <= 0)
            Attack();
        else
        {
            actualcooldown -= Time.deltaTime;
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);
    }

    private void Chased()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }

  

    void Attack()
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
                var tir = bul.GetComponent<EnemyBullet>();
                tir.Setup(attack, shotspeed, attackrange, direction);
                
               
            }
            else
            {
                Vector3 direction = Vector2.left;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBullet>();
                tir.Setup(attack, shotspeed, attackrange, direction);
            }
        }
        else
        {
            if (d.y >= 0)
            {
                Vector3 direction = Vector2.up;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBullet>();
                tir.Setup(attack, shotspeed, attackrange, direction);
            }
            else
            {
                Vector3 direction = Vector2.down;
                var bul = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                var tir = bul.GetComponent<EnemyBullet>();
                tir.Setup(attack, shotspeed, attackrange, direction);;
            }
        }
    }
}