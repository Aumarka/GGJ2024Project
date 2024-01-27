using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameUIManager : MonoBehaviour
{
    GameDirector gameDirector;

    // Start is called before the first frame update
    private void Start()
    {
        if (gameDirector == null)
        {
            gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
