using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update protected string name; //name of the perso
    int health; //nombre de health
    public int maxHealth; //Santé Max
    public int bonusHealth; //nombre de health bonus
    public int speed; //stat de vitesse
    public int attack; //stat d'attaque
    public int shotSpeed; //vitesse des projeciles
    public float cooldown; //cadence de l'attaque
    public int attackRange; //distance d'attaque
    public GameObject bullet;
    public GameObject player;
    Rigidbody2D rb;
    private float actualcooldown;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag is "EnemyBullet")
        {
            var compt = other.gameObject.GetComponent<Enemy>();
            health -= compt.Attack;
        }
        
    }
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        actualcooldown = cooldown;
    }

    void FixedUpdate()
    {
        if (IsAlive())
        {
            if (actualcooldown <= 0)
            {
                Fire();
            }
            else
                actualcooldown -= Time.deltaTime;


            Moveto();
        }
        else
        {
            Destroy(player);
        }
    }

    void Moveto()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal * speed, vertical * speed);
        rb.velocity = (move * speed * Time.deltaTime);
    }

    bool IsAlive() //if player still alive
    {
        return (this.health + this.bonusHealth > 0);
    }

    void Fire()
    {
        float firehorizontal = Input.GetAxis("FireHorizontal");
        float firevertical = Input.GetAxis("FireVertical");

        switch (firevertical)
        {
            case 1:
            {
                new AllyBullet(bullet, attack, shotSpeed, attackRange, transform.position,
                    Vector2.up);
                actualcooldown = cooldown;
                break;
            }
            case -1:
            {
                new AllyBullet(bullet, attack, shotSpeed, attackRange, transform.position,
                    Vector2.down);
                actualcooldown = cooldown;
                break;
            }
            default:
            {
                switch (firehorizontal)
                {
                    case 1:
                    {
                        new AllyBullet(bullet, attack, shotSpeed, attackRange, transform.position,
                            Vector2.right);
                        actualcooldown = cooldown;
                        break;
                    }
                    case -1:
                    {
                        new AllyBullet(bullet, attack, shotSpeed, attackRange, transform.position,
                            Vector2.left);
                        actualcooldown = cooldown;
                        break;
                    }
                }

                break;
            }
        }
    }
}