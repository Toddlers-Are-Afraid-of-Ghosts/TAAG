using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected int health;
    protected int bonusHealth;
    protected int speed;
    protected int attack;
    protected int shotSpeed;
    protected int fireRate;
    protected float attackRange;

    public GameObject item;

    public int Health
    {
        get => this.health;
        set { this.health = value; }
    }
    public int BonusHealth
    {
        get => this.bonusHealth;
        set { this.bonusHealth = value; }
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
    public int ShotSpeed
    {
        get => this.shotSpeed;
        set { this.shotSpeed = value; }
    }
    public int FireRate
    {
        get => this.fireRate;
        set { this.fireRate = value; }
    }
    public float AttackRange
    {
        get => this.attackRange;
        set { this.attackRange = value; }
    }

    public Item(int health = 0, int bonusHealth = 0, int speed = 0, int attack = 0, int shotSpeed = 0, int fireRate = 0, float attackRange = 0)
    {
        this.health = health;
        this.bonusHealth = bonusHealth;
        this.speed = speed;
        this.attack = attack;
        this.shotSpeed = shotSpeed;
        this.fireRate = fireRate;
        this.attackRange = attackRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        float collect = Input.GetAxis("Collect");
        if (other.gameObject.tag == "Player" && collect != 0)
        {
            Destroy(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
