using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Transform playerTransform;
    public bool PlayerMove=true;
    void FixedUpdate()
    {
        if (PlayerMove)
        {
            float Leftright = Input.GetAxisRaw("Horizontal");
            float Updown = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(Leftright, Updown, 0).normalized * speed;
            transform.Translate(movement * Time.deltaTime);
        }
    }
}
