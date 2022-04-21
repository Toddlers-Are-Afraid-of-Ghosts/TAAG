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
                actual.GetComponent<Patrol>().speed = speed;
                actual.GetComponent<Patrol>().name = name;
                actual.GetComponent<Patrol>().health = health;
                actual.GetComponent<Patrol>().dammage = dammage;
                actual.GetComponent<Patrol>().attack = attack;
                actual.GetComponent<Patrol>().shotspeed = shotspeed;
                actual.GetComponent<Patrol>().attackspeed = attackspeed;
                actual.GetComponent<Patrol>().attackrange = attackrange;
                break;
            case "Turn":
                actual.GetComponent<Turn>().speed = speed;
                actual.GetComponent<Turn>().name = name;
                actual.GetComponent<Turn>().health = health;
                actual.GetComponent<Turn>().dammage = dammage;
                actual.GetComponent<Turn>().attack = attack;
                actual.GetComponent<Turn>().shotspeed = shotspeed;
                actual.GetComponent<Turn>().attackspeed = attackspeed;
                actual.GetComponent<Turn>().attackrange = attackrange;
                break;
            case "Chase":
                actual.GetComponent<Chase>().speed = speed;
                actual.GetComponent<Chase>().name = name;
                actual.GetComponent<Chase>().health = health;
                actual.GetComponent<Chase>().dammage = dammage;
                actual.GetComponent<Chase>().attack = attack;
                actual.GetComponent<Chase>().shotspeed = shotspeed;
                actual.GetComponent<Chase>().attackspeed = attackspeed;
                actual.GetComponent<Chase>().attackrange = attackrange;
                break;
            case "Stay":
                actual.GetComponent<Stay>().speed = speed;
                actual.GetComponent<Stay>().name = name;
                actual.GetComponent<Stay>().health = health;
                actual.GetComponent<Stay>().dammage = dammage;
                actual.GetComponent<Stay>().attack = attack;
                actual.GetComponent<Stay>().shotspeed = shotspeed;
                actual.GetComponent<Stay>().attackspeed = attackspeed;
                actual.GetComponent<Stay>().attackrange = attackrange;
                break;
                
        }


    }

    public Enemy()
    {
    }

     void Start()
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