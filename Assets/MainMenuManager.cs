using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    GameObject helpMenu;
    GameObject controlsMenu;

    private void Start()
    {
        ToggleHelpMenu(false);
        ToggleControlsMenu(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleHelpMenu(bool toggleState)
    {
        helpMenu.SetActive(toggleState);
    }

    public void ToggleControlsMenu(bool toggleState)
    {
        controlsMenu.SetActive(toggleState);
    }
}
