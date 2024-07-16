using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : MonoBehaviour
{
    public static int numX;
    public static int numY;
    public static int Score = 0;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    public static string difiicult; 
    public static float GameTime = 300.0f;
    private void Awake()
    {
        switch (difiicult)
        {
            case "Nube":
                numX = 10;
                numY = 10;
                break;
            case "Easy":
                numX = 25;
                numY = 25;
                break;
            case "Nomal":
                numX = 50;
                numY = 50;
                break;
            case "Hard":
                numX= 100;
                numY= 100;
                break;
        }
    }
    private void Update()
    {
        GameTime = GameTime - Time.deltaTime;
        TimeText.text = "Time : " + GameTime.ToString("F3");
    }
    public void ScoreUp()
    {
        ScoreText.text = "Score : " + string.Format("{0:n0}", Score);
    }
    public void TimeUp()
    {
        GameTime = +10.0f;
    }
}
