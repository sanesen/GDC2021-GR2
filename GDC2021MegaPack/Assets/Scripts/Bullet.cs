using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameManager gameManager;

    Rigidbody RBbullet;
    public float bulletSpeed;
    int bulletBounces;
    public int maxBulletBounces;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        RBbullet = GetComponent<Rigidbody>();
        RBbullet.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gameManager.tank1Destroyed = true;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gameManager.tank2Destroyed = true;
        }
        else
        {
            if (bulletBounces <= maxBulletBounces)
            {
                bulletBounces++;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
