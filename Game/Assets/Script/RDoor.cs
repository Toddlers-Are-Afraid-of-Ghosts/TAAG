using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDoor : MonoBehaviour
{
    
    private int[] pos;
    private RoomsProperties[,] grid;
    private int size;

    //public bool isVertical;

    private GameObject HRD;

    private GameObject VRD;

    private List<Enemy> alive;
    
    
    // Start is called before the first frame update
    void Start()
    {
        grid = RoomTemplates.grid;
        size = RoomTemplates.size;
        HRD = Resources.Load<GameObject>("Rooms/HorizontalRDoor");
        VRD = Resources.Load<GameObject>("Rooms/VerticalRDoor");
    }

    // Update is called once per frame
    void Update() {
        int middle = size / 2;
        pos = Vdoor.pos;
        alive = GeneratorEnemi.alive;
        if (!grid[pos[0], pos[1]].DoorEntered && grid[pos[0], pos[1]].IsPLayerIn) {
            if (grid[pos[0], pos[1]].Right) {
                Vector3 Rcoords = new Vector3((pos[0] - middle) * 39 + (float) 19.5, (pos[1] - middle) * 19, 0);
                    Instantiate(VRD, Rcoords, Quaternion.identity);
            }
            if (grid[pos[0], pos[1]].Left) {
                Vector3 Lcoords = new Vector3((pos[0] - middle) * 39 - (float) 19.5, (pos[1] - middle) * 19, 0);
                Instantiate(VRD, Lcoords, Quaternion.identity);
            }
            if (grid[pos[0], pos[1]].Top) {
                Vector3 Tcoords = new Vector3((pos[0] - middle) * 39, (pos[1] - middle) * 19 + (float) 9.5, 0);
                Instantiate(HRD, Tcoords, Quaternion.identity);
            }
            if (grid[pos[0], pos[1]].Bottom) {
                 Vector3 Bcoords = new Vector3((pos[0] - middle) * 39, (pos[1] - middle) * 19 - (float) 9.5, 0);
                Instantiate(HRD, Bcoords, Quaternion.identity);
            }

            grid[pos[0], pos[1]].DoorEntered = true;

        }
        
    }
}
