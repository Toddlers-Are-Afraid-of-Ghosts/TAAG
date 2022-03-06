using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
     Transform[] moveSpots;
    public GameObject Player;
    public GameObject enemyPefab;
    public float speed;
    
    int amountToSpawn;
    int pos;
     Transform playerSpots;
    // Start is called before the first frame update
    void Start()
    {

        pos = 0;
        playerSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        playerSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
        CreateEnemiesAroundPoint(8,playerSpots.position,2);
        
    }
    float DistanceToSpot()
    {
        return Vector2.Distance(moveSpots[pos].transform.position, transform.position);
    }
    public void CreateEnemiesAroundPoint(int num, Vector2 point, float radius)
    {

        for (int i = 0; i < num; i++)
        {

            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertrical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector2(horizontal, vertrical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var enemy = Instantiate(enemyPefab, spawnPos, Quaternion.identity) as GameObject;

            /* Rotate the enemy to face towards player */
            enemy.transform.LookAt(point);

            /* Adjust height */
            enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));
        }
    }
    void TurnCircle()
    {
        playerSpots.position = new Vector2(Player.transform.position.x, Player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[pos].position, speed * Time.deltaTime);
        if (DistanceToSpot() < 0.2f)
        {
            if (pos >= moveSpots.Length - 1)
            {
                pos = 0;
            }
            else
            {
                pos++;
            }
        }
    }
}




