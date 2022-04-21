using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : Enemy
{
    
    GameObject[] spot;
    public GameObject player;
    public GameObject moveSpawn;
    
    int amountToSpawn, pos;

    Transform playerSpots;
    public Turn(GameObject gameObject)
    {
        Instantiate(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        amountToSpawn = 64;

        spot = new GameObject[amountToSpawn];

        playerSpots = player.transform;

        playerSpots.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        CreateMoveAroundPoint(amountToSpawn, playerSpots.position, 3, playerSpots);

        pos = Random.Range(0, amountToSpawn - 1);



    }

    // Update is called once per frame
    void Update()
    {
        TurnCircle();

    }
    public void CreateMoveAroundPoint(int AmountToSpawn, Vector3 point, float radius, Transform parent)
    {

        for (int i = 0; i < AmountToSpawn; i++)
        {

            /* Distance circle */
            var rad = 2 * Mathf.PI / AmountToSpawn * i;

            /* Get the vector direction */
            var vertrical = Mathf.Sin(rad);
            var horizontal = Mathf.Cos(rad);

            var spawnDir = new Vector3(horizontal, vertrical, 0);

            /* Spawn pos */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */

            GameObject move = Instantiate(moveSpawn, spawnPos, Quaternion.identity, parent);
            spot[i] = move;


        }
    }
    private float DistanceToSpot()
    {
        return Vector2.Distance(spot[pos].transform.position, transform.position);
    }
   
    private void TurnCircle()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot[pos].transform.position, this.speed * Time.deltaTime);
        if (DistanceToSpot() < 1)
        {
            pos = (pos + 1) % amountToSpawn;
        }
    }
}




