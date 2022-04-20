using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private GameObject player;
    public Transform moveSpots;
    Enemy enemy = new Enemy("Chaser", 10, 0, 2, 5, 5, 10, 10);
    bool stop = false;
    private float goInTime;
    private float chaseTime;
    private float waitTime;
    bool charge = false;
    // Start is called before the first frame update
    void Start()
    {

        chaseTime = Random.Range(0, 20);
        waitTime = Random.Range(0, 5);
        goInTime = Random.Range(0, 30);
        player = GameObject.FindWithTag("Player");
        moveSpots.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (charge)
        {
            if (Distance(moveSpots) <= 1.3)
            {
                charge = false;
            }

            Charge();
        }
        else if (chaseTime > 0)
        {
            if (goInTime < 0)
            {
                charge = true;
                moveSpots.position = new Vector2(player.transform.position.x, player.transform.position.y);
                goInTime = Random.Range(0, 30);
            }
            else
            {
                Chased();
                chaseTime -= Time.deltaTime;
                goInTime -= Time.deltaTime;
                // Debug.Log($"ChaseTime: {chaseTime}");
                // Debug.Log("Charge: " + goInTime);
            }

        }
        else
        {
            if (waitTime > 0)
            {
                moveSpots.position = new Vector2(this.transform.position.x, this.transform.position.y);
                waitTime -= Time.deltaTime;
                // Debug.Log($"WaitTime: {waitTime}");
            }
            else
            {
                waitTime = Random.Range(0, 5);
                chaseTime = Random.Range(0, 20);
            }

        }


    }
    private float Distance(Transform obj)
    {
        return Vector2.Distance(obj.position, this.transform.position);
    }
    private void Move(int vitesse)
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, vitesse * Time.deltaTime);
    }
    private void Chased()
    {
        if (Distance(player.transform) > 1.3)
        {
            Move(enemy.Speed);
            moveSpots.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }

    }
    private void Charge()
    {
        if (Distance(player.transform) > 1.3)
        {
            Move(enemy.Speed + 10);
            
        }
    }
}
