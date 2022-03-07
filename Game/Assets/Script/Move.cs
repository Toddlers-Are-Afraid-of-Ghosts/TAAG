using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
     private int speedbase = 40;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical") ;
        Vector2 move = new Vector2(horizontal * speedbase, vertical * speedbase);
        rb.velocity= (move*speedbase*Time.deltaTime);

    
    }
}