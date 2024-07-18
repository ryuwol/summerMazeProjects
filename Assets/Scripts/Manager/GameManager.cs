using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int MazeSize { get; private set; }
    public static int numX, numY, HintItem, BombItem;
    public static int Score = 0;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    public static string difiicult; 
    public static float GameTime = 300.0f;
    public static int GenPer = 0;
    private void Awake()
    {
        switch (difiicult)
        {
            case "Nube":
                numX = 10;
                numY = 10;
                GenPer = 4;
                break;
            case "Easy":
                numX = 25;
                numY = 25;
                GenPer = 5;
                break;
            case "Nomal":
                numX = 50;
                numY = 50;
                GenPer = 7;
                break;
            case "Hard":
                numX= 100;
                numY= 100;
                GenPer = 10;
                break;
        }
        HintItem = numX / 4;
        BombItem = numX / 5;
    }
    private void Update()
    {
        GameTime = GameTime - Time.deltaTime;
        TimeText.text = "Time : " + GameTime.ToString("F3");
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
