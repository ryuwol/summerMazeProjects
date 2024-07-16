using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public GameObject Popup;
    public void Click()
    {
        Popup.SetActive(true);
    }
}
