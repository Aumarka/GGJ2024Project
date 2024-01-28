using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyInteractable : Item
{
    GameDirector gameDirector;

    public Transform Camera;
    public float InteractRange;

    public AudioClip interactionSound;
    public GameObject interactionEffect;

    public int linkedTaskID;

    private void Start()
    {
        if (Camera == null)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        gameDirector = gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
    }

    public override void Interact()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.gameObject.CompareTag("Baby"))
            {

                Debug.Log($"{itemName} interacted with");

                if (interactionSound != null)
                {
                    SoundManager.instance.PlaySound2(interactionSound);
                }

                if(interactionEffect != null)
                {
                    Instantiate(interactionEffect, transform.position, transform.rotation);
                }

                gameDirector.CompleteTask(linkedTaskID);
            }
        }
    }
}
