using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NubeButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.difiicult = "Nube";
        SceneManager.LoadScene("GameScenes");
    }
}
