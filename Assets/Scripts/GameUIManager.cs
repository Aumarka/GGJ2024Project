using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class GameUIManager : MonoBehaviour
{
    GameDirector gameDirector;

    [Header("Game UI Elements References")]
    public Image happinessBar;
    public GameObject tasksContainerPanel;
    public TaskContainer[] taskContainers;
    public TMP_Text timerText;
    public TMP_Text itemDisplayText;
    public Image throwBar;

    private bool taskMenuState;

    private Color32 nomalTaskColour = new Color32(255, 255, 255, 50);
    private Color32 completedTaskColour = new Color32(0, 255, 37, 50);

    private void Start()
    {
        if(gameDirector == null)
        {
            gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        }
        taskMenuState = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleTasksMenu();
        }

        UpdateTaskListState();
        UpdateHappinessBar();
    }

    private void UpdateHappinessBar()
    {
        happinessBar.fillAmount = Mathf.Lerp(happinessBar.fillAmount, (float)gameDirector.babyHappiness / (float)gameDirector.maxBabyHappiness, Time.deltaTime * 5.0f);
    }

    public void UpdateThrowBar(float fillAmount)
    {
        throwBar.fillAmount = Mathf.Lerp(throwBar.fillAmount, fillAmount, Time.deltaTime * 5.0f);
    }
    private void UpdateTaskListState()
    {
        for(int i = 0; i < taskContainers.Length; i++)
        {
            if (gameDirector.GetCompletedTasks().Contains(i))
            {
                taskContainers[i].SetContainerPanelColour(completedTaskColour);
            }
            else
            {
                taskContainers[i].SetContainerPanelColour(nomalTaskColour);
            }
        }
    }

    private void ToggleTasksMenu()
    {
        taskMenuState = !taskMenuState;

        tasksContainerPanel.SetActive(taskMenuState);
    }

    public void SetTask(int taskPanelIndex, Task selectedTask)
    {
        taskContainers[taskPanelIndex].SetTaskContainerText(selectedTask.taskDescription);
    }

    public void ToggleTaskContainer(int taskPanelIndex, bool toggleState)
    {
        taskContainers[taskPanelIndex].gameObject.SetActive(toggleState);
    }

    public void SetTimerText(string timeText)
    {
        timerText.text = timeText;
    }

    public void SetItemText(string itemText)
    {
        itemDisplayText.text = itemText;
    }

}
