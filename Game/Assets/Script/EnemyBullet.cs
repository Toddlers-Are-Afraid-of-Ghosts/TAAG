using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    protected int attack;
    protected int shotspeed;
    protected float attackrange;
    protected Vector2 position;
    protected Vector2 direction;

    protected GameObject allyBullet;
    public GameObject Bullet => this.allyBullet;

    public int Attack
    {
        get => this.attack;
        set { this.attack = value; }
    }

    Rigidbody2D fire;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag is not ("Wall" or "Player")) return;
        Destroy(this.gameObject);
    }

    public  EnemyBullet(GameObject bullet, int attack, int shotspeed, float attackrange, Vector2 position,
        Vector2 direction)
    {
        // // allyBullet = Instantiate(bullet, position + direction, quaternion.identity);
        // this.attack = attack;
        // this.shotspeed = shotspeed;
        // this.attackrange = attackrange;
        // this.direction = direction;
        allyBullet = Instantiate(bullet, position+direction , quaternion.identity);
        var comp = allyBullet.GetComponent<EnemyBullet>();
        comp.attack = attack;
        comp.shotspeed = shotspeed;
        comp.attackrange = attackrange;
        comp.direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<Rigidbody2D>();
    }

    void MoveProjectile()
    {
        // transform.position= Vector2.MoveTowards(transform.position,direction ,(shotspeed * Time.deltaTime));
        fire.velocity = (direction * shotspeed * Time.deltaTime);
        this.attackrange -= Time.deltaTime;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        MoveProjectile();
        if (attackrange <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}