using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TurnMultiplayer : EnemyMultiplayer
{
    GameObject[] spot;
    public GameObject player;
    public GameObject moveSpawn;
    public GameObject bullet;
    int amountToSpawn, pos;
    public Animator animator; //objet animation

    Transform playerSpots;

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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        var pl = GameObject.FindGameObjectsWithTag("Player");
        player = pl[new System.Random().Next(0, pl.Length - 1)];
        amountToSpawn = 64;

        spot = new GameObject[amountToSpawn];

        playerSpots = player.transform;

        playerSpots.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        CreateMoveAroundPoint(amountToSpawn, playerSpots.position, 4, playerSpots);

        pos = Random.Range(0, amountToSpawn - 1);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isServer) return;
        TurnCircle();
        if (actualcooldown <= 0)
            Attack();
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

    public void CreateMoveAroundPoint(int AmountToSpawn, Vector3 point, float radius, Transform parent)
    {
        for (int i = 0; i < AmountToSpawn; i++)
        {
            /* Distance circle */
            var rad = 2 * Mathf.PI / AmountToSpawn * i;

            /* Get the vector direction */
            var vertrical = Mathf.Sin(rad);
            var horizontal = Mathf.Cos(rad);

            var spawnDir = new Vector3(horizontal, vertrical, 0);

            /* Spawn pos */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */

            GameObject move = Instantiate(moveSpawn, spawnPos, Quaternion.identity, parent);
            spot[i] = move;
        }
    }

    private float DistanceToSpot()
    {
        return Vector2.Distance(spot[pos].transform.position, transform.position);
    }

    private void TurnCircle()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, spot[pos].transform.position, this.speed * Time.deltaTime);

        //animation
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Speed", transform.position.magnitude);

        if (DistanceToSpot() < 1)
        {
            pos = (pos + 1) % amountToSpawn;
        }
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