using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishArea : MonoBehaviour
{
    GameDirector gameDirector;

    public Transform rubbishPoint;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RubbishBag"))
        {
            other.gameObject.transform.position = rubbishPoint.position;
            gameDirector.CompleteTask(14);
        }
    }
}
