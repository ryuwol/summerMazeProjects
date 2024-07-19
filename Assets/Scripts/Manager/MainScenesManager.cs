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
        HighScore.text = "����:" + string.Format("{0:n0}", GameManager.highScore) + "\n�ð�:" + GameManager.highTime.ToString("F3")
        + "   ���̵�:" + GameManager.highDiff;
        LastScore.text = "����:" + string.Format("{0:n0}", GameManager.Score) + "\n�ð�:" + GameManager.GameTime.ToString("F3")
        + "   ���̵�:" + GameManager.difiicult;
    }
}
