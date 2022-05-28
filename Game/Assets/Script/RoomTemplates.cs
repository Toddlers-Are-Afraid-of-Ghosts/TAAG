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

public class RoomTemplates : MonoBehaviour
{
    public static int size;
    public static RoomsProperties[,] grid;
    public static List<RoomsProperties> ExtractRooms(string path, string roomCode, bool top, bool bottom, bool left,
        bool right)
    {
        List<RoomsProperties> roomList = new List<RoomsProperties>();
        for (int i = 0; i <= 4; i++)
        {
            string mypath = $"{path}/{roomCode}{i}";
            GameObject prefab = Resources.Load<GameObject>(mypath);
            RoomsProperties property = new RoomsProperties(top, bottom, left, right);
            property.Room = prefab;
            roomList.Add(property);
        }

        return roomList;
    }

    public static GameObject ChooseRoom(bool top, bool bottom, bool left, bool right, List<RoomsProperties>[] roomArray)
    {
        GameObject returnedRoom = null;
        Random rng = new Random();
        foreach (List<RoomsProperties> folder in roomArray)
        {
            RoomsProperties f = folder[0];
            if (f.Top == top && f.Bottom == bottom && f.Left == left && f.Right == right)
            {
                returnedRoom = folder[rng.Next(0, 5)].Room;
                break;
            }
        }

        return returnedRoom;
    }

    void Awake()
    {
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

        List<RoomsProperties> StartRooms =
            ExtractRooms("Rooms/RoomsTemplate/StartRooms", "S", true, true, true, true);

        List<RoomsProperties> TopLeftRightRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopLeftRightRooms", "TLR", true, false, true, true);

        List<RoomsProperties> TopBottomLeftRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopBottomLeftRooms", "TBL", true, true, true, false);

        List<RoomsProperties> TopBottomRigthRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopBottomRightRooms", "TBR", true, true, false, true);

        List<RoomsProperties> BottomLeftRightRooms =
            ExtractRooms("Rooms/RoomsTemplate/BottomLeftRightRooms", "BLR", false, true, true, true);

        List<RoomsProperties> TopSpecialRooms =
            ExtractRooms("Rooms/RoomsTemplate/TopSpecialRooms", "TS", true, false, false, false);
        TopSpecialRooms[0].IsItems = true;
        TopSpecialRooms[1].IsShop = true;
        TopSpecialRooms[2].IsBoss = true;
        TopSpecialRooms[3].IsDeadEnd = true;

        List<RoomsProperties> BottomSpecialRooms =
            ExtractRooms("Rooms/RoomsTemplate/BottomSpecialRooms", "BS", false, true, false, false);
        BottomSpecialRooms[0].IsItems = true;
        BottomSpecialRooms[1].IsShop = true;
        BottomSpecialRooms[2].IsBoss = true;
        BottomSpecialRooms[3].IsDeadEnd = true;

        List<RoomsProperties> LeftSpecialRooms =
            ExtractRooms("Rooms/RoomsTemplate/LeftSpecialRooms", "LS", false, false, true, false);
        LeftSpecialRooms[0].IsItems = true;
        LeftSpecialRooms[1].IsShop = true;
        LeftSpecialRooms[2].IsBoss = true;
        LeftSpecialRooms[3].IsDeadEnd = true;

        List<RoomsProperties> RightSpecialRooms =
            ExtractRooms("Rooms/RoomsTemplate/RightSpecialRooms", "RS", false, false, false, true);
        RightSpecialRooms[0].IsItems = true;
        RightSpecialRooms[1].IsShop = true;
        RightSpecialRooms[2].IsBoss = true;
        RightSpecialRooms[3].IsDeadEnd = true;

        List<RoomsProperties> AllRooms =
            ExtractRooms("Rooms/RoomsTemplate/AllRooms", "A", true, true, true, true);

        List<RoomsProperties>[] RoomArray =
        {
            TopRooms, BottomRooms, LeftRooms, RightRooms,
            TopBottomRooms, LeftRightRooms, TopLeftRooms,
            TopRightRooms, BottomLeftRooms, BottomRightRooms,
            TopLeftRightRooms, TopBottomLeftRooms, TopBottomRigthRooms,
            BottomLeftRightRooms, StartRooms, TopSpecialRooms,
            BottomSpecialRooms, LeftSpecialRooms, RightSpecialRooms,
            AllRooms
        };

        // min size = 8, sinon ca nique tout
        size = 7;
        grid = GenerationV3.Spawn(size);
        grid = GenerationV3.PlaceRooms(grid, size, RoomArray);
    }
}