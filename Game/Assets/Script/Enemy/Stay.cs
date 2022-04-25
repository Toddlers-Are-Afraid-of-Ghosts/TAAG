using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : Enemy
{
    public GameObject player;
    public Transform moveSpots;
    private Transform spot;
    private Transform cam;
    public GameObject bullet;
    private float stayedx, stayedy;

    public float minX, minY, maxX, maxY;

    //Enemy enemy = new Enemy("Spawner", 10, 0, 7, 5, 5, 10, 10);
    // Start is called before the first frame update

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "AllyBullet":
            {
                var compt = other.gameObject.GetComponent<AllyBullet>();
                this.health -= compt.Attack;
                break;
            }
        }
    }

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        player = GameObject.FindWithTag("Player");
        stayedx = 5;
        stayedy = 0;
        spot.position = new Vector3(player.transform.position.x + stayedx, player.transform.position.y + stayedy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GoodCo())
        {
            if (OnPoint())
            {
                if (DistanceToPlayer(player.transform, spot) <= 5)
                {
                    stayedx = Random.Range(-8, 8);
                    stayedy = Random.Range(-8, 8);


                    spot.position = new Vector3(player.transform.position.x + stayedx,
                        player.transform.position.y + stayedy);
                }
            }

            Move();
        }
        else
        {
            stayedx = Random.Range(-8, 8);
            stayedy = Random.Range(-8, 8);
            spot.position = new Vector3(player.transform.position.x + stayedx,
                player.transform.position.y + stayedy);
        }

        if (actualcooldown <= 0)
        {
            Attack();
        }
        else
        {
            actualcooldown -= Time.deltaTime;
        }
    }


    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);
    }

    private float DistanceToPlayer(Transform obj, Transform obj2)
    {
        return Vector2.Distance(obj.position, obj2.position);
        //return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private bool OnPoint()
    {
        if (Vector2.Distance(spot.transform.position, this.transform.position) <= 0.2f)
        {
            return true;
        }

        return false;
    }

    private bool GoodCo()
    {
        return (!(spot.position.x > maxX + cam.position.x) && !(spot.position.x < minX + cam.position.x)) &&
               !(spot.position.y > maxY + cam.position.y) && !(spot.position.y < minY + cam.position.y) &&
               !(DistanceToPlayer(player.transform, spot) <= 7);
    }

    bool Dead()
    {
        return this.health <= 0;
    }

    void Attack()
    {
        actualcooldown = cooldown;
        new EnemyBullet(this.bullet, this.attack, shotspeed, attackrange, transform.position, Vector2.left);
        new EnemyBullet(this.bullet, this.attack, shotspeed, attackrange, transform.position, Vector2.right);
        new EnemyBullet(this.bullet, this.attack, shotspeed, attackrange, transform.position, Vector2.up);
        new EnemyBullet(this.bullet, this.attack, shotspeed, attackrange, transform.position, Vector2.down);
    }
}