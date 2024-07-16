using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static int numX;
    public static int numY;
    public static string difiicult;
    private void Update()
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
}
