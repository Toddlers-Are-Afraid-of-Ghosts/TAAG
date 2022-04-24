using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using Random = System.Random;

public class ImprovedLevelGeneratoion : MonoBehaviour
{
    public static List<List<RoomsProperties>> GenerateGrid(int size, List<RoomsProperties>[] roomArray) {
        List<List<RoomsProperties>> emptyGrid = new List<List<RoomsProperties>>();
        for (int i = 0; i < size; i++) {
            emptyGrid.Add(new List<RoomsProperties>());
            for (int j = 0; j < size; j++) {
                emptyGrid[i].Add(new RoomsProperties(false, false, false, false, 0,0, null));
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
        if (x < size && y < size) {
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
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3], currX, currY + (int) Offset.Top, room);
                    if ((currY + (int) Offset.Top) % 13 == 0) {
                        Debug.Log($"Problem on Top ({currX} , {currY + (int) Offset.Top})");
                    }
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
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3], currX, currY + (int) Offset.Bottom, room);
                    if ((currY + (int) Offset.Bottom) % 13 == 0) {
                        Debug.Log($"Problem on Bottom ({currX} , {currY + (int) Offset.Bottom})");
                    }
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
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3], currX, currY + (int) Offset.Left, room);
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
                        new RoomsProperties(dir[0], dir[1], dir[2], dir[3], currX, currY + (int) Offset.Right, room);
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
        grid[spawnX][spawnY] = new RoomsProperties(true, true, true, true, 0, 0, roomArray[10][0].Room);
        //Instantiate(grid[spawnX][spawnY].Room, Vector3.zero, Quaternion.identity);
        if (Exist(grid, spawnX, spawnY)) {
            NewRooms(grid, spawnX, spawnY, 0, 0, roomArray);
        }
        
    }

    public static (bool, bool[]) CheckSides(List<List<RoomsProperties>> grid, int x, int y, int direction) {
        bool[] directions = {false, false, false, false};
        directions[direction] = true;
        if (Exist(grid, x, y + 1)) {
            if (grid[x][y + 1].Bottom && grid[x][y].Top && directions[]) {
                
            }
        }
    }

    public static void CheckForDoubles(List<List<RoomsProperties>> grid) {
        List<(int, int)> coordinates = new List<(int, int)>();
        int size = grid.Count;
    }
    
}
