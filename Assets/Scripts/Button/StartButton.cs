using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject Popup;
    public void Click()
    {
         Popup.SetActive(true);
    }
}
