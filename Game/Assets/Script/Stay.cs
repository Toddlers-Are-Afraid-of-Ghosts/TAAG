using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : MonoBehaviour
{
    public GameObject Player;
    public Transform moveSpots;
    public float speed = 5;
    private float stayedx, stayedy;
    public float minX, minY, maxX, maxY;
    // Start is called before the first frame update
    void Start()
    {
        stayedx = 5;
        stayedy = 0;
        moveSpots.position = new Vector2(Player.transform.position.x + stayedx, Player.transform.position.y + stayedy);
    }

    // Update is called once per frame
    void Update()
    {
        if (GoodCo())
        {
            if (OnPoint())
            {
                if (DistanceToPlayer() <= 5)
                {
                    stayedx = Random.Range(-8, 8);
                    stayedy = Random.Range(-8, 8);


                    moveSpots.position = new Vector2(Player.transform.position.x + stayedx, Player.transform.position.y + stayedy);

                }
            }
            Stayed();
        }
        else
        {
            stayedx = Random.Range(-8, 8);
            stayedy = Random.Range(-8, 8);
            moveSpots.position = new Vector2(Player.transform.position.x + stayedx, Player.transform.position.y + stayedy);
        }

    }
    private void Move()
    {
       
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed*Time.deltaTime );
    }
    private void Stayed()
    {
        Move();

    }
    private float DistanceToPlayer()
    {
        return Vector2.Distance(Player.transform.position, this.transform.position);
    }
    private bool OnPoint()
    {
        if (Vector2.Distance(moveSpots.transform.position, this.transform.position) <= 0.2f)
        {
            return true;
        }
        return false;
    }
    private bool GoodCo()
    {
        if ((moveSpots.position.x > maxX || moveSpots.position.x < minX) || moveSpots.position.y > maxY || moveSpots.position.y < minY)
        {
            return false;
        }
        return true;
    }
}
