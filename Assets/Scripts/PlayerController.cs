using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Leftright = Input.GetAxisRaw("Horizontal");
        float Updown = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(Leftright, Updown, 0).normalized * speed;
        transform.Translate(movement * Time.deltaTime);
    }
}
