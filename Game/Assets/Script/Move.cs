using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rb;

    
    public int speedbase = 1000;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;
        rb.velocity = new Vector2(horizontal * speedbase, vertical * speedbase);

        if (Input.GetKey(KeyCode.B))
        {
            if (speedbase < 5000)
            {
                speedbase += 100;
            }
        }
        if (Input.GetKey(KeyCode.N))
        {
            if (speedbase > 0)
            {
                speedbase -= 100;
            }
        }
    }
}
