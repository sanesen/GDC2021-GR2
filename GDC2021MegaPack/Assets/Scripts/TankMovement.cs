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



    void Start()
    {
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
        move_handler();
        compass();
      

        cam.transform.position = camPos.transform.position;
        cam.transform.rotation = camPos.transform.rotation;

    }

    void compass()
    {
        pointer.transform.LookAt(opponent.transform.position);
    }

    //reset position
    void reset_position()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            
            gameObject.transform.rotation = Quaternion.identity;

        }
    }

    void move_handler()
    {
        if (player1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                RBvelocity.AddForce(transform.forward * moveSpeed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                RBvelocity.AddForce(-transform.forward * moveSpeed);
            }

            if (Input.GetKey("d"))
            {
                transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.deltaTime;
            }

            if (Input.GetKey("a"))
            {
                transform.eulerAngles += new Vector3(0, -rotationSpeed, 0) * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                reloadUI.GetComponent<Image>().color = new Vector4(255, 255, 255, 40);

                if (timeSinceLastFire + reloadTime <= Time.time)
                {
                    timeSinceLastFire = Time.time;
                    Instantiate(bullet, new Vector3(gunPoint1.transform.position.x, gunPoint1.transform.position.y, gunPoint1.transform.position.z),
                    Quaternion.Euler(new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z)));
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {

                gameObject.transform.rotation = Quaternion.identity;

            }

            if (timeSinceLastFire+reloadTime>Time.time)
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
            if (Input.GetKey(KeyCode.UpArrow))
            {
                RBvelocity.AddForce(transform.forward * moveSpeed);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                RBvelocity.AddForce(-transform.forward * moveSpeed);
            }

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

    public void test()
    {
        print("test");
    }
}
