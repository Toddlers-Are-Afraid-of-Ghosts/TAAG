using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using Random = System.Random;

public class ImprovedLevelGeneratoion : MonoBehaviour
{
    public static List<List<RoomsProperties>> GenerateGrid(int size) {
        List<List<RoomsProperties>> emptyGrid = new List<List<RoomsProperties>>();
        for (int i = 0; i < size; i++) {
            emptyGrid.Add(new List<RoomsProperties>());
            for (int j = 0; j < size; j++) {
                emptyGrid[i].Add(new RoomsProperties(false, false, false, false, -1,-1, RoomTemplates.ClosedRoom.Room));
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
        //Top
        if (grid[x][y].Top && Exist(grid, x,y+1) && grid[x][y+1].IsClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Bottom);
            grid[x][y + 1] = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray, currX, currY + (int) Offset.Top);
            NewRooms(grid, x, y+1, currX, currY + (int) Offset.Top, roomArray);
        }
        //Bottom
        if (grid[x][y].Bottom && Exist(grid, x,y-1) && grid[x][y-1].IsClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Top);
            grid[x][y - 1] = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray, currX, currY + (int) Offset.Bottom);
            NewRooms(grid, x, y-1, currX, currY + (int) Offset.Bottom, roomArray);
        }
        // Left
        if (grid[x][y].Left && Exist(grid, x-1,y) && grid[x-1][y].IsClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Right);
            grid[x-1][y] = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray, currX, currY + (int) Offset.Left);
            NewRooms(grid, x-1, y, currX + (int) Offset.Left, currY, roomArray);
        }
        //Right
        if (grid[x][y].Right && Exist(grid, x + 1,y) && grid[x+1][y].IsClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Left);
            grid[x+1][y] = RoomTemplates.ChooseRoom(dir[0], dir[1], dir[2], dir[3], roomArray, currX, currY + (int) Offset.Right);
            NewRooms(grid, x+1, y+1, currX + (int) Offset.Right, currY, roomArray);
        }
    }


    public static void GenerateFloorLayout(List<List<RoomsProperties>> grid, List<RoomsProperties>[] roomArray) {
        Random rng = new Random();
        int size = grid.Count;
        int spawnX = size / 2;
        int spawnY = size / 2;

        RoomsProperties start = RoomTemplates.StartRoom;
        start.X = spawnX;
        start.Y = spawnY;
        grid[spawnX][spawnY] = start;
        //Top
        NewRooms(grid, spawnX, spawnY + 1, spawnX, spawnY + (int) Offset.Top, roomArray);
        //Bottom
        NewRooms(grid, spawnX, spawnY - 1, spawnX, spawnY + (int) Offset.Bottom, roomArray);
        //Left
        NewRooms(grid, spawnX - 1, spawnY, spawnX + (int) Offset.Left, spawnY, roomArray);
        //Right
        NewRooms(grid, spawnX + 1, spawnY, spawnX + (int) Offset.Right, spawnY, roomArray);
    }
    
}
