using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using Random = System.Random;

public class ImprovedLevelGeneratoion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<List<RoomsProperties>> emptyFloorGrid = GenerateGrid(12);
        GenerateFloorLayout(emptyFloorGrid);
    }

    public List<List<RoomsProperties>> GenerateGrid(int size) {
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

    public int NumberOfTrue(bool[] arr) {
        int count = 0;
        for (int i = 0; i < arr.Length; i++) {
            if (arr[i]) {
                count++;
            }
        }

        return count;
    }

    public bool[] OpeningGenerator(int direction) {
        bool[] oppenings = {false, false, false, false};
        oppenings[direction] = true;
        Random rng = new Random();
        int numberOfOppenings = rng.Next(0,3);
        while (NumberOfTrue(oppenings) < numberOfOppenings ) {
            oppenings[rng.Next(0, 4)] = true;
        }

        return oppenings;
    }

    public bool Exist(List<List<RoomsProperties>> grid, int x, int y) {
        int size = grid.Count;
        if (x < size && y < size) {
            return true;
        }

        return false;
    }
    
    public void NewRooms(List<List<RoomsProperties>> grid, int x, int y) {
        if (grid[x][y].Top && Exist(grid, x,y+1) && grid[x][y+1].isClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Bottom);
            grid[x][y + 1] = new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
            NewRooms(grid, x, y+1);
        }
        if (grid[x][y].Bottom && Exist(grid, x,y-1) && grid[x][y-1].isClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Top);
            grid[x][y + 1] = new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
            NewRooms(grid, x, y-1);
        }
        if (grid[x][y].Left && Exist(grid, x-1,y) && grid[x-1][y].isClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Right);
            grid[x][y + 1] = new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
            NewRooms(grid, x-1, y);
        }
        if (grid[x][y].Top && Exist(grid, x + 1,y) && grid[x+1][y].isClosed()) {
            bool[] dir = OpeningGenerator((int) Directions.Left);
            grid[x][y + 1] = new RoomsProperties(dir[0], dir[1], dir[2], dir[3]);
            NewRooms(grid, x+1, y+1);
        }
    }


    public void GenerateFloorLayout(List<List<RoomsProperties>> grid) {
        Random rng = new Random();
        int size = grid.Count;
        int spawnX = size / 2;
        int spawnY = size / 2;
        
        grid[spawnX][spawnY] = new RoomsProperties(true, true, true, true);
        NewRooms(grid, spawnX, spawnY + 1);
        NewRooms(grid, spawnX, spawnY - 1);
        NewRooms(grid, spawnX - 1, spawnY);
        NewRooms(grid, spawnX + 1, spawnY);

    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
