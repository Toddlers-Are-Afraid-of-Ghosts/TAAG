using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AdvancedGeneration : MonoBehaviour {
    public enum Offset {
        Vertical = 13,
        Horizontal = 27
    }

    public static List<List<RoomsProperties>> CreateGrid(int size) {
        List<List<RoomsProperties>> grid = new List<List<RoomsProperties>>();
        for (int x = 0; x < size; x++) {
            grid.Add(new List<RoomsProperties>());
            for (int y = 0; y < size; y++) {
                grid[x].Add(null);
            }
        }

        Debug.Log("Grid Generated");
        return grid;
    }

    public static bool[] RandomBool(int from) {
        bool[] oppenings = {false, false, false, false};
        oppenings[from] = true;
        Random rng = new Random();
        int nbOfOp = rng.Next(1, 4);
        while (NbTrue(oppenings) <= nbOfOp) {
            if (rng.Next(0, 2) == 1) {
                oppenings[rng.Next(0, 4)] = true;
            }
        }

        return oppenings;
    }

    public static bool NotOOB(List<List<RoomsProperties>> grid, int x, int y) {
        int size = grid.Count;
        if (x < size && y < size && x >= 0 && y >= 0) {
            return true;
        }

        return false;
    }

    public static int NbTrue(bool[] directions) {
        int count = 0;
        foreach (bool b in directions) {
            if (b) {
                count++;
            }
        }

        return count;
    }

    public static void Entrance(List<List<RoomsProperties>> grid, int x, int y) {
        
    }

    public static void PlaceEntrance(List<List<RoomsProperties>> grid) {
        Random rng = new Random();
        int size = grid.Count;
        int mid = size / 2;
        grid[mid][mid] = new RoomsProperties(true, true, true, true);
        if (NotOOB(grid, mid, mid)) {
            Entrance(grid, mid, mid);
        }
    }

    public static void PlaceCoordinates(List<List<RoomsProperties>> grid) {
        int size = grid.Count;
        int sX = size / 2;
        int sY = size / 2;
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (grid[x][y] != null) {
                    grid[x][y].X = (x - sX) * (int) Offset.Horizontal;
                    grid[x][y].Y = (y - sY) * (int) Offset.Vertical;
                }
            }
        }
    }

    public static GameObject ChooseRooms(bool top, bool bottom, bool left, bool right,
        List<RoomsProperties>[] roomArray) {
        GameObject returnedRoom = null;
        Random rng = new Random();
        foreach (List<RoomsProperties> roomList in roomArray) {
            RoomsProperties r = roomList[0];
            if (r.Top == top && r.Bottom == bottom && r.Left == left && r.Right == right) {
                returnedRoom = roomList[rng.Next(0, 5)].Room;
                Debug.Log("Room Choosed");
                break;
            }
        }

        if (returnedRoom == null) {
            Debug.Log($"ChooseRoom: No room matching {top} {bottom} {left} {right}");
        }

        return returnedRoom;
    }

    public static bool IsClosed(List<List<RoomsProperties>> grid, int x, int y) {
        RoomsProperties r = grid[x][y];
        if (r.Top == false && r.Bottom == false && r.Left == false && r.Right == false) {
            return true;
        }

        return false;
    }

    public static void PlaceRooms(List<List<RoomsProperties>> grid, List<RoomsProperties>[] roomArray) {
        int size = grid.Count;
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (grid[x][y] != null) {
                    if (!IsClosed(grid, x, y)) {
                        RoomsProperties r = grid[x][y];
                        grid[x][y].Room = ChooseRooms(r.Top, r.Bottom, r.Left, r.Right, roomArray);
                        Debug.Log("Room placed");
                    }
                }
                
            }
        }
    }

    public static void GenerateLayout(int size, List<RoomsProperties>[] roomArray) {
        List<List<RoomsProperties>> grid = CreateGrid(size);
        PlaceEntrance(grid);
        PlaceCoordinates(grid);
        PlaceRooms(grid, roomArray);
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (grid[x][y] != null) {
                    if (grid[x][y].Room != null) {
                        Vector3 coords = new Vector3(grid[x][y].X, grid[x][y].Y, 0);
                        Instantiate(grid[x][y].Room, coords, Quaternion.identity);
                        Debug.Log($"spawned room at {coords}");
                    }
                }
            }
        }
    }
}