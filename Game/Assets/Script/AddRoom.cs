using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("ROOMS").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
