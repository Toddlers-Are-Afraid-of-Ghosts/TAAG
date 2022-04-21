using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class RoomTemplates : MonoBehaviour {

    public static List<RoomsProperties> ExtractRooms(string path, string roomCode, bool top, bool bottom, bool left,
        bool right) {
        List<RoomsProperties> roomList = new List<RoomsProperties>();
        for (int i = 0; i <= 4; i++) {
            GameObject room = Resources.Load<GameObject>($"{path}/{roomCode} {i.ToString()}.prefab");
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
    public static RoomsProperties StartRoom;
    public static RoomsProperties ClosedRoom;
    public static List<RoomsProperties>[] RoomArray = {
        TopRooms, BottomRooms, LeftRooms, RightRooms,
        TopBottomRooms, LeftRightRooms, TopLeftRooms,
        TopRightRooms, BottomLeftRooms, BottomRightRooms
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

        RoomsProperties StartRoom = new RoomsProperties(true, true, true, true, 0, 0,
            Resources.Load<GameObject>("Rooms/RoomsTemplate/TBLR.prefab"));

        RoomsProperties ClosedRoom = new RoomsProperties(true, true, true, true, 0, 0,
            Resources.Load<GameObject>("Rooms/RoomsTemplate/C.prefab"));
    }
}


