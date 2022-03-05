using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ia : MonoBehaviour
{

    public int speedbase = 10;
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpots;
    public float maxX, minX, maxY, minY;


    void Start()
    {
        waitTime = startWaitTime;
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

    }

    // Update is called once per frame
    void Update()
    {

        Patrol();
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speedbase * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots.position) <= 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

   


}
