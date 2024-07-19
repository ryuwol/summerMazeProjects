using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScenesManager : MonoBehaviour
{
    public static bool Timeover=false;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LastScore;
    public GameObject TimeoverPopup;
    private void Awake()
    {
        if (Timeover)
        {
            TimeoverPopup.SetActive(true);
            Timeover = false;
        }
        HighScore.text = "점수:" + string.Format("{0:n0}", GameManager.highScore) + "\n시간:" + GameManager.highTime.ToString("F3")
        + "   난이도:" + GameManager.highDiff;
        LastScore.text = "점수:" + string.Format("{0:n0}", GameManager.Score) + "\n시간:" + GameManager.GameTime.ToString("F3")
        + "   난이도:" + GameManager.difiicult;
    }
}
