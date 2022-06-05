using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update protected string name; //name of the perso
    public float health; //nombre de health
    public float maxHealth; //Santé Max
    public int bonusHealth; //nombre de health bonus
    public int speed; //stat de vitesse
    public int attack; //stat d'attaque
    public int shotSpeed; //vitesse des projeciles
    public float cooldown; //cadence de l'attaque
    public float attackRange; //distance d'attaque
    public int gold;
    public GameObject bullet;
    public GameObject player;
    Rigidbody2D rb;
    public Animator animator;
    private float actualcooldown;
    public static int[] pos;
    public static int size;
    public float Health => health;

    public bool god = true;
    //needed for player selection


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag is "EnemyBullet")
        {
            if (god) return;
            var compt = other.gameObject.GetComponent<EnemyBullet>();
            health -= compt.Attack;
            ManagerSfx.PlaySound("playerHit");
        }

        if (other.gameObject.tag is "Boss")
        {
            var compt = other.gameObject.GetComponent<Boss>();
            health -= compt.Attack;
            ManagerSfx.PlaySound("playerHit");
        }
    }

    public void addstat(float he, int boHe, int sp, int at, int shSp, float co, float atRa, int go)
    {
        this.health += he;
        if (this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
        }

        this.bonusHealth += boHe;
        this.speed += sp;
        this.attack += at;
        this.shotSpeed += shSp;
        this.cooldown -= co;
        this.attackRange += atRa;
        this.gold += go;
    }


    void Start() {
        size = RoomTemplates.size;
        pos = new[] {size / 2, size / 2};
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        actualcooldown = cooldown;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
        if (IsAlive())
        {
            if (health < 5)
                ManagerSfx.PlaySound("Health");
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
        // ManagerSfx.PlaySound("playerWalk");
        float horizontal=0;
        float vertical=0;
        if (Input.GetKey(InputManager.IM.moveUp))
        {
            vertical++;
        }
        if (Input.GetKey(InputManager.IM.moveLeft))
        {
            horizontal--;
        }
        if (Input.GetKey(InputManager.IM.moveRight))
        {
            horizontal++;
        }
        if (Input.GetKey(InputManager.IM.moveDown))
        {
            vertical--;
        }
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
        float firehorizontal = 0;
        float firevertical = 0;
        if (Input.GetKey(InputManager.IM.fireUp))
        {
            firevertical++;
        }
        if (Input.GetKey(InputManager.IM.fireLeft))
        {
            firehorizontal--;
        }
        if (Input.GetKey(InputManager.IM.fireRight))
        {
            firehorizontal++;
        }
        if (Input.GetKey(InputManager.IM.fireDown))
        {
            firevertical--;
        }

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

    //function copied from charactermanager
}