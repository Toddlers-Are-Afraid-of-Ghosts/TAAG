using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class class_perso : MonoBehaviour
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
    int speedbase = 1000;
    Rigidbody2D rb;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    

    public string Name=>this.name;

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


    bool IsDead() //if player still alive
    { 
        return (this.health+this.bonushealth > this.dammage);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical") ;
        Vector2 move = new Vector2(horizontal * speedbase, vertical * speedbase);
        rb.velocity= (move*speedbase*Time.deltaTime);
    }

    //void Fire()
    //{
    //    
    //}

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            Move();
        }
        
    }
}
