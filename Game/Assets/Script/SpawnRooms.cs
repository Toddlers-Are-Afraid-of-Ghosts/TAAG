using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<List<RoomsProperties>> emptyFloorGrid = ImprovedLevelGeneratoion.GenerateGrid(12);
        ImprovedLevelGeneratoion.GenerateFloorLayout(emptyFloorGrid);
    }

    void Spawn() {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
