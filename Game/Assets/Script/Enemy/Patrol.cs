using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patrol : Enemy
{
    public Transform moveSpots;
    public GameObject player;
    private Transform spot;
    private float waitTime;
    private bool wallhit = false;
    private Transform cam;

   
    public float maxX, minX, maxY, minY, chaseRange, startWaitTime;
    

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY))+cam.position;
            wallhit = true;
        }
    }
    
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity,cam);
        player = GameObject.FindWithTag("Player");
        waitTime = Random.Range(0, startWaitTime);
        spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY))+cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceToPlayer() <= chaseRange && !wallhit)
        {
            this.Chase();
        }
        else
        {
            this.Patro();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);
    }

    private void Patro()
    {
        this.Move();

        if (Vector2.Distance(transform.position, spot.position) <= 0.2f)
        {
            wallhit = false;
            if (waitTime <= 0)
            {
                spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY))+cam.position;

                waitTime = Random.Range(0, startWaitTime);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private void Chase()
    {
        if (DistanceToPlayer() > 1.3) // evite que l'ennemi soit trop
        {
            this.Move();
            spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }

    bool Dead()
    {
        return this.health <= 0;
    }
}