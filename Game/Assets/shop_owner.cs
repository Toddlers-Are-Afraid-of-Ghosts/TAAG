using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class shop_owner : MonoBehaviour
{
    public int sceneID, scenebase;

    private bool Isinrange;
    public float speed, speedsafe;
    public Transform moveSpots;
    public GameObject player;
    private Transform spot;
    private float waitTime;
    private bool wallhit = false;
    private Transform cam;
    public float maxX, minX, maxY, minY, startWaitTime;
    public Animator animator; //objet animation

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
            case "Ennemy":
                spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)) + cam.position;
                wallhit = true;
                break;
        
        }
    }

    void Start()
    {
        animator=GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        spot = Instantiate(moveSpots, this.transform.position, Quaternion.identity, cam);
        player = GameObject.FindWithTag("Player");
        waitTime = Random.Range(0, startWaitTime);
        spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)) + cam.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ouvre scene shop lorsque spacebar est pressée et joueur est dans zone de collision
        if (Isinrange && Input.GetKeyDown(KeyCode.Space) && !wallhit)
        {
            speed=0;
            SceneManager.LoadScene(sceneID);
        }
        else
        {
            speed=speedsafe;
            this.Patro();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, spot.position, speed * Time.deltaTime);

        //animation
        //animator.SetFloat("Horizontal", transform.position.x);
        //animator.SetFloat("Speed", transform.position.magnitude);
    }

    private void Patro()
    {
        this.Move();

        if (Vector2.Distance(transform.position, spot.position) <= 0.2f)
        {
            wallhit = false;
            if (waitTime <= 0)
            {
                spot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)) + cam.position;

                waitTime = Random.Range(0, startWaitTime);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, this.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Isinrange=true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Isinrange=false;
        }

    }



}
