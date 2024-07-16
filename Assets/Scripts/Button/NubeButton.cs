using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NubeButton : MonoBehaviour
{
    void Click()
    {
        GameManager.difiicult = "Nube";
        SceneManager.LoadScene("GameScenes");
    }
}
