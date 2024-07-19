using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int MazeSize { get; private set; }
    public static int numX, numY, HintItem, BombItem;
    public static int Score = 0;
    public static int highScore = 0;
    public static float highTime = 0.0f;
    public static string highDiff;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    public static string difiicult; 
    public static float GameTime = 123.0f;
    public static int GenPer = 0;
    private void Awake()
    {
        Score = 0;
        switch (difiicult)
        {
            case "Nube":
                numX = 10;
                numY = 10;
                GenPer = 4;
                GameTime = 180.0f;
                break;
            case "Easy":
                numX = 25;
                numY = 25;
                GenPer = 5;
                GameTime = 180.0f;
                break;
            case "Nomal":
                numX = 50;
                numY = 50;
                GenPer = 7;
                GameTime = 240.0f;
                break;
            case "Hard":
                numX= 100;
                numY= 100;
                GenPer = 10;
                GameTime = 300.0f;
                break;
        }
        ScoreText.text = "Score : " + string.Format("{0:n0}", Score);
        HintItem = numX / 4;
        BombItem = numX / 5;
    }
    private void Update()
    {
        GameTime = GameTime - Time.deltaTime;
        TimeText.text = "Time : " + GameTime.ToString("F3");
        if (GameTime<=0)
        {
            GameTime = 0;
            MainScenesManager.Timeover = true;
            SceneManager.LoadScene("MainScenes");
        }
    }
    public void ScoreUp()
    {
        Score = Score + 1000;
        ScoreText.text = "Score : " + string.Format("{0:n0}", Score);
    }
    public void TimeUp()
    {
        GameTime = GameTime + 10.0f;
    }
}
