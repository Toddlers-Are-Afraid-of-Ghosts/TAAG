using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
     GameObject[] moveSpots;
    public GameObject Player;
    public GameObject enemyPefab;
    public float speed;

    int amountToSpawn,pos;
   
    Transform playerSpots;
    // Start is called before the first frame update
    void Start()
    {
        amountToSpawn = 64;

        moveSpots = new GameObject[amountToSpawn];

        playerSpots = Player.transform;

        playerSpots.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);

        CreateMoveAroundPoint(amountToSpawn, playerSpots.position, 3, playerSpots);

        pos = Random.Range(0,amountToSpawn-1);



    }

    // Update is called once per frame
    void Update()
    {
        TurnCircle();

    }
    public void CreateMoveAroundPoint(int num, Vector3 point, float radius, Transform parent)
    {

        for (int i = 0; i < num; i++)
        {

            /* Distance circle */
            var rad = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertrical = Mathf.Sin(rad);
            var horizontal = Mathf.Cos(rad);

            var spawnDir = new Vector3(horizontal, vertrical, 0);

            /* Spawn pos */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */

            GameObject move = Instantiate(enemyPefab, spawnPos, Quaternion.identity, parent);
            moveSpots[i] = move;


        }
    }
    private float DistanceToSpot()
    {
        return Vector2.Distance(moveSpots[pos].transform.position, transform.position);
    }

    private void TurnCircle()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[pos].transform.position, speed * Time.deltaTime);
        if (DistanceToSpot() < 1)
        {
           pos=(pos+1)%amountToSpawn;
        }
    }
}




