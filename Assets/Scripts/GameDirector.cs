using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public GameUIManager gameUIManager;
    public EndGameUIManager endGameUIManager;
    public GameObject endGameCanvas;

    public bool gameRunning;
    
    public int gameTimeInSeconds = 600;
    public int babyHappiness = 60;
    public int maxBabyHappiness;
    public int BabyHappinessMultiplier = 1;

    public AudioClip[] babyLaughs;
    public AudioClip babyCry;
    public AudioClip completeSound;

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
        endGameCanvas.SetActive(false);

        StartCoroutine(Countdown());
        GenerateTasks();

        completedTasks = new List<int>();

        maxBabyHappiness = babyHappiness;

        gameRunning = true;
        Time.timeScale = 1;
    }

    public void Update()
    {
        gameUIManager.SetTimerText(GetTime());


        // Game Testing Reload Function
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReloadGame();
        }

        // For Game State Testing Purposes
        if (Input.GetKeyDown(KeyCode.O))
        {
            while (true)
            {
                int selectedTaskIndex = Random.Range(0, gameTasks.Count);
                int selectedTaskID = gameTasks[selectedTaskIndex].taskID;

                if (!completedTasks.Contains(selectedTaskIndex))
                {
                    CompleteTask(selectedTaskID);
                    break;
                }
            }
         
        }
    }

    // Generates a list of tasks for the player to complete
    public void GenerateTasks()
    {
        numberOfTotalTasks = Random.Range(minNumberOfTasks, maxNumberOfTasks + 1);
        numberOfHouseholdTasks = Random.Range(2, Mathf.FloorToInt(maxNumberOfTasks / 2));

        gameTasks.Add(babyTasks.tasks[0]);
        gameTasks.Add(babyTasks.tasks[2]);

        int taskListIndex = 2;

        for(int i = 2; i < numberOfTotalTasks - numberOfHouseholdTasks; i++)
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
                    if(gameTasks[i].taskType == Task.taskTypes.Baby)
                    {
                        babyHappiness += gameTasks[i].happinessValue; // Change to task happiness value;
                        SoundManager.instance.PlaySound(babyLaughs[Random.Range(0, babyLaughs.Length)]);

                        if (babyHappiness > 60.0f)
                        {
                            babyHappiness = 60;
                        }
                    }
                    else
                    {
                        SoundManager.instance.PlaySound(completeSound);
                    }

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

        if(babyHappiness > 0)
        {
            babyHappiness -= 1 * BabyHappinessMultiplier;
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("End Game");

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        endGameCanvas.SetActive(true);
        gameRunning = false;

        // Determine the end game message to give the player
        if(completedTasks.Count == numberOfTotalTasks)
        {
            if(babyHappiness > 0)
            {
                endGameUIManager.SetEndGameText(0);
            }
            else
            {
                endGameUIManager.SetEndGameText(2);
                SoundManager.instance.PlaySound(babyCry);
            }
        }
        else
        {
            if(babyHappiness > 0)
            {
                endGameUIManager.SetEndGameText(1);
            }
            else
            {
                endGameUIManager.SetEndGameText(3);
                SoundManager.instance.PlaySound(babyCry);
            }
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeHappinessModifier(bool isIncreasing)
    {
        if (isIncreasing)
        {
            BabyHappinessMultiplier *= 2;
            return;
        }

        BabyHappinessMultiplier /= 2;
        if (BabyHappinessMultiplier < 1) BabyHappinessMultiplier = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}