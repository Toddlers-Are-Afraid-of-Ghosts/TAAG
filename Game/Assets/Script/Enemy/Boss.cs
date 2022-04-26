using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    private Transform cam;

    private GameObject player;

    public Transform moveSpots;

    public Animator animator; //objet animation

    bool stop = false;
    private Transform spot;
    private float goInTime;
    private float chaseTime;
    private float waitTime;


    bool charge = false;

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "AllyBullet":
            {
                var compt = other.gameObject.GetComponent<AllyBullet>();
                this.health -= compt.Attack;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        chaseTime = Random.Range(0, 20);
        waitTime = Random.Range(0, 5);
        goInTime = Random.Range(0, 30);
        player = GameObject.FindWithTag("Player");
        spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charge)
        {
            if (Distance(spot) <= 1.3)
            {
                charge = false;
                goInTime = Random.Range(0, 30);
                spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
            }
            else
            {
                Charge();
            }
        }
        else if (chaseTime > 0)
        {
            if (goInTime < 0)
            {
                charge = true;
                spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
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
                spot.position = new Vector2(this.transform.position.x, this.transform.position.y);
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
        transform.position = Vector2.MoveTowards(transform.position, spot.position, vitesse * Time.deltaTime);

        //animation
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Speed", vitesse);
    }

    private void Chased()
    {
        if (Distance(player.transform) > 1.3)
        {
            spot.position = new Vector2(player.transform.position.x, player.transform.position.y);
            Move(this.Speed);
        }
    }

    private void Charge()
    {
        if (Distance(spot.transform) > 1.3)
        {
            Move(this.Speed + 10);
        }
    }

    bool Dead()
    {
        return this.health <= 0;
    }
}