using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class pCamera : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 4.0f;
    float z = -10.0f;
    Transform CameraTransform;
    Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = GetComponent<Transform>();
        PlayerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraTransform.position = Vector2.Lerp(CameraTransform.position, 
        player.transform.position, followSpeed);
        CameraTransform.Translate(0, 0, z);
    }
}
