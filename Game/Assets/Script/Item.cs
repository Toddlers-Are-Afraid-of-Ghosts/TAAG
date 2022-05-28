using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int health;
    public int bonusHealth;
    public int speed;
    public int attack;
    public int shotSpeed;
    public float cooldown;
    public float attackRange;
    public int gold;

    public GameObject item;

    public Item(int health = 0, int bonusHealth = 0, int speed = 0, int attack = 0, int shotSpeed = 0, float cooldown = 0, float attackRange = 0, int gold = 0)
    {
        this.health = health;
        this.bonusHealth = bonusHealth;
        this.speed = speed;
        this.attack = attack;
        this.shotSpeed = shotSpeed;
        this.cooldown = cooldown;
        this.attackRange = attackRange;
        this.gold = gold;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        float collect = Input.GetAxis("Collect");
        if (other.gameObject.tag is "Player")
        {
            var compt = other.gameObject.GetComponent<Player>();
            compt.addstat(this.health, this.bonusHealth, this.speed, this.attack, this.shotSpeed, this.cooldown, this.attackRange, this.gold);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
