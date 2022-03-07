using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
 protected string name;//name of the perso
    protected int health; //nombre de health
    protected int dammage;//nombre de dommages
    protected int speed; //stat de vitesse
    protected int attack;//stat d'attaque
    protected int shotspeed; //vitesse des projeciles
    protected int attackspeed;//cadence de l'attaque
    protected int attackrange;//distance d'attaque
    

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
