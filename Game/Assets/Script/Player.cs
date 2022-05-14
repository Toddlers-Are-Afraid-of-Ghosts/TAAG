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
    public GameObject bullet;
    public GameObject player;
    Rigidbody2D rb;
    public Animator animator;
    private float actualcooldown;
    public float Health => health;

    //needed for player selection
    public CharacterDatabase CharacterDB;

    public SpriteRenderer artworkSprite;

    private int SelectedOption = 0;


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag is "EnemyBullet")
        {
            var compt = other.gameObject.GetComponent<EnemyBullet>();
            health -= compt.Attack;
        }
    }

    public void addstat(float he, int boHe, int sp, int at, int shSp, int co, float atRa)
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
    }


    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        actualcooldown = cooldown;
        animator = GetComponent<Animator>();

        if(!PlayerPrefs.HasKey("SelectedOption"))
        {
            SelectedOption=0;
        }
        else
        {
            Load();
        }

        UpdateCharacter(SelectedOption);
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

    //function copied from charactermanager
    private void UpdateCharacter(int SelectedOption)
    {
        Character character= CharacterDB.GetCharacter(SelectedOption);
        artworkSprite.sprite=character.CharacterSprite;
    }

    private void Load()
    {
        SelectedOption = PlayerPrefs.GetInt("SelectedOption");
    }
}