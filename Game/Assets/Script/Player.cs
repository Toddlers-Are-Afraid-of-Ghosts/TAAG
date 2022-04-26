using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update protected string name; //name of the perso
    private float health; //nombre de health
    public float maxHealth; //Santé Max
    public int bonusHealth; //nombre de health bonus
    public int speed; //stat de vitesse
    public int attack; //stat d'attaque
    public int shotSpeed; //vitesse des projeciles
    public float cooldown; //cadence de l'attaque
    public float attackRange; //distance d'attaque
    public GameObject bullet;
    public GameObject player;
    Rigidbody2D rb;
    public Animator animator;
    private float actualcooldown;
    public float Health => health;

    public void OnCollisionEnter2D(Collision2D other)
    {
        float collect = Input.GetAxis("Collect");
        if (other.gameObject.tag is "EnemyBullet")
        {
            var compt = other.gameObject.GetComponent<EnemyBullet>();
            health -= compt.Attack;
        }

        if (other.gameObject.tag is "Item" && collect != 0)
        {
            var compt = other.gameObject.GetComponent<Item>();
            health += compt.Health;
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            bonusHealth += compt.BonusHealth;
            speed += compt.Speed;
            attack += compt.Attack;
            shotSpeed += compt.ShotSpeed;
            cooldown += compt.Cooldown;
            attackRange += compt.AttackRange;
        }
    }

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        actualcooldown = cooldown;
        animator = GetComponent<Animator>();
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
            Destroy(this.gameObject);
        }
    }


    void Moveto()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal * speed, vertical * speed);
        rb.velocity = (move * speed * Time.deltaTime);

        //animation
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", move.magnitude);
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
                Vector3 direction = Vector2.up;
                var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                var tir = bul.GetComponent<AllyBullet>();
                tir.Setup(attack, shotSpeed, attackRange, direction);
                actualcooldown = cooldown;
                break;
            }
            case -1:
            {
                Vector3 direction = Vector2.down;
                var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                var tir = bul.GetComponent<AllyBullet>();
                tir.Setup(attack, shotSpeed, attackRange, direction);
                actualcooldown = cooldown;
                break;
            }
            default:
            {
                switch (firehorizontal)
                {
                    case 1:
                    {
                        Vector3 direction = Vector2.right;
                        var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                        var tir = bul.GetComponent<AllyBullet>();
                        tir.Setup(attack, shotSpeed, attackRange, direction);
                        actualcooldown = cooldown;
                        break;
                    }
                    case -1:
                    {
                        Vector3 direction = Vector2.left;
                        var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                        var tir = bul.GetComponent<AllyBullet>();
                        tir.Setup(attack, shotSpeed, attackRange, direction);
                        actualcooldown = cooldown;
                        break;
                    }
                }

                break;
            }
        }
    }
}