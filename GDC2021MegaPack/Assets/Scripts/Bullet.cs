using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody RBbullet;
    public float bulletSpeed;

    void Awake()
    {
        RBbullet = GetComponent<Rigidbody>();
        RBbullet.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
