using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float speed;
	Rigidbody2D rigidbody;

	void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		rigidbody.velocity = new Vector2(horizontal * speed, vertical * speed);
	}
}
