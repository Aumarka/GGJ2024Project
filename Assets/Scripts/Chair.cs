using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform BabySnapPoint, ItemSnapPoint;

    public bool IsTimedChair = false;
    public bool IsOccupied = false;

    public float OccupiedTimer = 0, MaxOccupiedTimer = 5;

    GameDirector _gameDirector;

    public int TaskID;

    void Start()
    {
        _gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
    }

    void Update()
    {
        ChairTimer();
    }

    void ChairTimer()
    {
        if (!IsOccupied) return;
        if (!IsTimedChair) return;

        OccupiedTimer += Time.deltaTime;

        if (OccupiedTimer < MaxOccupiedTimer) return;
        _gameDirector.CompleteTask(TaskID);

        OccupiedTimer = 0;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Baby")) return;

        other.transform.GetComponent<Baby>().SitDown(this, BabySnapPoint);

        IsOccupied = true;
    }
}