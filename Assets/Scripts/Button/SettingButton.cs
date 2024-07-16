using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public GameObject Popup;
    void Click()
    {
        Popup.SetActive(true);
    }
}
