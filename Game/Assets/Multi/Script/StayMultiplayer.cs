using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class StayMultiplayer : EnemyMultiplayer
{
    public GameObject player;
    public Transform moveSpots;
    private Transform spot;
    private Transform cam;
    public GameObject bullet;
    private float stayedx, stayedy;
    private List<GameObject> allenemi;
    private GameManagerMultiplayer gameManager;
    private float spawntime;
    public Animator animator; //objet animation
    public float minX, minY, maxX, maxY;

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "AllyBullet":
            {
                var compt = other.gameObject.GetComponent<AllyBulletMultiplayer>();
                this.health -= compt.Attack;
                break;
            }
        }
    }

    void Start()
    {
        spawntime = 5;
        animator = GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        var pl = GameObject.FindGameObjectsWithTag("Player");
        player = pl[Random.Range(0, pl.Length - 1)];
        stayedx = 5;
        stayedy = 0;
        spot.position = new Vector3(player.transform.position.x + stayedx, player.transform.position.y + stayedy);

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManagerMultiplayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isServer) return;
        if (GoodCo())
        {
            if (OnPoint())
            {
                if (DistanceToPlayer(spot))
                {
                    stayedx = Random.Range(-8, 8);
                    stayedy = Random.Range(-8, 8);


                    spot.position = new Vector3(player.transform.position.x + stayedx,
                        player.transform.position.y + stayedy);
                }

                if (spawntime <= 0)
                {
                    Spawn();
                }
                else
                    spawntime -= Time.deltaTime;
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

        if (health <= 0)
        {
            // ManagerSfx.PlaySound("ghost");
            NetworkServer.Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);

        //animation
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Speed", transform.position.magnitude);
    }

    private bool DistanceToPlayer(Transform obj2, float distance = 5)
    {
        var pl = GameObject.FindGameObjectsWithTag("Player");
        foreach (var VARIABLE in pl)
        {
            if (Vector2.Distance(VARIABLE.transform.position, obj2.position) < distance)
                return true;
        }

        return false;
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
               !(DistanceToPlayer(spot, 7));
    }


    void Spawn()
    {
        spawntime = 5;
        gameManager.Enemy();
    }

    new void Attack()
    {
        actualcooldown = cooldown;
        var bullet1 = Instantiate(bullet, transform.position + Vector3.up, Quaternion.identity);
        NetworkServer.Spawn(bullet1);
        var bullet2 = Instantiate(bullet, transform.position + Vector3.down * 2, Quaternion.identity);
        NetworkServer.Spawn(bullet2);
        var bullet3 = Instantiate(bullet, transform.position + Vector3.left, Quaternion.identity);
        NetworkServer.Spawn(bullet3);
        var bullet4 = Instantiate(bullet, transform.position + Vector3.right, Quaternion.identity);
        NetworkServer.Spawn(bullet4);
        bullet1.GetComponent<EnemyBulletMultiplayer>().Setup(attack, shotspeed, attackrange, Vector2.up);
        bullet2.GetComponent<EnemyBulletMultiplayer>().Setup(attack, shotspeed, attackrange, Vector2.down);
        bullet3.GetComponent<EnemyBulletMultiplayer>().Setup(attack, shotspeed, attackrange, Vector2.left);
        bullet4.GetComponent<EnemyBulletMultiplayer>().Setup(attack, shotspeed, attackrange, Vector2.right);
    }
}