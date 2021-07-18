using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreCanvasScript : MonoBehaviour
{
    public Text score1;
    public Text score2;
    public Text score3;

    public Text gameModeEasyText;
    public Text gameModeNormalText;
    public Text gameModeHardText;

    void Start()
    {
        score1.text = "      " + MainManager.MMinstance.firstPlayerName + "  " + MainManager.MMinstance.score1;
        score2.text = "      " + MainManager.MMinstance.secondPlayerName + "  " + MainManager.MMinstance.score2; 
        score3.text = "      " + MainManager.MMinstance.thirdPlayerName + "  " + MainManager.MMinstance.score3;

        gameModeEasyText.text = "Easy: " + MainManager.MMinstance.easyPlays;
        gameModeNormalText.text = "Normal: " + MainManager.MMinstance.normalPlays;
        gameModeHardText.text = "Hard: " + MainManager.MMinstance.hardPlays;
}
    public void BackButton()
    {
        MainManager.MMinstance.BackToMenu();
    }
    public void ResetAllScore()
    {
        MainManager.MMinstance.ResetAllScore();
    }

}
