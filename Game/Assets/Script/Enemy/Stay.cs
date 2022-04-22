using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : Enemy
{
    public GameObject player;
    public Transform moveSpots;
    private Transform spot;
    private Transform cam;

    private float stayedx, stayedy;

    public float minX, minY, maxX, maxY;

    //Enemy enemy = new Enemy("Spawner", 10, 0, 7, 5, 5, 10, 10);
    // Start is called before the first frame update


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
            spot.position = new Vector3(player.transform.position.x + stayedx, player.transform.position.y + stayedy);
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
}