using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour {

    public static List<RoomsProperties> ExtractRooms(string path, string roomCode, bool top, bool bottom, bool left, bool right) {
        List<RoomsProperties> roomList = new List<RoomsProperties>();
        for (int i = 0; i <= 4; i++) {
            GameObject room = Resources.Load<GameObject>($"{path}/{roomCode} {i.ToString()}");
            RoomsProperties property = new RoomsProperties(top, bottom, left, right, 0, 0, room);
            roomList.Add(property);
        }

        return roomList;
    }
    // Room lists
    public static List<RoomsProperties> TopRooms = ExtractRooms("Rooms/RoomsTemplate/TopRooms", "T", true, false, false, false);
    public static List<RoomsProperties> BottomRooms = ExtractRooms("Rooms/RoomsTemplate/BottomRooms", "B", false, true, false, false);
    public static List<RoomsProperties> LeftRooms = ExtractRooms("Rooms/RoomsTemplate/LeftRooms", "L", false, false, true, false);
    public static List<RoomsProperties> RightRooms = ExtractRooms("Rooms/RoomsTemplate/RightRooms", "R", false, false, false, true);
    public static List<RoomsProperties> TopBottomRooms = ExtractRooms("Rooms/RoomsTemplate/TopBottomRooms", "TB", true, true, false, false);
    public static List<RoomsProperties> LeftRightRooms = ExtractRooms("Rooms/RoomsTemplate/LeftRightRooms", "LR", false, false, true, true);
    public static List<RoomsProperties> TopLeftRooms = ExtractRooms("Rooms/RoomsTemplate/TopLeftRooms", "TL", true, false, true, false);
    public static List<RoomsProperties> TopRightRooms = ExtractRooms("Rooms/RoomsTemplate/TopRightRooms", "TR", true, false, false, true);
    public static List<RoomsProperties> BottomLeftRooms = ExtractRooms("Rooms/RoomsTemplate/BottomLeftRooms", "BL", false, true, true, false);
    public static List<RoomsProperties> BottomRightRooms = ExtractRooms("Rooms/RoomsTemplate/BottomRightRooms", "BR", false, true, false, true);


}
