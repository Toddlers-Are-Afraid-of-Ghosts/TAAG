using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using Random = System.Random;

public class ImprovedLevelGeneration : MonoBehaviour {

    void Start() {
        
    }
    public static List<List<RoomsProperties>> GenerateGrid(int size, List<RoomsProperties>[] roomArray) {
        List<List<RoomsProperties>> emptyGrid = new List<List<RoomsProperties>>();
        for (int i = 0; i < size; i++) {
            emptyGrid.Add(new List<RoomsProperties>());
            for (int j = 0; j < size; j++) {
                emptyGrid[i].Add(new RoomsProperties(false, false, false, false));
            }
        }
        return emptyGrid;
    }

    public enum Directions {
        Top = 0,
        Bottom = 1,
        Left = 2,
        Right = 3
    }

    public enum Offset {
        Top = 13,
        Bottom = -13,
        Left = -27,
        Right = 27
    }

    public static int NumberOfTrue(bool[] arr) {
        int count = 0;
        for (int i = 0; i < arr.Length; i++) {
            if (arr[i]) {
                count++;
            }
        }

        return count;
    }

    public static bool[] OpeningGenerator(int direction) {
        bool[] oppenings = {false, false, false, false};
        oppenings[direction] = true;
        Random rng = new Random();
        int numberOfOppenings = rng.Next(0,3);
        while (NumberOfTrue(oppenings) < numberOfOppenings ) {
            oppenings[rng.Next(0, 4)] = true;
        }

        return oppenings;
    }

    public static bool Exist(List<List<RoomsProperties>> grid, int x, int y) {
        int size = grid.Count;
        if (x < size && y < size && x > 0 && y > 0) {
            return true;
        }

        return false;
    }

    public static void NewRooms(List<List<RoomsProperties>> grid, int x, int y, int currX, int currY, List<RoomsProperties>[] roomArray) {
        int tempX;
        int tempY;
        if (Exist(grid, x, y + 1)) {
            if (grid[x][y].Top) {
                tempX = x;
                tempY = y + 1;
                if (grid[tempX][tempY].Room == null) {
                    bool[] dir = OpeningGenerator((int) Directions.Bottom);
                    GameObject room = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray);
                    grid[tempX][tempY] = 
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
                    if (NumberOfTrue(dir) > 1) {
                        NewRooms(grid, tempX, tempY, currX, currY + 13, roomArray);
                    }
                }
            }
        }
        if (Exist(grid, x, y - 1)) {
            if (grid[x][y].Bottom) {
                tempX = x;
                tempY = y - 1;
                if (grid[tempX][tempY].Room == null) {
                    bool[] dir = OpeningGenerator((int) Directions.Top);
                    GameObject room = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray);
                    grid[tempX][tempY] = 
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
                    if (NumberOfTrue(dir) > 1) {
                        NewRooms(grid, tempX, tempY, currX, currY - 13, roomArray);
                    }
                }
            }
        }
        if (Exist(grid, x - 1, y)) {
            if (grid[x][y].Left) {
                tempX = x - 1;
                tempY = y;
                if (grid[tempX][tempY].Room == null) {
                    bool[] dir = OpeningGenerator((int) Directions.Right);
                    GameObject room = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray);
                    grid[tempX][tempY] = 
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
                    if (NumberOfTrue(dir) > 1) {
                        NewRooms(grid, tempX, tempY, currX + (int) Offset.Left, currY, roomArray);
                    }
                }
            }
        }
        if (Exist(grid, x + 1, y)) {
            if (grid[x][y].Right) {
                tempX = x + 1;
                tempY = y;
                if (grid[tempX][tempY].Room == null) {
                    bool[] dir = OpeningGenerator((int) Directions.Left);
                    GameObject room = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray);
                    grid[tempX][tempY] = 
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
                    if (NumberOfTrue(dir) > 1) {
                        NewRooms(grid, tempX, tempY, currX  + (int) Offset.Right, currY, roomArray);
                    }
                    
                }
            }
        }
    }


    public static void GenerateFloorLayout(List<List<RoomsProperties>> grid, List<RoomsProperties>[] roomArray) {
        Random rng = new Random();
        int size = grid.Count;
        int spawnX = size / 2+1;
        int spawnY = size / 2+1;
        grid[spawnX][spawnY] = new RoomsProperties(true, true, true, true);
        //Instantiate(grid[spawnX][spawnY].Room, Vector3.zero, Quaternion.identity);
        if (Exist(grid, spawnX, spawnY)) {
            NewRooms(grid, spawnX, spawnY, 0, 0, roomArray);
        }
        
    }

    public static void FillGaps(List<List<RoomsProperties>> grid) {
        int size = grid.Count;
        for (int x = 1; x < size; x++) {
            for (int y = 1; y < size; y++) {
                if (grid[x][y].Room == null) {
                    //Top
                    if (Exist(grid, x, y+1)) {
                        if (grid[x][y + 1].Room != null) {
                            if (grid[x][y + 1].Bottom) {
                                grid[x][y + 1].Top = true;
                            }
                        }
                    }
                    //bottom
                    if (Exist(grid, x, y-1)) {
                        if (grid[x][y - 1].Room != null) {
                            if (grid[x][y - 1].Top) {
                                grid[x][y - 1].Bottom = true;
                            }
                        }
                    }
                    //Left
                    if (Exist(grid, x - 1, y)) {
                        if (grid[x - 1][y].Room != null) {
                            if (grid[x-1][y].Right) {
                                grid[x-1][y].Left = true;
                            }
                        }
                    }
                    //Right
                    if (Exist(grid, x + 1, y)) {
                        if (grid[x + 1][y].Room != null) {
                            if (grid[x+1][y].Left) {
                                grid[x+1][y].Right = true;
                            }
                        }
                    }
                }
            }
        }
    }
    
    public static void CheckForErrors(List<List<RoomsProperties>> grid) {
        List<int> validX = new List<int>();
        List<int> validY = new List<int>();
        int limit = 24;
        for (int i = 0; i < limit; i++) {
            validX.Add(i * 27);
            validX.Add(-i * 27);
            validY.Add(i * 13);
            validY.Add(-i * 13);
        }
        
        List<(int, int)> coordinates = new List<(int, int)>();
        int size = grid.Count;
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (!validX.Contains(grid[x][y].X) || !validY.Contains(grid[x][y].Y)) {
                    grid[x][y].Room = null;
                }
                
                if (coordinates.Contains((grid[x][y].X, grid[x][y].Y)) && (grid[x][y].X,grid[x][y].Y) != (0,0)) {
                    Debug.Log($"Destroyed room {grid[x][y]}");
                    grid[x][y].Room = null;
                }
                else {
                    coordinates.Add((grid[x][y].X, grid[x][y].Y));
                }
            }
        }
    }
    
}
