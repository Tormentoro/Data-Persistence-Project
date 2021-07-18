using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuCanvasScript : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Text diffInfoText;
    [SerializeField] TMP_Dropdown diffDropdown;
    private string difficultyInfo;

    public TextMeshProUGUI playerName;
    public float difficulty;

    private void Start()
    {
        
    }
    private void Update()
    {
        diffInfoText.text = "Game Mode: " + MainManager.MMinstance.gameMode;
        if (diffDropdown.value == 0)
        {
            difficulty = 0.75f;
            MainManager.MMinstance.gameMode = "easy";
        }else if (diffDropdown.value == 1)
        {
            difficulty = 1;
            MainManager.MMinstance.gameMode = "normal";
        }
        else if (diffDropdown.value == 2)
        {
            difficulty = 1.5f;
            MainManager.MMinstance.gameMode = "hard";
        }
    }

    public void StarGameButton()
    {
        MainManager.MMinstance.StartGame();      
        MainManager.MMinstance.playerName = playerName.text;
        if (MainManager.MMinstance.gameMode == "easy")
        {
            MainManager.MMinstance.easyPlays += 1;
        }
        if (MainManager.MMinstance.gameMode == "normal")
        {
            MainManager.MMinstance.normalPlays += 1;
        }
        if (MainManager.MMinstance.gameMode == "hard")
        {
            MainManager.MMinstance.hardPlays += 1;
        }

    }
    public void ExitButton()
    {
        MainManager.MMinstance.SaveGameData();
        MainManager.MMinstance.ExitGame();

    }
    public void SettingsButton()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void BackButton()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        MainManager.MMinstance.difficulty = difficulty;
    }
    public void HighScoreButton()
    {
        MainManager.MMinstance.HighScore();
    }
}
