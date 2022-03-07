using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Perso : MonoBehaviour
{
    protected string name; //name of the perso
    protected int health; //nombre de health
    protected int dammage;//nombre de dommages
    protected int bonushealth;//nombre de health bonus
    protected int speed; //stat de vitesse
    protected int attack;//stat d'attaque
    protected int shotspeed; //vitesse des projeciles
    protected int attackspeed;//cadence de l'attaque
    protected int attackrange;//distance d'attaque
    [SerializeField]
    int speedbase = 40;
    Rigidbody2D rb;

    public string Name => this.name;

    public int Health
    {
        get => this.health;
        set { this.health = value; }
    }

    public int Dammage
    {
        get => this.dammage;
        set { this.dammage = value; }
    }

    public int Bonushealth
    {
        get => this.bonushealth;
        set { this.bonushealth = value; }
    }

    public int Speed
    {
        get => this.speed;
        set { this.speed = value; }
    }

    public int Attack
    {
        get => this.attack;
        set { this.attack = value; }
    }

    public int Shotspeed
    {
        get => this.shotspeed;
        set { this.shotspeed = value; }
    }

    public int Attackspeed
    {
        get => this.attackspeed;
        set { this.attackspeed = value; }
    }

    public int Attackrange
    {
        get => this.attackrange;
        set { this.attackrange = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (IsDead())
        {
            Move();
        }

    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal * speedbase, vertical * speedbase);
        rb.velocity = (move * speedbase * Time.deltaTime);
    }
    bool IsDead() //if player still alive
    {
        return (this.health + this.bonushealth > this.dammage);
    }
}
