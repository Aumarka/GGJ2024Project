using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Game UI Elements References")]
    public Image happinessBar;
    public GameObject tasksContainerPanel;
    public TaskContainer[] taskContainers;
    public TMP_Text timerText;

    private bool taskMenuState;

    private void Start()
    {
        taskMenuState = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleTasksMenu();
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

    public void SetTimerText(string timeText)
    {
        timerText.text = timeText;
    }

}
