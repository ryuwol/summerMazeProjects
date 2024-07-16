using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject Popup;
    void Click()
    {
        Popup.SetActive(false);
    }
}
