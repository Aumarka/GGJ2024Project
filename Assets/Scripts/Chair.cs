using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform BabySnapPoint, ItemSnapPoint;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Baby")) return;

        other.transform.GetComponent<Baby>().SitDown(this, BabySnapPoint);
    }
}