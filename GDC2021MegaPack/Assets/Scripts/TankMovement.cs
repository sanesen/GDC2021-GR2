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
    public bool player1;
    public Material color1, color2;
    //public GameObject child;
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
    public AudioSource audioSource;
    bool playShotSound;
    bool playReloadSound;
    Animator tankAnimator;
    PowerUpManager powerUpManager;
    int whichPowerUp;
    public int amountOfPowerUps;
    public bool powerUpSuperSpeed;
    public bool powerUpReloadTime;
    public bool powerUpScattershot;
    public float powerUpTime;
    public bool powerUpActive;
    public float maxPowerUpTime;
    float superReloadTime;
    float standardReloadTime;
    MeshRenderer tankRenderer;
    MeshRenderer color;
    public Material reloadRed, reloadGreen;

    GameManager gameManager;



    void Start()
    {
        tankRenderer = this.gameObject.transform.Find("Reload-indicator").GetComponent<MeshRenderer>();
        tankAnimator = this.gameObject.transform.Find("Tank2 V2,lav_Animation_Skud og larvefødder").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        RBvelocity = gameObject.GetComponent<Rigidbody>();
        color = gameObject.GetComponent<MeshRenderer>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource.clip = audioManager.play_idle();
        audioSource.volume = 0.1f;
        audioSource.Play();

        standardReloadTime = reloadTime;
        superReloadTime = reloadTime / 2;

        if (player1)
        {
            color.material = color1;
            //child.GetComponent<MeshRenderer>().material = color1;
        }
        else
        {
            color.material = color2;
            //child.GetComponent<MeshRenderer>().material = color2;
        }
        rotationSpeed = 60f;
    }

    void Update()
    {


        muzzle_sounds();
        compass();
        power_active();

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
                audioSource.volume = 0.9f;
                audioSource.Play();

                if (Input.GetKeyDown(KeyCode.W))
                {
                    zInput = 1.5f;
                    rotationSpeed = 45f;
                    tankAnimator.SetBool("Forward", true);
                    if (powerUpSuperSpeed)
                    {
                        zInput *= 1.5f;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    zInput = -1f;
                    rotationSpeed = -45f;
                    tankAnimator.SetBool("Backward", true);

                    if (powerUpSuperSpeed)
                    {
                        zInput *= 1.5f;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                audioSource.clip = audioManager.play_idle();
                audioSource.volume = 0.05f;
                audioSource.Play();
                zInput = 0;
                rotationSpeed = 60f;
                tankAnimator.SetBool("Forward", false);
                tankAnimator.SetBool("Backward", false);
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
                if (powerUpReloadTime)
                {
                    reloadTime = superReloadTime;
                }

                if (timeSinceLastFire + reloadTime <= Time.time)
                {
                    timeSinceLastFire = Time.time;
                    Instantiate(bullet, new Vector3(gunPoint1.transform.position.x, gunPoint1.transform.position.y, gunPoint1.transform.position.z),
                    Quaternion.Euler(new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z)));
                    playShotSound = true;
                    playReloadSound = true;
                }
                reloadTime = standardReloadTime;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {

                gameObject.transform.rotation = Quaternion.identity;

            }

            if (timeSinceLastFire + reloadTime > Time.time)
            {
                tankRenderer.material = reloadRed;
                reloadUI.GetComponent<CanvasGroup>().alpha = 0.6f;
            }
            else
            {
                tankRenderer.material = reloadGreen;
                reloadUI.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.clip = audioManager.play_move();
                audioSource.volume = 0.9f;
                audioSource.Play();
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    rotationSpeed = 45f;
                    zInput = 1.5f;
                    tankAnimator.SetBool("Forward", true);
                    if (powerUpSuperSpeed)
                    {
                        zInput *= 1.5f;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    zInput = -1f;
                    rotationSpeed = -45f;
                    tankAnimator.SetBool("Backward", true);
                    if (powerUpSuperSpeed)
                    {
                        zInput *= 1.5f;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                audioSource.clip = audioManager.play_idle();
                audioSource.volume = 0.05f;
                audioSource.Play();
                zInput = 0;
                rotationSpeed = 60f;
                tankAnimator.SetBool("Forward", false);
                tankAnimator.SetBool("Backward", false);
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
                if (powerUpReloadTime)
                {
                    reloadTime = superReloadTime;
                }
                if (timeSinceLastFire + reloadTime <= Time.time)
                {
                    timeSinceLastFire = Time.time;
                    Instantiate(bullet, new Vector3(gunPoint1.transform.position.x, gunPoint1.transform.position.y, gunPoint1.transform.position.z),
                    Quaternion.Euler(new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z)));
                    playShotSound = true;
                    playReloadSound = true;
                }
                reloadTime = standardReloadTime;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {

                gameObject.transform.rotation = Quaternion.identity;

            }

            if (timeSinceLastFire + reloadTime > Time.time)
            {
                //tankRenderer.material = reloadRed;
                //reloadUI.GetComponent<CanvasGroup>().alpha = 0.6f;
            }
            else
            {
                //tankRenderer.material = reloadGreen;
                //reloadUI.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
        RBvelocity.velocity = Vector3.ClampMagnitude(RBvelocity.velocity, maxMoveSpeed);
    }

    void muzzle_sounds()
    {
        if (playShotSound)
        {
            GameObject muzzle = this.gameObject.transform.Find("Tank2 V2,lav_Animation_Skud og larvefødder").Find("Muzzle").gameObject;
            playShotSound = false;
            muzzle.GetComponent<AudioSource>().clip = audioManager.play_reload();
            muzzle.GetComponent<AudioSource>().volume = 0.5f;
            muzzle.GetComponent<AudioSource>().Play();
            tankAnimator.SetTrigger("Shoot");
        }
    }

    void power_up()
    {
        if (powerUpManager.powerUpPlayer1)
        {

        }
    }

    public void mysterybox_collision()
    {
        if (!powerUpActive)
        {
            if (this.gameObject.tag == "Player1" || this.gameObject.tag == "Player2")
            {
                whichPowerUp = Random.Range(0, amountOfPowerUps);


                if (whichPowerUp == 0)
                {
                    powerUpSuperSpeed = true;
                }
                else if (whichPowerUp == 1)
                {
                    powerUpReloadTime = true;
                }
                //else if (whichPowerUp == 2)
                //{
                //    powerUpScattershot = true;
                //}

                powerUpTime = Time.time;
                powerUpActive = true;
            }

        }
    }

    void power_active()
    {
        if (Time.time >= powerUpTime + maxPowerUpTime)
        {
            powerUpSuperSpeed = false;
            powerUpReloadTime = false;
            powerUpScattershot = false;
            powerUpActive = false;
        }
    }
}
