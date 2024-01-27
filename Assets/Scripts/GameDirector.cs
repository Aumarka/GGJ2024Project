using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public GameUIManager gameUIManager;

    
    public int gameTimeInSeconds = 600;

    [Header("Game Tasks Variables")]
    [SerializeField] private TaskList babyTasks;
    [SerializeField] private TaskList householdTasks;
    public int minNumberOfTasks;
    public int maxNumberOfTasks;
    private int numberOfTotalTasks;
    private int numberOfHouseholdTasks;
    private List<int> completedTasks = new List<int>();
    public List<Task> gameTasks = new List<Task>();

    private void Start()
    {
        StartCoroutine(Countdown());
        GenerateTasks();

        completedTasks = new List<int>();
    }

    public void Update()
    {
        gameUIManager.SetTimerText(GetTime());

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            CompleteTask(13);
        }


        // Checks if all tasks are completed before ending the game
        if(completedTasks.Count == numberOfTotalTasks)
        {
            EndGame();
        }
    }

    // Generates a list of tasks for the player to complete
    public void GenerateTasks()
    {
        numberOfTotalTasks = Random.Range(minNumberOfTasks, maxNumberOfTasks + 1);
        numberOfHouseholdTasks = Random.Range(2, Mathf.FloorToInt(maxNumberOfTasks / 2));

        int taskListIndex = 0;

        for(int i = 0; i < numberOfTotalTasks - numberOfHouseholdTasks; i++)
        {
            while (true)
            {
                Task newTask = babyTasks.tasks[Random.Range(0, babyTasks.tasks.Length)];

                if (!gameTasks.Contains(newTask))
                {
                    gameTasks.Add(newTask);
                    taskListIndex++;
                    break;
                }
            }          
        }

        for(int i = taskListIndex; i < numberOfTotalTasks; i++)
        {
            while (true)
            {
                Task newTask = householdTasks.tasks[Random.Range(0, householdTasks.tasks.Length)];

                if (!gameTasks.Contains(newTask))
                {
                    gameTasks.Add(newTask);
                    taskListIndex++;
                    break;
                }
            }
        }

        for(int i = 0; i < gameUIManager.taskContainers.Length; i++)
        {
            if(i < numberOfTotalTasks)
            {
                gameUIManager.ToggleTaskContainer(i, true);
                gameUIManager.SetTask(i, gameTasks[i]);
            }
            else
            {
                gameUIManager.ToggleTaskContainer(i, false);
            }
        }
    }

    public void CompleteTask(int taskID)
    {
        for(int i = 0; i < gameTasks.Count; i++)
        {
            if (gameTasks[i].taskID == taskID)
            {
                if (!completedTasks.Contains(i))
                {
                    completedTasks.Add(i);
                    break;
                }
            }
        }
    }

    public List<int> GetCompletedTasks()
    {
        return completedTasks;
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
