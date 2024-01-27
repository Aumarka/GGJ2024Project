using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    GameDirector gameDirector;

    private int pillowCount;
    private int blanketCount;

    public GameObject madeBlanket;
    public GameObject unmadeBlanket;
    public GameObject pillow1;
    public GameObject pillow2;

    private void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        pillowCount = 0;
        blanketCount = 0;

        madeBlanket.SetActive(false);
        pillow1.SetActive(false);
        pillow2.SetActive(false);


        unmadeBlanket.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pillow"))
        {
            pillowCount++;

            if(pillowCount >= 1)
            {
                pillow1.SetActive(true);
            }

            if(pillowCount >= 2)
            {
                pillow2.SetActive(true);
            }

            CheckCompletionStatus();
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Blanket"))
        {
            blanketCount++;

            if(blanketCount >= 1)
            {
                madeBlanket.SetActive(true);
                unmadeBlanket.SetActive(false);
            }

            CheckCompletionStatus();
            Destroy(other.gameObject);
        }
    }

    private void CheckCompletionStatus()
    {
        if(pillowCount >= 2 && blanketCount >= 1)
        {
            gameDirector.CompleteTask(17);
        }
    }
}
