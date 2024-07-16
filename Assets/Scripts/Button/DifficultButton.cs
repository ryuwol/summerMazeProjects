using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.difiicult = this.gameObject.tag;
        SceneManager.LoadScene("GameScenes");
    }
}
