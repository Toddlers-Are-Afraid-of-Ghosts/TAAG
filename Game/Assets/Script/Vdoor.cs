using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class Vdoor : MonoBehaviour {
    public RoomsProperties[,] grid;
    public int size;
    private Vector3 dest;
    private Vector3 playerTP;
    public int movementDirection;
    public Transform mc;
    public Transform player;
    private bool isLerp = false;
    private float Speed = 3f;
    public float Delay = 10f;
    private Vector3 _cam;
    private GameObject HVD;
    private GameObject VVD;

    private void Awake() {
        mc = GameObject.FindGameObjectWithTag("MainCamera").transform;
        grid = RoomTemplates.grid;
        size = RoomTemplates.size;
        HVD = Resources.Load<GameObject>("Rooms/HorizontalRDoor");
        VVD = Resources.Load<GameObject>("Rooms/VerticalRDoor");
        grid = RoomTemplates.grid;
        size = RoomTemplates.size;
    }

    private void Update() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (isLerp) PositionChanging(grid, ref Player.pos[0], ref Player.pos[1]);
    }

    private void PositionChanging(RoomsProperties[,] grid, ref int x, ref int y) {
        var currX = x;
        var currY = y;
        switch (movementDirection) {
            case 1:
                //bottom

                mc.transform.position += new Vector3(0, (float) -19, 0);

                player.transform.position += Vector3.up;
                player.transform.position += Vector3.right * 3;
                player.transform.position += Vector3.down * 4;
                player.transform.position += Vector3.left * 3;
                isLerp = false;

                y--;
                break;
            case 2:
                //top
                mc.transform.position += new Vector3(0, (float) 19, 0);

                player.transform.position += Vector3.down;
                player.transform.position += Vector3.right * 3;
                player.transform.position += Vector3.up * 4;
                player.transform.position += Vector3.left * 3;
                isLerp = false;
                y++;
                break;
            case 3:
                //left
                mc.transform.position += new Vector3((float) -39, 0, 0);

                player.transform.position += Vector3.right;
                player.transform.position += Vector3.up * 3;
                player.transform.position += Vector3.left * 4;
                player.transform.position += Vector3.down * 3;
                isLerp = false;
                x--;
                break;
            case 4:
                //right
                mc.transform.position += new Vector3((float) 39, 0, 0);
                player.transform.position += Vector3.left;
                player.transform.position += Vector3.up * 3;
                player.transform.position += Vector3.right * 4;
                player.transform.position += Vector3.down * 3;
                isLerp = false;
                x++;
                break;
        }

        grid[currX, currY].IsPLayerIn = false;
        grid[x, y].IsPLayerIn = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) isLerp = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) isLerp = false;
    }

    // public static int[] Enter()
    // {
    // }
}