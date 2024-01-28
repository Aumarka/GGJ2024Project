using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Item
{
    public bool IsHungry = false, NeedsChanging = false;

    public bool IsAirborne = false;
    public float AirborneTimer = 0, AirborneTimerSuccessLimit = 1;

    public float SpikeSpeedThreshold = 5;
    public Material Material;
    public float AngryTimer = 0, AngryResetLimit = 6;
    public bool IsAngry = false;

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

        Material.DisableKeyword("_EMISSION");
        Material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
    }

    // Update is called once per frame
    void Update()
    {
        FixedSitSpot();

        UpdateAirborneTimer();

        AngryBaby();
    }

    void FixedSitSpot()
    {
        if (!IsSitting) return;
        if (!CurrentChair) return;

        SitDown(CurrentChair, CurrentChair.BabySnapPoint);
    }

    void UpdateAirborneTimer()
    {
        if (!IsAirborne) return;

        AirborneTimer += Time.deltaTime;

        if (AirborneTimer < AirborneTimerSuccessLimit) return;

        _gameDirector.CompleteTask(2);
    }

    void AngryBaby()
    {
        if (!IsAngry) return;

        AngryTimer += Time.deltaTime;

        if (AngryTimer < AngryResetLimit) return;

        IsAngry = false;

        Material.DisableKeyword("_EMISSION");
        Material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
    }

    public override void Yeet(Vector3 force, Transform launchPoint)
    {
        base.Yeet(force, launchPoint);

        IsAirborne = true;
        AirborneTimer = 0;
    }

    public void GetHungry()
    {
        IsHungry = true;

        _gameDirector.ChangeHappinessModifier(true);

        _gameDirector.gameUIManager.ToggleHungerIcon(true);

        Debug.Log("Time to feed da baby");
    }

    public void GetNeedsChanging()
    {
        NeedsChanging = true;

        _gameDirector.ChangeHappinessModifier(true);

        _gameDirector.gameUIManager.ToggleDiaperIcon(true);

        Debug.Log("Damn you got a stinky diaper");
    }

    void SeatedCollisionCheck(Transform other)
    {
        if (!IsSitting) return;

        if (other.CompareTag("Baby Food") && IsHungry && CurrentChair.CompareTag("High Chair"))
        {
            SitDown(CurrentChair, CurrentChair.BabySnapPoint);
            other.GetComponent<Item>().SitDown(CurrentChair, CurrentChair.ItemSnapPoint);

            _gameDirector.ChangeHappinessModifier(false);

            IsHungry = false;

            //float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
            //Invoke(nameof(GetHungry), rand);

            _gameDirector.CompleteTask(1);

            _gameDirector.gameUIManager.ToggleHungerIcon(false);

            return;
        }

        if (other.CompareTag("Diaper") && NeedsChanging && CurrentChair.CompareTag("Change Table"))
        {
            SitDown(CurrentChair, CurrentChair.BabySnapPoint);
            other.GetComponent<Item>().SitDown(CurrentChair, CurrentChair.ItemSnapPoint);

            _gameDirector.ChangeHappinessModifier(false);

            NeedsChanging = false;

            //float rand = Random.Range(MinRandomEventTimer, MaxRandomEventTimer);
            //Invoke(nameof(GetNeedsChanging), rand);

            _gameDirector.CompleteTask(3);

            _gameDirector.gameUIManager.ToggleDiaperIcon(false);

            return;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (IsAirborne)
        {
            IsAirborne = false;
            
            float speed = Rb.velocity.magnitude;
            Debug.Log(speed);
            if (speed >= SpikeSpeedThreshold)
            {
                Material.EnableKeyword("_EMISSION");
                Material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;

                IsAngry = true;
                AngryTimer = 0;
            }
        }

        SeatedCollisionCheck(other.transform);
    }
}