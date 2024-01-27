using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Item
{
    public bool IsHungry = false, NeedsChanging = false;
    public bool IsSitting = false;

    public float MinRandomEventTimer = 60, MaxRandomEventTimer = 90;

    public Chair CurrentChair;


    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
        Invoke(nameof(GetHungry), rand);
        rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
        Invoke(nameof(GetNeedsChanging), rand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHungry()
    {
        IsHungry = true;
        // TODO -- Increase cry multiplier
    }

    public void GetNeedsChanging()
    {
        NeedsChanging = true;
        // TODO -- Increase cry multiplier
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

    private void OnTriggerEnter(Collider other)
    {
        if (!IsSitting) return;

        if (other.CompareTag("Baby Food") && IsHungry)
        {
            other.transform.SetPositionAndRotation(CurrentChair.ItemSnapPoint.position, CurrentChair.ItemSnapPoint.rotation);

            // TODO
            // Snap collided object to table
            // Decrease cry multiplier

            IsHungry = false;

            float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
            Invoke(nameof(GetHungry), rand);
        }
    }
}