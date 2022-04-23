using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = System.Random;

public class RoomTemplates : MonoBehaviour {

    public static List<RoomsProperties> ExtractRooms(string path, string roomCode, bool top, bool bottom, bool left,
        bool right) {
        List<RoomsProperties> roomList = new List<RoomsProperties>();
        for (int i = 0; i <= 4; i++) {
            string mypath = $"{path}/{roomCode}{i}";
            GameObject prefab = Resources.Load<GameObject>(mypath);
            //GameObject prefab = Resources.Load($"Rooms/RoomsTemplate/{path}/{roomCode}{i}") as GameObject;
            GameObject room = Instantiate(prefab);
            GameObject.Destroy(room);
            
            //GameObject room = Instantiate(Resources.Load<GameObject>($"{path}/{roomCode}{i.ToString()}.prefab"));
            RoomsProperties property = new RoomsProperties(top, bottom, left, right, 0, 0, room);
            roomList.Add(property);
        }

        return roomList;
    }

    public static RoomsProperties ChooseRoom(bool top, bool bottom, bool left, bool right,
        List<RoomsProperties>[] roomArray, int x, int y) {
        RoomsProperties returnedRoom = null;
        Random rng = new Random();
        foreach (List<RoomsProperties> folder in roomArray) {
            RoomsProperties f = folder[0];
            if (f.Top == top && f.Bottom == bottom && f.Left == left && f.Right == right) {
                returnedRoom = folder[rng.Next(0, 5)];
                returnedRoom.X = x;
                returnedRoom.Y = y;
                break;
            }
        }

        return returnedRoom;
    }

    public static List<RoomsProperties> TopRooms;
    public static List<RoomsProperties> BottomRooms;
    public static List<RoomsProperties> LeftRooms;
    public static List<RoomsProperties> RightRooms;
    public static List<RoomsProperties> TopBottomRooms;
    public static List<RoomsProperties> LeftRightRooms;
    public static List<RoomsProperties> TopLeftRooms;
    public static List<RoomsProperties> TopRightRooms;
    public static List<RoomsProperties> BottomLeftRooms;
    public static List<RoomsProperties> BottomRightRooms;
    public static List<RoomsProperties> StartRoom;
    public static List<RoomsProperties> ClosedRoom;
    public static List<RoomsProperties>[] RoomArray = {
        TopRooms, BottomRooms, LeftRooms, RightRooms,
        TopBottomRooms, LeftRightRooms, TopLeftRooms,
        TopRightRooms, BottomLeftRooms, BottomRightRooms,
        StartRoom, ClosedRoom
    };

    void Awake() {
        List<RoomsProperties> TopRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopRooms", "T", true, false, false, false);

        List<RoomsProperties> BottomRooms =
            ExtractRooms("Rooms/RoomsTemplate/BottomRooms", "B", false, true, false, false);

        List<RoomsProperties> LeftRooms =
            ExtractRooms("Rooms/RoomsTemplate/LeftRooms", "L", false, false, true, false);

        List<RoomsProperties> RightRooms =
            ExtractRooms("Rooms/RoomsTemplate/RightRooms", "R", false, false, false, true);

        List<RoomsProperties> TopBottomRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopBottomRooms", "TB", true, true, false, false);

        List<RoomsProperties> LeftRightRooms =
            ExtractRooms("Rooms/RoomsTemplate/LeftRightRooms", "LR", false, false, true, true);

        List<RoomsProperties> TopLeftRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopLeftRooms", "TL", true, false, true, false);

        List<RoomsProperties> TopRightRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopRightRooms", "TR", true, false, false, true);

        List<RoomsProperties> BottomLeftRooms =
            ExtractRooms("Rooms/RoomsTemplate/BottomLeftRooms", "BL", false, true, true, false);

        List<RoomsProperties> BottomRightRooms =
            ExtractRooms("Rooms/RoomsTemplate/BottomRightRooms", "BR", false, true, false, true);

        List<RoomsProperties> StartRoom = new List<RoomsProperties>();
        List<RoomsProperties> ClosedRoom = new List<RoomsProperties>();

        StartRoom.Add(new RoomsProperties(true, true, true, true, 0, 0,
            Resources.Load<GameObject>("Rooms/RoomsTemplate/TBLR")));

        ClosedRoom.Add(new RoomsProperties(true, true, true, true, 0, 0,
            Resources.Load<GameObject>("Rooms/RoomsTemplate/C")));
        
        List<RoomsProperties>[] RoomArray = {
            TopRooms, BottomRooms, LeftRooms, RightRooms,
            TopBottomRooms, LeftRightRooms, TopLeftRooms,
            TopRightRooms, BottomLeftRooms, BottomRightRooms,
            StartRoom, ClosedRoom
        };

        List<List<RoomsProperties>> floorGrid = ImprovedLevelGeneratoion.GenerateGrid(12);
        Debug.Log(RoomArray[0][0].X);
        ImprovedLevelGeneratoion.GenerateFloorLayout(floorGrid, RoomArray);
        Spawn(floorGrid);
    }
    
    void Spawn(List<List<RoomsProperties>> grid) {
        int size = grid.Count;
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Vector3 coordinates = new Vector3(grid[i][j].X, grid[i][j].Y, 0);
                Instantiate(grid[i][j].Room, coordinates, quaternion.identity);
            }
        }
    }
    
}


