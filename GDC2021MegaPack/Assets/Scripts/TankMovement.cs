using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour
{
    Rigidbody RBvelocity;
    public float moveSpeed;
    public float maxMoveSpeed;
    public float rotationSpeed;
    public GameObject cam;
    public GameObject camPos;
    public bool player1;
    public Material color1, color2;
    public GameObject child;
    public GameObject bullet;
    public Transform gunPoint1;
    private float timeSinceLastFire;
    public float reloadTime;
    public GameObject opponent;
    public GameObject pointer;
    public GameObject reloadUI;
    public AudioClip idleSound, drivingSound;
    public AudioManager audioManager;
    float xInput, zInput;
    AudioSource audioSource;
    bool playShotSound;
    bool playReloadSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        RBvelocity = gameObject.GetComponent<Rigidbody>();
        MeshRenderer color = gameObject.GetComponent<MeshRenderer>();

        if (player1)
        {
            color.material = color1;
            child.GetComponent<MeshRenderer>().material = color1;
        }
        else
        {
            color.material = color2;
            child.GetComponent<MeshRenderer>().material = color2;
        }
    }

    void Update()
    {

        muzzle_sounds();
        compass();


        cam.transform.position = camPos.transform.position;
        cam.transform.rotation = camPos.transform.rotation;

    }
    private void FixedUpdate()
    {
        move_handler();
    }

    void compass()
    {
        pointer.transform.LookAt(opponent.transform.position);
    }

    //reset position
    void reset_position()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            gameObject.transform.rotation = Quaternion.identity;

        }
    }

    void move_handler()
    {
        if (player1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                audioSource.clip = audioManager.play_move();
                audioSource.Play();
                if (Input.GetKeyDown(KeyCode.W))
                {
                    zInput = 1f;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    zInput = -1f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                audioSource.clip = audioManager.play_idle();
                audioSource.Play();
                zInput = 0;
            }
            RBvelocity.AddForce(zInput * transform.forward * moveSpeed);




            if (Input.GetKey("d"))
            {
                transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.fixedDeltaTime;
            }

            if (Input.GetKey("a"))
            {
                transform.eulerAngles += new Vector3(0, -rotationSpeed, 0) * Time.fixedDeltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                reloadUI.GetComponent<Image>().color = new Vector4(255, 255, 255, 40);

                if (timeSinceLastFire + reloadTime <= Time.time)
                {
                    timeSinceLastFire = Time.time;
                    Instantiate(bullet, new Vector3(gunPoint1.transform.position.x, gunPoint1.transform.position.y, gunPoint1.transform.position.z),
                    Quaternion.Euler(new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z)));
                    playShotSound = true;
                    playReloadSound = true;
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {

                gameObject.transform.rotation = Quaternion.identity;

            }

            if (timeSinceLastFire + reloadTime > Time.time)
            {
                reloadUI.GetComponent<CanvasGroup>().alpha = 0.6f;
            }
            else
            {
                reloadUI.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.clip = audioManager.play_move();
                audioSource.Play();
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    zInput = 1f;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    zInput = -1f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                audioSource.clip = audioManager.play_idle();
                audioSource.Play();
                zInput = 0;
            }
            RBvelocity.AddForce(zInput * transform.forward * moveSpeed);

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles += new Vector3(0, -rotationSpeed, 0) * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (timeSinceLastFire + reloadTime <= Time.time)
                {
                    timeSinceLastFire = Time.time;
                    Instantiate(bullet, new Vector3(gunPoint1.transform.position.x, gunPoint1.transform.position.y, gunPoint1.transform.position.z),
                    Quaternion.Euler(new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z)));
                    playShotSound = true;
                    playReloadSound = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.O))
            {

                gameObject.transform.rotation = Quaternion.identity;

            }

            if (timeSinceLastFire + reloadTime > Time.time)
            {
                reloadUI.GetComponent<CanvasGroup>().alpha = 0.6f;
            }
            else
            {
                reloadUI.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
        RBvelocity.velocity = Vector3.ClampMagnitude(RBvelocity.velocity, maxMoveSpeed);
    }

    void muzzle_sounds()
    {
        if (playShotSound)
        {
            print("muzzleSounds");
            GameObject muzzle = this.gameObject.transform.Find("Tank_Færdigmoddeleret_UdenAnimation").Find("Muzzle").gameObject;
            //print(test);
            //this.gameObject.transform.Find("Muzzle").GetComponent<AudioSource>().clip = audioManager.play_shot();
            //this.gameObject.transform.Find("Muzzle").GetComponent<AudioSource>().Play();
            playShotSound = false;
            //this.gameObject.transform.Find("Muzzle").GetComponent<AudioSource>().clip = audioManager.play_reload();
            //this.gameObject.transform.Find("Muzzle").GetComponent<AudioSource>().Play();
            muzzle.GetComponent<AudioSource>().clip = audioManager.play_reload();
            muzzle.GetComponent<AudioSource>().Play();
        }
        else if (!playShotSound)
        {
            print("error");
        }
    }
    
    public void test()
    {
        print("test");
    }
}
