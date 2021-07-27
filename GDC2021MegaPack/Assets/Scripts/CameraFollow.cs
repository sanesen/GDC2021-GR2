using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform targetRotation;
    private Vector3 followTargetVelocity;

    [Range(0, 3)] public float damping;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref followTargetVelocity, damping);
       
        var rotationVector = targetRotation.eulerAngles;

        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
