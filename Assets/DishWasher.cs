using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishWasher : MonoBehaviour
{
    GameDirector gameDirector;

    private int dishesCounter;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        dishesCounter = 0;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dish"))
        {
            dishesCounter++;
            Destroy(other.gameObject);

            if (dishesCounter >= 5)
            {
                gameDirector.CompleteTask(16);
            }
        }
    }
}
