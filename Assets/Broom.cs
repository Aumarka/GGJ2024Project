using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Item
{
    GameDirector gameDirector;

    public Transform Camera;
    public float InteractRange;

    public int pilesCleaned = 0;

    private void Start()
    {
        if(Camera == null)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        gameDirector = gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        pilesCleaned = 0;
    }

    public override void Interact()
    {
        Debug.Log("Sweep");

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.gameObject.CompareTag("MacaroniPile"))
            {
                pilesCleaned++;

                if(pilesCleaned >= 3)
                {
                    gameDirector.CompleteTask(13);
                }

                Destroy(hit.collider.gameObject);
            }
        }
    }
}
