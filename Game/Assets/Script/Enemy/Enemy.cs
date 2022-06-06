using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected string name; //name of the perso
    protected int health; //nombre de health

    protected int speed; //stat de vitesse
    protected int attack; //stat d'attaque
    protected int shotspeed; //vitesse des projeciles
    protected float cooldown; //cadence de l'attaque
    protected int attackrange; //distance d'attaque
    private GameObject actual;
    public GameObject Actual => actual;

    protected float actualcooldown;
        public string Name => this.name;

    public int Health
    {
        get => this.health;
        set { this.health = value; }
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

    public float Cooldown
    {
        get => this.cooldown;
        set { this.cooldown = value; }
    }

    public int Attackrange
    {
        get => this.attackrange;
        set { this.attackrange = value; }
    }

    protected string sfx;
    public string SFX => this.sfx;
                    

    public int Create(string name, int health, int attack, int speed, int shotspeed,
        float cooldown,
        int attackrange,string sfx)
    {
        this.health = health;
        this.speed = speed;
        this.name = name;
        this.health = health;
        this.attack = attack;
        this.shotspeed = shotspeed;
        this.cooldown = cooldown;
        this.attackrange = attackrange;
        actualcooldown = cooldown;
        this.sfx = sfx;
        return 1;
    }


    public bool IsDead() //if ennemi still alive
    {
        return (this.health <= 0);
    }

    //si les dégats doivent affaiblir l'ennmi, il est impacté à un certain pourcentage
    //void Degats()
    //{
    //    this.attack-=this.dammage*100/this.health;
    //    this.cooldown-=this.dammage*100/this.health;
    //    this.shotspeed-=this.dammage*100/this.health;
    //}

    // Update is called once per frame
}