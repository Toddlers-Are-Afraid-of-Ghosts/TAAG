using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform moveSpots;
    public GameObject Player;

    public float maxX, minX, maxY, minY, startWaitTime;
    private float waitTime;
    private float health, attack, defense, speed, attackCooldown, maxHealth, chaseRange;

    public float Health
    {
        get => health;
        set => health = value;
    }

    public float Attack => attack;

    public float Defense => defense;

    public float Speed => speed;

    public float AttackCooldown => attackCooldown;

    public float MAXHealth => maxHealth;

    public float ChaseRange => chaseRange;

    public Enemy(float health, float attack, float defense, float speed, float attackCooldown, float maxHealth,
        float chaseRange)
    {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.attackCooldown = attackCooldown;
        this.maxHealth = maxHealth;
        this.chaseRange = chaseRange;
    }

    public bool Heel(float many)
    {
        if (health + many > maxHealth)
        {
            return false;
        }
        else
        {
            health += many;
            return true;
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
    }

    public void Patrol()
    {
        Move();
        if (Vector2.Distance(transform.position, moveSpots.position) <= 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

                waitTime = Random.Range(0, startWaitTime);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void Chase()
    {
        Move();
        moveSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
    }
}