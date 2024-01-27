using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameUIManager : MonoBehaviour
{
    GameDirector gameDirector;

    public TMP_Text endGameText;

    // Start is called before the first frame update
    private void Start()
    {
        if (gameDirector == null)
        {
            gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        }
    }

    public void SetEndGameText(int endingID)
    {
        switch(endingID)
        {
            case 0:
                endGameText.text = "You successfully took care of the baby with no issues!";
                break;
            case 1:
                endGameText.text = "You stopped the baby from crying but you didn't do all the tasks! Better luck next time";
                break;
            case 2:
                endGameText.text = "You were able to do all the tasks but you made the baby cry! Better luck next time";
                break;
            case 3:
                endGameText.text = "You didn't finish all the tasks and the baby cried. Worst babysitter ever...";
                break;   
        }
    }
}
