using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patrol : Enemy 
{

    public Transform moveSpots;
    public GameObject Player;
    
    private float waitTime;
    private bool wallhit = false;
   
    public float maxX, minX, maxY, minY, chaseRange, startWaitTime;
    Enemy enemy;

    public void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Wall")
        {
            moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            wallhit = true;
           
        }

    }

    void Start()
    {

        enemy = new Enemy();
        waitTime = Random.Range(0, startWaitTime);
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {


        if (DistanceToPlayer() <= chaseRange && !wallhit)
        {

            Chase();
        }
        else
        {
            Patro();


        }

    }
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, enemy.Speed * Time.deltaTime);

    }
    private void Patro()
    {

        Move();
        
        if (Vector2.Distance(transform.position, moveSpots.position) <= 0.2f)
        {
            wallhit = false;
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
    private float DistanceToPlayer()
    {
        return Vector2.Distance(Player.transform.position, this.transform.position);
    }
    private void Chase()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            moveSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
        }
    }


}
