using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class AllyBulletMultiplayer : NetworkBehaviour
{
    // Start is called before the first frame update
    protected int attack;
    protected int shotspeed;
    protected float attackrange;
    [SyncVar] protected Vector2 position;
    protected Vector2 direction;

    [SyncVar] protected GameObject allyBullet;
    public GameObject Bullet => this.allyBullet;

    public int Attack
    {
        get => this.attack;
        set { this.attack = value; }
    }

    Rigidbody2D fire;

    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!isServer) return;
        if (other.gameObject.tag is not ("Wall" or "Player" or "AllyBullet" or "EnemyBullet" or "Ennemy" or "Boss"
            or "Obstacle")) return;
        NetworkServer.Destroy(this.gameObject);
    }

    public void Setup(int attack, int shotspeed, float attackrange, Vector2 direction)
    {
        this.attack = attack;
        this.shotspeed = shotspeed;
        this.attackrange = attackrange;
        this.direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<Rigidbody2D>();
    }
    
    
    void MoveProjectile()
    {
        
        fire.velocity = (direction * shotspeed * Time.deltaTime);
        this.attackrange -= Time.deltaTime;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isServer) return;
        MoveProjectile();
        if (attackrange <= 0)
        {
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
