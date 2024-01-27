using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBox : MonoBehaviour
{
    GameDirector gameDirector;

    private int toyCount;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        toyCount = 0;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Toy"))
        {
            toyCount++;

            if (toyCount >= 6)
            {
                gameDirector.CompleteTask(15);
            }
        }
    }
}
