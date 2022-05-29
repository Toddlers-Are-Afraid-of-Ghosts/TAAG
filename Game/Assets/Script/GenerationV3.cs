using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;
using Unity.Mathematics;

public class GenerationV3 : MonoBehaviour
{
    public static RoomsProperties[,] Spawn(int size)
    {
        RoomsProperties[,] grid = CreateGrid(size);
        int[][] special = PlaceDeadEnds(grid, size);
        PlacePath(grid, size, special);
        OnlyPath(grid, size, special);
        PrintPath(grid, size);
        PlaceSpecial(grid, special);
        PlaceCoordinates(grid, size);
        return grid;
    }

    public static void PrintPath(RoomsProperties[,] grid, int size)
    {
        string str = "";
        int mid = size / 2;
        for (int i = 0; i < size; i++)
        {
            str += "\n";
            for (int j = 0; j < size; j++)
            {
                if (i == mid && j == mid)
                {
                    str += "=";
                }
                else if (grid[i, j].IsSpecial)
                {
                    str += "~";
                }
                else if (grid[i, j].IsPath)
                {
                    str += "+";
                }
                else
                {
                    str += "-";
                }
            }
        }

        Debug.Log(str);
        Console.WriteLine(str);
    }

    public static RoomsProperties[,] CreateGrid(int size)
    {
        RoomsProperties[,] grid = new RoomsProperties[size, size];
        for (int i = 0; i < size * size; i++)
        {
            grid[i % size, i / size] = new RoomsProperties(false, false, false, false);
        }

        ;
        Debug.Log(grid[0, 0].Top);
        return grid;
    }

    public static int[][] PlaceDeadEnds(RoomsProperties[,] grid, int size)
    {
        Random rng = new Random();
        int middle = size / 2;
        int[] topLeft = {rng.Next(0, middle - 2), rng.Next(middle + 2, size)};
        int[] topRight = {rng.Next(middle + 2, size), rng.Next(middle + 2, size)};
        int[] bottomLeft = {rng.Next(0, middle - 2), rng.Next(0, middle - 2)};
        int[] bottomRight = {rng.Next(middle + 2, size), rng.Next(0, middle - 2)};

        int[][] special = {topLeft, topRight, bottomLeft, bottomRight};

        grid[topLeft[0], topLeft[1]].IsSpecial = true;

        grid[topRight[0], topRight[1]].IsSpecial = true;

        grid[bottomLeft[0], bottomLeft[1]].IsSpecial = true;

        grid[bottomRight[0], bottomRight[1]].IsSpecial = true;

        return special;
    }

    public static void PlacePath(RoomsProperties[,] grid, int size, int[][] specialRooms)
    {
        Random rng = new Random();
        int middle = size / 2;
        foreach (int[] special in specialRooms)
        {
            int x = special[0];
            int y = special[1];
            while (x != middle || y != middle)
            {
                if (x == middle)
                {
                    if (y > middle)
                    {
                        y--;
                    }
                    else
                    {
                        y++;
                    }
                }
                else if (y == middle)
                {
                    if (x > middle)
                    {
                        x--;
                    }
                    else
                    {
                        x++;
                    }
                }
                else
                {
                    int rdm = rng.Next(0, 2);
                    if (rdm == 1)
                    {
                        if (x > middle)
                        {
                            x--;
                        }
                        else
                        {
                            x++;
                        }
                    }
                    else
                    {
                        if (y > middle)
                        {
                            y--;
                        }
                        else
                        {
                            y++;
                        }
                    }
                }

                grid[x, y].IsPath = true;
            }
        }
    }

    public static void OnlyPath(RoomsProperties[,] grid, int size, int[][] specialRooms)
    {
        int mid = size / 2;
        grid[mid, mid].IsStart = true;
        grid[mid, mid].HasBeenEntered = true;
        grid[mid, mid].IsPLayerIn = true;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y].IsPath || grid[x, y].IsSpecial || grid[x, y].IsStart)
                {
                    if (y + 1 < size)
                    {
                        if (grid[x, y + 1].IsPath || grid[x, y + 1].IsSpecial || grid[x, y + 1].IsStart)
                        {
                            grid[x, y].Top = true;
                        }
                    }

                    if (y - 1 >= 0)
                    {
                        if (grid[x, y - 1].IsPath || grid[x, y - 1].IsSpecial || grid[x, y - 1].IsStart)
                        {
                            grid[x, y].Bottom = true;
                        }
                    }

                    if (x + 1 < size)
                    {
                        if (grid[x + 1, y].IsPath || grid[x + 1, y].IsSpecial || grid[x + 1, y].IsStart)
                        {
                            grid[x, y].Right = true;
                        }
                    }

                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, y].IsPath || grid[x - 1, y].IsSpecial || grid[x - 1, y].IsStart)
                        {
                            grid[x, y].Left = true;
                        }
                    }
                }
            }
        }
    }

    public static void PlaceSpecial(RoomsProperties[,] grid, int[][] special)
    {
        Random rng = new Random();
        List<int[]> list = new List<int[]>();
        foreach (int[] spe in special)
        {
            list.Add(spe);
        }

        int index = rng.Next(0, list.Count);
        grid[list[index][0], list[index][1]].IsBoss = true;
        list.RemoveAt(index);
        index = rng.Next(0, list.Count);
        grid[list[index][0], list[index][1]].IsItems = true;
        list.RemoveAt(index);
        index = rng.Next(0, list.Count);
        grid[list[index][0], list[index][1]].IsShop = true;
        list.RemoveAt(index);
        index = rng.Next(0, list.Count);
        grid[list[index][0], list[index][1]].IsDeadEnd = true;
        list.RemoveAt(index);
    }

    public static void PlaceCoordinates(RoomsProperties[,] grid, int size)
    {
        int middle = size / 2;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (grid[x, y].IsPath || grid[x, y].IsSpecial || grid[x, y].IsStart)
                {
                    grid[x, y].X = (x - middle) * 39;
                    grid[x, y].Y = (y - middle) * 19;
                }
            }
        }
    }

    public static void ChooseRoom(RoomsProperties[,] grid, int x, int y, List<RoomsProperties>[] roomArray)
    {
        if (grid[x, y].IsSpecial)
        {
            if (grid[x, y].Top)
            {
                if (grid[x, y].IsItems)
                {
                    grid[x, y].Room = roomArray[15][0].Room;
                }
                else if (grid[x, y].IsShop)
                {
                    grid[x, y].Room = roomArray[15][1].Room;
                }
                else if (grid[x, y].IsBoss)
                {
                    grid[x, y].Room = roomArray[15][2].Room;
                }
                else if (grid[x, y].IsDeadEnd)
                {
                    grid[x, y].Room = roomArray[15][3].Room;
                }
            }

            else if (grid[x, y].Bottom)
            {
                if (grid[x, y].IsItems)
                {
                    grid[x, y].Room = roomArray[16][0].Room;
                }
                else if (grid[x, y].IsShop)
                {
                    grid[x, y].Room = roomArray[16][1].Room;
                }
                else if (grid[x, y].IsBoss)
                {
                    grid[x, y].Room = roomArray[16][2].Room;
                }
                else if (grid[x, y].IsDeadEnd)
                {
                    grid[x, y].Room = roomArray[16][3].Room;
                }
            }

            else if (grid[x, y].Left)
            {
                if (grid[x, y].IsItems)
                {
                    grid[x, y].Room = roomArray[17][0].Room;
                }
                else if (grid[x, y].IsShop)
                {
                    grid[x, y].Room = roomArray[17][1].Room;
                }
                else if (grid[x, y].IsBoss)
                {
                    grid[x, y].Room = roomArray[17][2].Room;
                }
                else if (grid[x, y].IsDeadEnd)
                {
                    grid[x, y].Room = roomArray[17][3].Room;
                }
            }

            else if (grid[x, y].Right)
            {
                if (grid[x, y].IsItems)
                {
                    grid[x, y].Room = roomArray[18][0].Room;
                }
                else if (grid[x, y].IsShop)
                {
                    grid[x, y].Room = roomArray[18][1].Room;
                }
                else if (grid[x, y].IsBoss)
                {
                    grid[x, y].Room = roomArray[18][2].Room;
                }
                else if (grid[x, y].IsDeadEnd)
                {
                    grid[x, y].Room = roomArray[18][3].Room;
                }
            }
        }
        else if (grid[x, y].IsPath || grid[x, y].IsStart)
        {
            RoomsProperties g = grid[x, y];
            Random rng = new Random();
            switch (g.Top, g.Bottom, g.Left, g.Right)
            {
                case (true, false, false, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[0][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[0][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, true, false, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[1][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[1][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, false, true, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[2][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[2][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, false, false, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[3][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[3][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, true, false, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[4][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[4][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, false, true, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[5][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[5][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, false, true, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[6][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[6][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, false, false, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[7][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[7][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, true, true, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[8][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[8][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, true, false, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[9][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[9][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, false, true, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[10][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[10][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, true, true, false):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[11][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[11][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, true, false, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[12][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[12][rng.Next(0, 5)].Room;
                    }

                    break;
                case (false, true, true, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[13][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[13][rng.Next(0, 5)].Room;
                    }

                    break;
                case (true, true, true, true):
                    if (g.IsStart)
                    {
                        grid[x, y].Room = roomArray[19][0].Room;
                    }
                    else
                    {
                        grid[x, y].Room = roomArray[19][rng.Next(0, 5)].Room;
                    }

                    break;
            }
        }
    }

    public static RoomsProperties[,] PlaceRooms(RoomsProperties[,] grid, int size, List<RoomsProperties>[] roomArray)
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                ChooseRoom(grid, x, y, roomArray);
                if (grid[x, y].Room != null)
                {
                    var here = grid[x, y];
                    var hare = grid[x, y].Room.name;
                    Vector3 coordinates = new Vector3(grid[x, y].X, grid[x, y].Y, 0);
                    var gameObject = Instantiate(grid[x, y].Room, coordinates, quaternion.identity);
                    foreach (Transform t in gameObject.transform)
                    {
                        if (t.gameObject.CompareTag("SpawnPoint"))
                        {
                            
                            t.gameObject.GetComponent<SpawnSpointProperties>().Setup(x,y);
                            grid[x, y].spawnPoin.Add(t.gameObject);
                            
                            
                        }
                        
                    }
                }
            }
        }

        return grid;
    }
}