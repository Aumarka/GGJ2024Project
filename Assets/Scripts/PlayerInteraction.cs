using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform Camera;
    public float InteractRange;

    public Transform EquippedItem;
    public Transform EquipPoint;

    public float ChargeTime, MaxChargeTime;
    public float YeetForce;

    bool IsPickingUpItem = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EquippedItem)
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit ray, InteractRange))
            {
                if (ray.collider.CompareTag("Interactable") || ray.collider.CompareTag("Baby"))
                {
                    IsPickingUpItem = true;

                    EquippedItem = ray.collider.transform;

                    EquippedItem.position = EquipPoint.position;
                    EquippedItem.parent = EquipPoint;

                    Rigidbody rb = EquippedItem.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    EquippedItem.GetComponent<Collider>().enabled = false;

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

            Rigidbody rb = EquippedItem.GetComponent<Rigidbody>();
            rb.AddForce(YeetForce * ChargeTime * Camera.transform.forward);
            rb.useGravity = true;

            EquippedItem.parent = null;

            EquippedItem.GetComponent<Collider>().enabled = true;

            EquippedItem = null;
            ChargeTime = 0;
        }
    }
}