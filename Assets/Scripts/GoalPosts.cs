using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPosts : MonoBehaviour
{
    public bool IsActiveTask = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsActiveTask) return;
        if (!other.CompareTag("Baby")) return;

        Debug.Log("Kobeeeeeeeeeeeeeeeeeeeee");
    }
}