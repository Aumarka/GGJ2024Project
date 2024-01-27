using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    GameDirector gameDirector;

    private int paperCount;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        paperCount = 0;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paper"))
        {
            paperCount++;

            if(paperCount >= 3)
            {
                gameDirector.CompleteTask(20);
            }
        }
    }
}
