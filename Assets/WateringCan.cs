using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Item
{
    GameDirector gameDirector;

    public Transform Camera;
    public float InteractRange;

    public int plantsWatered = 0;

    public List<GameObject> wateredPlants = new List<GameObject>();

    private void Start()
    {
        if (Camera == null)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        gameDirector = gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        plantsWatered = 0;
    }

    public override void Interact()
    {
        Debug.Log("Water");

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.gameObject.CompareTag("Plant") && !wateredPlants.Contains(hit.collider.gameObject))
            {
                plantsWatered++;
                wateredPlants.Add(hit.collider.gameObject);

                if (plantsWatered >= 4)
                {
                    gameDirector.CompleteTask(18);
                }
            }
        }
    }
}
