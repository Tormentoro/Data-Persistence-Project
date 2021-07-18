using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager MMinstance;
       
    [Header("Storing Data")]
    
    public string gameMode;

    public int bestScore;
    public float difficulty=1;

    public string playerName;

    public string firstPlayerName = "empty";
    public int score1;
    public string secondPlayerName = "empty";
    public int score2;
    public string thirdPlayerName = "empty";
    public int score3;

    public int easyPlays;
    public int normalPlays;
    public int hardPlays;

    private void Awake()
    {
        if (MMinstance != null)
        {
            Destroy(gameObject);
            return;
        }
        MMinstance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void HighScore()
    {
        SceneManager.LoadScene(2);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetAllScore()
    {
        easyPlays = 0;
        normalPlays = 0;
        hardPlays = 0;

        firstPlayerName = "empty";
        secondPlayerName = "empty";
        thirdPlayerName = "empty";

        score1 = 0;
        score2 = 0;
        score3 = 0;

        SceneManager.LoadScene(0);
    }

[System.Serializable]
class SaveData
{
    public int score1;
    public int score2;
    public int score3;

    public string firstPlayerName;
    public string secondPlayerName;
    public string thirdPlayerName;


    public int easyPlays;
    public int normalPlays;
    public int hardPlays;
}
    public void SaveGameData()
    {
        SaveData data = new SaveData();

        data.easyPlays = easyPlays;
        data.normalPlays = normalPlays;
        data.hardPlays = hardPlays;

        data.firstPlayerName = firstPlayerName;
        data.secondPlayerName = secondPlayerName;
        data.thirdPlayerName = thirdPlayerName;

        data.score1 = score1;
        data.score2 = score2;
        data.score3 = score3;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            easyPlays = data.easyPlays;
            normalPlays = data.normalPlays;
            hardPlays = data.hardPlays;

            firstPlayerName = data.firstPlayerName;
            secondPlayerName = data.secondPlayerName;
            thirdPlayerName = data.thirdPlayerName;

            score1 = data.score1;
            score2 = data.score2;
            score3 = data.score3;
        }
    }
}
