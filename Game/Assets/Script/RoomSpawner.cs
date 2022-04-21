using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private List<RoomsProperties>[] roomArray = RoomTemplates.RoomArray;
    void Start()
    {
        List<List<RoomsProperties>> floorGrid = ImprovedLevelGeneratoion.GenerateGrid(12);
        ImprovedLevelGeneratoion.GenerateFloorLayout(floorGrid, roomArray);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
