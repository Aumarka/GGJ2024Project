using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task List", menuName = "ScriptableObjects/Task List")]
public class TaskList : ScriptableObject
{
    public Task[] tasks;
}
