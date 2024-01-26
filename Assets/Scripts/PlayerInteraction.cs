using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform Camera;
    public float InteractRange;

    public Item EquippedItem;
    public Transform EquipPoint;

    public float ChargeTime, MaxChargeTime;
    public float YeetForce;

    bool IsPickingUpItem = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EquippedItem)
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractRange))
            {
                if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Baby"))
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
}