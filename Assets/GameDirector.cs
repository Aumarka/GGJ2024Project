using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameUIManager gameUIManager;

    public List<Task> gameTasks = new List<Task>();
    public int gameTimeInSeconds = 600;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    public void Update()
    {
        gameUIManager.SetTimerText(GetTime());
    }

    string GetTime()
    {
        int minutes = Mathf.FloorToInt(gameTimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(gameTimeInSeconds % 60);

        return $"{minutes}:{seconds.ToString("00")}";
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);

        if(gameTimeInSeconds > 0)
        {
            gameTimeInSeconds--;
            StartCoroutine(Countdown());
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {

    }
}
