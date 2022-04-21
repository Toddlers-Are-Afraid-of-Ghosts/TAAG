using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : Enemy
{
    public GameObject player;
    public Transform moveSpots;
    private Transform spot;

    private float stayedx, stayedy;
    public float minX, minY, maxX, maxY;
    //Enemy enemy = new Enemy("Spawner", 10, 0, 7, 5, 5, 10, 10);
    // Start is called before the first frame update
    public Stay(GameObject gameObject)
    {
        Instantiate(gameObject);
    }
    void Start()
    {
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity);
        player = GameObject.FindWithTag("Player");
        stayedx = 5;
        stayedy = 0;
        spot.position = new Vector2(player.transform.position.x + stayedx, player.transform.position.y + stayedy);
    }

    // Update is called once per frame
    void Update()
    {
        if (GoodCo())
        {
            if (OnPoint())
            {
                if (DistanceToPlayer(player.transform, spot) <= 5)
                {
                    stayedx = Random.Range(-8, 8);
                    stayedy = Random.Range(-8, 8);


                    spot.position = new Vector2(player.transform.position.x + stayedx, player.transform.position.y + stayedy);

                }
            }
            Move();
        }
        else
        {
            stayedx = Random.Range(-8, 8);
            stayedy = Random.Range(-8, 8);
            spot.position = new Vector2(player.transform.position.x + stayedx, player.transform.position.y + stayedy);
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
        if ((spot.position.x > maxX || spot.position.x < minX) || spot.position.y > maxY || spot.position.y < minY 
        || DistanceToPlayer(player.transform, spot) <= 7)
        {
            return false;
        }
        return true;
    }
}
