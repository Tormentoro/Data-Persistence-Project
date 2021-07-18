using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Text ScoreText;
    public Text playerName;
    public Text gameModeText;
    public Text endGameHighscore;

    public Text score1;
    public Text score2;
    public Text score3;

    public GameObject GameOverText;
    public GameObject GameOverScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        playerName.text = MainManager.MMinstance.playerName + "'s";
        gameModeText.text = "Game mode: " + MainManager.MMinstance.gameMode;

        score1.text = "      " + MainManager.MMinstance.firstPlayerName + "  " + MainManager.MMinstance.score1;
        score2.text = "      " + MainManager.MMinstance.secondPlayerName + "  " + MainManager.MMinstance.score2;
        score3.text = "      " + MainManager.MMinstance.thirdPlayerName + "  " + MainManager.MMinstance.score3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * (2.0f * MainManager.MMinstance.difficulty), ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                m_GameOver = false;
                m_Started = false;
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

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    void AddPoint(int point)
    {
        if (MainManager.MMinstance.gameMode == "easy")
        {
            m_Points += point;
            ScoreText.text = $"Score: {m_Points}";
        }
        if (MainManager.MMinstance.gameMode == "normal")
        {
            m_Points += point*2;
            ScoreText.text = $"Score: {m_Points}";
        }
        if (MainManager.MMinstance.gameMode == "hard")
        {
            m_Points += point * 3;
            ScoreText.text = $"Score: {m_Points}";
        }
    }

    public void GameOver()
    {        
        m_GameOver = true;
        if (m_Points > MainManager.MMinstance.score1)
        {   
            GameOverScoreText.SetActive(true);
            endGameHighscore.text = "Your score is: " + m_Points;
            MainManager.MMinstance.score3 = MainManager.MMinstance.score2;      //присваиваем значение бывшего второго места третьему месту
            MainManager.MMinstance.score2 = MainManager.MMinstance.score1;      //присваиваем значение бывшего первого места второму месту
            MainManager.MMinstance.score1 = m_Points;                           //присваиваем текущее значение первому месту
            MainManager.MMinstance.thirdPlayerName = MainManager.MMinstance.secondPlayerName;   //присваиваем имя бывшего второго места третьему месту
            MainManager.MMinstance.secondPlayerName = MainManager.MMinstance.firstPlayerName;   //присваиваем имя бывшего первого места второму месту
            MainManager.MMinstance.firstPlayerName = playerName.text;                           //присваиваем текущее имя первому месту
            score3.text = score2.text;
            score2.text = score1.text;
            score1.text = "      " + MainManager.MMinstance.firstPlayerName + "  " + MainManager.MMinstance.score1;
            
        }
        else
        if (m_Points > MainManager.MMinstance.score2)
        {
            GameOverScoreText.SetActive(true);
            endGameHighscore.text = "Your score is: " + m_Points;
            MainManager.MMinstance.score3 = MainManager.MMinstance.score2;
            MainManager.MMinstance.score2 = m_Points;
            MainManager.MMinstance.thirdPlayerName = MainManager.MMinstance.secondPlayerName;
            MainManager.MMinstance.secondPlayerName = playerName.text;
            score3.text = score2.text;
            score2.text = "      " + MainManager.MMinstance.secondPlayerName + "  " + MainManager.MMinstance.score2;
        }
        else
        if (m_Points > MainManager.MMinstance.score3)
        {
            GameOverScoreText.SetActive(true);
            endGameHighscore.text = "Your score is: " + m_Points;
            MainManager.MMinstance.score3 = m_Points;
            MainManager.MMinstance.thirdPlayerName = playerName.text;            
            score3.text = "      " + MainManager.MMinstance.thirdPlayerName +"  "+ MainManager.MMinstance.score3;
        }    
        else
        {
            GameOverText.SetActive(true);
        }
    }

}
