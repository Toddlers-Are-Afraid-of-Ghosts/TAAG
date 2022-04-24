using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AdvancedGeneration : MonoBehaviour
{
    public static List<List<RoomsProperties>> CreateGrid(int size) {
        List<List<RoomsProperties>> grid = new List<List<RoomsProperties>>();
        for (int x = 0; x < size; x++) {
            grid.Add(new List<RoomsProperties>());
            for (int y = 0; y < size; y++) {
                grid[x].Add(null);
            }
        }

        return grid;
    }

    public static bool RandomBool() {
        Random rng = new Random();
        if (rng.Next(0, 2) == 1) {
            return true;
        }

        return false;
    }

    public static bool NotOOB(List<List<RoomsProperties>> grid, int x, int y) {
        int size = grid.Count;
        if (x < size && y  < size && x >= 0 && y <= 0) {
            return true;
        }
        return false;
    }

    public static void PlaceRooms(List<List<RoomsProperties>> grid, int x, int y) {
        bool top = RandomBool();
        bool bottom = RandomBool();
        bool left = RandomBool();
        bool right = RandomBool();
        if (grid[x][y] == null) {
            grid[x][y] = new RoomsProperties(top, bottom, left, right, 0, 0, null);
            if (top && NotOOB(grid, x, y+1)) {
                PlaceRooms(grid, x,y+1);
            }
            if (bottom && NotOOB(grid, x, y - 1)) {
                PlaceRooms(grid, x, y - 1);
            }
            if (left && NotOOB(grid, x - 1, y)) {
                PlaceRooms(grid, x - 1, y);
            }
            if (right && NotOOB(grid, x + 1, y)) {
                PlaceRooms(grid, x + 1, y);
            }
        }
        
    }
}
