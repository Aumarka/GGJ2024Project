using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "ScriptableObjects/Task")]
public class Task : ScriptableObject
{
    public int taskID;
    [TextArea] public string taskDescription;
    public enum taskTypes
    {
        Baby,
        Housekeeping
    }

    public taskTypes taskType;

    public int happinessValue;
    
}
