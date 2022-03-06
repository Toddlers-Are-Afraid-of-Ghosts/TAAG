// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

//ne fonctionne pas


// public class Projectile : MonoBehaviour
// {
//     protected int attack;
//     protected int shotspeed;
//     protected int attackrange;
//     protected int axisx;
//     protected int axisy;
//     protected string direction;

//     Rigidbody2D fire;
//     void Start()
//     {
//         fire=GetComponent<Rigidbody2D>();
//     }

//     public Projectile(int attack, int shotspeed, int attackrange, int x, int y, string direction)
//     {
//         this.attack=attack;
//         this.shotspeed=shotspeed;
//         this.attackrange=attackrange;
//         this.axisx=x;
//         this.axisy=y;
//         this.direction=direction;
//     }

//     void MoveProjectile()
//     {
//         float firehorizontal = Input.GetAxis("FireHorizontal");
//         float firevertical = Input.GetAxis("FireVertical") ;
//         Vector2 move = new Vector2(firehorizontal * shotspeed, firevertical * shotspeed);
//         fire.velocity= (move*shotspeed*Time.deltaTime);
//         this.attackrange-=shotspeed*Time.deltaTime;
        
//     }
//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
