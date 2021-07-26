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

    private void Update()
    {
        Debug.Log(RBbullet.velocity);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
            gameManager.tank1Destroyed = true;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.SetActive(false);
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
            Vector3 bulletdirection = Vector3.zero;

            bulletdirection = new Vector3(RBbullet.velocity.x, 0 , RBbullet.velocity.z);
            //bulletdirection.Normalize();
            bulletdirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(bulletdirection);
            transform.rotation = targetRotation;

        }
    }
}
