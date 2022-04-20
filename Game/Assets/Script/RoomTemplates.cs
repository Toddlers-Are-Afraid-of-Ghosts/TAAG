using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour {
    public static RoomTemplates main;

    void Awake() {
        main = this;
    }

    public GameObject[] RoomsArray;

    void Start() {
        List<List<RoomsProperties>> emptyFloorGrid = ImprovedLevelGeneratoion.GenerateGrid(12);
        ImprovedLevelGeneratoion.GenerateFloorLayout(emptyFloorGrid);
        Spawn(emptyFloorGrid);
    }

    public static GameObject GetRoom(List<List<RoomsProperties>> grid, int x, int y) {
        GameObject GoodRoom = null;
        GameObject[] roomsArray = main.RoomsArray;
        foreach (GameObject room in roomsArray) {
            if (grid[x][y].Top == Properties.Top) {
                if (grid[x][y].Bottom == Properties.Bottom) {
                    if (grid[x][y].Left == Properties.Left) {
                        if (grid[x][y].Right == Properties.Right) {
                            GoodRoom = room;
                        }
                    }
                }
            }
        }

        return GoodRoom;
    }

    void Spawn(List<List<RoomsProperties>> grid) {
        int size = grid.Count;
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Vector3 coordinates = new Vector3(grid[i][j].X, grid[i][j].Y, 0);
                Instantiate(GetRoom(grid, i, j), coordinates, quaternion.identity);
            }
        }
    }
}
