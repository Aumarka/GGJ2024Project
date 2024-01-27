using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    GameDirector gameDirector;

    private int pillowCount;
    private int blanketCount;

    public GameObject madeBed;
    public GameObject unmadeBed;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        pillowCount = 0;
        blanketCount = 0;

        madeBed.SetActive(false);
        unmadeBed.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pillow"))
        {
            pillowCount++;
            CheckCompletionStatus();
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Blanket"))
        {
            blanketCount++;
            CheckCompletionStatus();
            Destroy(other.gameObject);
        }
    }

    private void CheckCompletionStatus()
    {
        if(pillowCount >= 2 && blanketCount >= 1)
        {
            madeBed.SetActive(true);
            unmadeBed.SetActive(false);

            gameDirector.CompleteTask(17);
        }
    }
}
