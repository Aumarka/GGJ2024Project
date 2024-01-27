using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Item
{
    public bool IsHungry = false, NeedsChanging = false;

    public float MinRandomEventTimer = 60, MaxRandomEventTimer = 90;


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

    private void OnCollisionEnter(Collision other)
    {
        if (!IsSitting) return;

        if (other.transform.CompareTag("Baby Food") && IsHungry)
        {
            SitDown(CurrentChair, CurrentChair.BabySnapPoint);
            other.transform.GetComponent<Item>().SitDown(CurrentChair, CurrentChair.ItemSnapPoint);

            // Decrease cry multiplier

            IsHungry = false;

            float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
            Invoke(nameof(GetHungry), rand);
        }
    }
}