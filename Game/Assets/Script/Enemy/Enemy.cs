using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected string name; //name of the perso
    protected int health; //nombre de health

    protected int dammage; //nombre de dommages

    protected int speed; //stat de vitesse
    protected int attack; //stat d'attaque
    protected int shotspeed; //vitesse des projeciles
    protected int attackspeed; //cadence de l'attaque
    protected int attackrange; //distance d'attaque
    private GameObject actual;
    public GameObject Actual => actual;

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

    public Enemy(GameObject gameObject, string name, int health, int dammage, int speed, int attack, int shotspeed,
        int attackspeed,
        int attackrange)
    {
        this.actual = Instantiate(gameObject);

        switch (name)
        {
            case "Patrol":
                var compPatrol = actual.GetComponent<Patrol>();
                compPatrol.speed = speed;
                compPatrol.name = name;
                compPatrol.health = health;
                compPatrol.dammage = dammage;
                compPatrol.attack = attack;
                compPatrol.shotspeed = shotspeed;
                compPatrol.attackspeed = attackspeed;
                compPatrol.attackrange = attackrange;
                break;
            case "Turn":
                var compTurn = actual.GetComponent<Turn>();
                compTurn.speed = speed;
                compTurn.name = name;
                compTurn.health = health;
                compTurn.dammage = dammage;
                compTurn.attack = attack;
                compTurn.shotspeed = shotspeed;
                compTurn.attackspeed = attackspeed;
                compTurn.attackrange = attackrange;
                break;
            case "Chase":
                var compChase = actual.GetComponent<Chase>();
                compChase.speed = speed;
                compChase.name = name;
                compChase.health = health;
                compChase.dammage = dammage;
                compChase.attack = attack;
                compChase.shotspeed = shotspeed;
                compChase.attackspeed = attackspeed;
                compChase.attackrange = attackrange;
                break;
            case "Stay":
                var compStay = actual.GetComponent<Stay>();
               compStay.speed = speed;
               compStay.name = name;
               compStay.health = health;
               compStay.dammage = dammage;
               compStay.attack = attack;
               compStay.shotspeed = shotspeed;
               compStay.attackspeed = attackspeed;
               compStay.attackrange = attackrange;
                break;
        }
    }

    public Enemy()
    {
    }


    public bool IsDead() //if ennemi still alive
    {
        return (this.health > this.dammage);
    }

    //si les dégats doivent affaiblir l'ennmi, il est impacté à un certain pourcentage
    //void Degats()
    //{
    //    this.attack-=this.dammage*100/this.health;
    //    this.attackspeed-=this.dammage*100/this.health;
    //    this.shotspeed-=this.dammage*100/this.health;
    //}

    // Update is called once per frame
}