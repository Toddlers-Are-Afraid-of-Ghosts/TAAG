using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


public class Patrol : Enemy
{
    public Transform moveSpots;
    public GameObject player;
    private Transform spot;
    private float waitTime;
    private bool wallhit = false;
    private Transform cam;
    public GameObject bullet;
    public float maxX, minX, maxY, minY, chaseRange, startWaitTime;
    public Animator animator; //objet animation

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)) + cam.position;
                wallhit = true;
                break;
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
        animator=GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        player = GameObject.FindWithTag("Player");
        waitTime = Random.Range(0, startWaitTime);
        spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)) + cam.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DistanceToPlayer() <= chaseRange && !wallhit)
        {
            this.Chase();
        }
        else
        {
            this.Patro();
        }

        if (Dead())
        {
            
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);

        //animation
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal * speed, vertical * speed);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", move.magnitude);
    }

    private void Patro()
    {
        this.Move();

        if (Vector2.Distance(transform.position, spot.position) <= 0.2f)
        {
            wallhit = false;
            if (waitTime <= 0)
            {
                spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)) + cam.position;

                waitTime = Random.Range(0, startWaitTime);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private void Chase()
    {
        if (DistanceToPlayer() > 1.3) // evite que l'ennemi soit trop
        {
            this.Move();
            spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
            if (actualcooldown <= 0)
                Attack();
            else
            {
                actualcooldown -= Time.deltaTime;
            }
        }
    }

    bool Dead()
    {
        return this.health <= 0;
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