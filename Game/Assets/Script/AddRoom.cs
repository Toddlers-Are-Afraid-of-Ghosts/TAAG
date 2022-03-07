using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    private void Start()
    {
        // Fonction qui ajoute a une liste toutes les salles que l'on spawn, afin de savoir ou placer la salle du boss
        templates = GameObject.FindGameObjectWithTag("ROOMS").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
