using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        rb.velocity = ((transform.forward * zAxis + transform.right * xAxis) * speed) + new Vector3(0, rb.velocity.y, 0);
    }
}
