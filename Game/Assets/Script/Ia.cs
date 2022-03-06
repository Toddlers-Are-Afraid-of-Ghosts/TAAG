using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Ia : MonoBehaviour
{
    
    public Transform moveSpots;
    public GameObject Player;
    public int speed = 5;
    private float waitTime;
    public float maxX, minX, maxY, minY, chaseRange, startWaitTime;

    void Start()
    {
        
        waitTime = Random.Range(0, startWaitTime);
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {

        if (DistanceToPlayer() <= chaseRange)
        {

            Chase();
        }
        else
        {
            Patrol();

        }
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
    }
    void Patrol()
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
    float DistanceToPlayer()
    {
        return Vector2.Distance(Player.transform.position, this.transform.position);
    }
    void Chase()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            moveSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
        }
    }


}
