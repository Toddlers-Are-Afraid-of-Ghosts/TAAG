using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<List<RoomsProperties>> emptyFloorGrid = ImprovedLevelGeneratoion.GenerateGrid(12);
        ImprovedLevelGeneratoion.GenerateFloorLayout(emptyFloorGrid);
        Spawn(emptyFloorGrid);
    }
    void Spawn(List<List<RoomsProperties>> grid) {
        int size = grid.Count;
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Vector3 coordinates = new Vector3(grid[i][j].X, grid[i][j].Y, 0);
                Instantiate(GetRoom(grid, i, j), coordinates, quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
