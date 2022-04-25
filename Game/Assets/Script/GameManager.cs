using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int numberOfPlayer;

    void Start()
    {
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayer <= 0)
        {
            var componentGenerator = this.GetComponent<GeneratorEnemi>();
            componentGenerator.CleanEnemy();
            Debug.Log("FINIIIIIIIIII");
        }
    }
}