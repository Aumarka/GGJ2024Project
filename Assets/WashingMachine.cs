using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : MonoBehaviour
{
    GameDirector gameDirector;

    private int laundryCount;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        laundryCount = 0;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laundry"))
        {
            laundryCount++;
            Destroy(other.gameObject);

            if (laundryCount >= 4)
            {
                gameDirector.CompleteTask(19);
            }
        }
    }
}
