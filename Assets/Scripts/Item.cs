using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName = "undefined";

    public Rigidbody Rb;
    public Collider Collider;

    public Chair CurrentChair;
    public bool IsSitting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void PickUp(Transform equipPoint, Transform camera)
    {
        transform.position = equipPoint.position;
        transform.parent = equipPoint;
        transform.eulerAngles = Vector3.up;
        transform.LookAt(camera);

        Rb.useGravity = false;
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
        
        Collider.enabled = false;

        IsSitting = false;
    }

    public virtual void Yeet(Vector3 force, Transform launchPoint)
    {
        transform.position = launchPoint.position;

        Rb.AddForce(force);
        Rb.useGravity = true;

        transform.parent = null;

        Collider.enabled = true;
    }

    public virtual void Interact()
    {
        // Add code in child classes for interactions
        Debug.Log("Interaction Triggered");
    }

    public void SitDown(Chair chair, Transform snapPoint)
    {
        CurrentChair = chair;
        IsSitting = true;

        transform.SetPositionAndRotation(snapPoint.position, snapPoint.rotation);

        Rb.useGravity = false;
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
    }
}