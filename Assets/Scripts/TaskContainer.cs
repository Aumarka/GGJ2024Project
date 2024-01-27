using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskContainer : MonoBehaviour
{
    public TMP_Text taskText;
    public Image taskContainerPanel;

    public void SetContainerPanelColour(Color panelColour)
    {
        taskContainerPanel.color = panelColour;
    }

    public void SetTaskContainerText(string text)
    {
        taskText.text = text;
    }
}
