using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameDirector gameDirector;


    public Transform Camera;
    public float InteractRange;

    public Item EquippedItem;
    public Transform EquipPoint;

    public float ChargeTime, MaxChargeTime;
    public float YeetForce;

    bool IsPickingUpItem = false;

    public string[] interactableTags;


    private void Start()
    {
        if(gameDirector == null)
        {
            gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameDirector)
        {
            gameDirector.gameUIManager.UpdateThrowBar(ChargeTime / MaxChargeTime);
        }

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit preHit, InteractRange))
        {
            if (preHit.collider.gameObject.TryGetComponent<Item>(out Item item))
            {
                if (gameDirector)
                {
                    gameDirector.gameUIManager.SetItemText(item.itemName);
                }
                else
                {
                    Debug.Log(item.itemName);
                }
                
            }
            else
            {
                if (gameDirector)
                {
                    gameDirector.gameUIManager.SetItemText("");
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !EquippedItem)
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractRange))
            {
                if (TagIsInteractable(hit.collider.gameObject.tag))
                {
                    IsPickingUpItem = true;

                    EquippedItem = hit.collider.GetComponent<Item>();

                    EquippedItem.PickUp(EquipPoint, Camera);

                    return;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (EquippedItem)
            {
                ChargeTime += Time.deltaTime;

                if (ChargeTime > MaxChargeTime)
                    ChargeTime = MaxChargeTime;
            } 
        }

        else
            Yeet();
    }

    public void Yeet()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (IsPickingUpItem)
            {
                IsPickingUpItem = false;
                return;
            }

            if (ChargeTime <= 0 || !EquippedItem)
                return;

            EquippedItem.Yeet(YeetForce * ChargeTime * Camera.transform.forward);

            EquippedItem = null;
            ChargeTime = 0;
        }
    }

    public bool TagIsInteractable(string tag)
    {
        for(int i = 0; i < interactableTags.Length; i++)
        {
            if(tag == interactableTags[i])
            {
                return true;
            }
        }

        return false;
    }
}