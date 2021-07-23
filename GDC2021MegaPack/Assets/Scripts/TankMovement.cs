using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (timeSinceLastFire + reloadTime <= Time.time)
                {
                    timeSinceLastFire = Time.time;
                    Instantiate(bullet, new Vector3(gunPoint1.transform.position.x, gunPoint1.transform.position.y, gunPoint1.transform.position.z),
                    Quaternion.Euler(new Vector3(this.gameObject.transform.eulerAngles.x, this.gameObject.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z)));
                }

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
        }


        RBvelocity.velocity = Vector3.ClampMagnitude(RBvelocity.velocity, maxMoveSpeed);

        cam.transform.position = camPos.transform.position;
        cam.transform.rotation = camPos.transform.rotation;
    }
}
