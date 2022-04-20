using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    private GameObject player;
    public Transform moveSpots;
    Enemy enemy = new Enemy("Chaser", 10, 0, 5, 5, 5, 10, 10);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        moveSpots.position = new Vector2(player.transform.position.x, player.transform.position.y);
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
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, enemy.Speed * Time.deltaTime);
    }
    private void Chased()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            moveSpots.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }
}
