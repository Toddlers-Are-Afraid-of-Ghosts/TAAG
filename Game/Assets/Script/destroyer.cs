using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spawnpoint"))
        {
            Destroy(other.gameObject);
        }
        // fonction qui permet de detruire les salles qui vont vouloir se placer au dessus du spawn
        // car le spawn ne possede pas de spawn point et ne rentre pas dans la fonction habituelle
        
    }
}
