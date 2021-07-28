using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    private GameManager gameManager;

    Rigidbody RBbullet;
    public float bulletSpeed;
    int bulletBounces;
    public int maxBulletBounces;
    public AudioManager audioManager;
    AudioSource audioSource;
    TankMovement tankMovement;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        RBbullet = GetComponent<Rigidbody>();
        tankMovement = FindObjectOfType<TankMovement>();
        RBbullet.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        audioSource.clip = audioManager.play_shot();
        audioSource.volume = 0.7f;
        audioSource.Play();

    }
    private void Update()
    {
        if (gameManager.tankDestroyed)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            collision.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(audioManager.play_explosion(), collision.transform.position);
            Destroy(gameObject);
            gameManager.tank1Destroyed = true;

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }

        }
        else if (collision.gameObject.tag == "Player2")
        {
            collision.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(audioManager.play_explosion(), collision.transform.position);
            Destroy(gameObject);
            gameManager.tank2Destroyed = true;

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }

        }
        else
        {
            if (bulletBounces <= maxBulletBounces)
            {
                audioSource.clip = audioManager.play_bounce();
                audioSource.volume = 1f;
                audioSource.Play();
                bulletBounces++;
            }
            else
            {
                Destroy(gameObject);
            }
            Vector3 bulletdirection = Vector3.zero;

            bulletdirection = new Vector3(RBbullet.velocity.x, 0, RBbullet.velocity.z);
            //bulletdirection.Normalize();
            bulletdirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(bulletdirection);
            transform.rotation = targetRotation;

        }

    }
}
