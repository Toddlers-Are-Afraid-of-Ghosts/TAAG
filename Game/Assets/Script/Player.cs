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
    public int fireRate; //cadence de l'attaque
    public int attackRange; //distance d'attaque

    public Animator animator; //objet animation
    public GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        health=maxHealth;
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }
    void Update()
    {
        if (IsAlive())
        {
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

        //animation
        animator.SetFloat("Horizontal",horizontal);
        animator.SetFloat("Vertical",vertical);
        animator.SetFloat("Speed",move.magnitude);
    }
     bool IsAlive() //if player still alive
    {
        return (this.health + this.bonusHealth > 0);
    }
    
}