using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public float destroyTimer = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
}
