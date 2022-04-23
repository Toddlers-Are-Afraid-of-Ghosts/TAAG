using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Enemy
{
    public GameObject player;
    private Transform cam;
    public Transform moveSpots;

    private Transform spot;

    // Enemy enemy = new Enemy("Chaser", 10, 0, 5, 5, 5, 10, 10);
    // Start is called before the first frame update
    public Chase(GameObject gameObject)
    {
        var obj = Instantiate(gameObject);
        
    }

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity,cam);
        player = GameObject.FindWithTag("Player");
        spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Chased();
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, this.speed * Time.deltaTime);
    }

    private void Chased()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }
    bool Dead()
    {
        return this.health <= 0;
    }
}