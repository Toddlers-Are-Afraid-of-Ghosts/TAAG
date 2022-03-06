using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject Player;
     public Transform moveSpots;
     public float speed =5;
    // Start is called before the first frame update
    void Start()
    {
         moveSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Chased();
    }
    private float DistanceToPlayer()
    {
        return Vector2.Distance(Player.transform.position, this.transform.position);
    }
      private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
    }
    private void Chased()
    {
        if (DistanceToPlayer() > 1.3)
        {
            Move();
            moveSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
        }
    }
}
