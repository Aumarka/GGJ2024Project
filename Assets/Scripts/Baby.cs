using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Item
{
    public bool IsHungry = false, NeedsChanging = false;

    public float MinRandomEventTimer = 60, MaxRandomEventTimer = 90;

    GameDirector _gameDirector;


    // Start is called before the first frame update
    void Start()
    {
        _gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();

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

        _gameDirector.ChangeHappinessModifier(true);

        // TODO -- Feedback

        Debug.Log("Time to feed da baby");
    }

    public void GetNeedsChanging()
    {
        NeedsChanging = true;

        _gameDirector.ChangeHappinessModifier(true);

        // TODO -- Feedback

        Debug.Log("Damn you got a stinky diaper");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsSitting) return;

        if (other.transform.CompareTag("Baby Food") && IsHungry && CurrentChair.CompareTag("High Chair"))
        {
            SitDown(CurrentChair, CurrentChair.BabySnapPoint);
            other.transform.GetComponent<Item>().SitDown(CurrentChair, CurrentChair.ItemSnapPoint);

            _gameDirector.ChangeHappinessModifier(false);

            IsHungry = false;

            float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
            Invoke(nameof(GetHungry), rand);

            _gameDirector.CompleteTask(1);

            return;
        }

        if (other.transform.CompareTag("Diaper") && NeedsChanging && CurrentChair.CompareTag("Change Table"))
        {
            SitDown(CurrentChair, CurrentChair.BabySnapPoint);
            other.transform.GetComponent<Item>().SitDown(CurrentChair, CurrentChair.ItemSnapPoint);

            _gameDirector.ChangeHappinessModifier(false);

            NeedsChanging = false;

            float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
            Invoke(nameof(GetNeedsChanging), rand);

            _gameDirector.CompleteTask(3);

            return;
        }
    }
}