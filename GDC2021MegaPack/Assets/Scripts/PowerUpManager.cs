using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameManager gameManager;
    Transform spawnpointPos;
    float timeSinceLastSpawn;
    float spawnRate;
    public float minSpawnRate;
    public float maxSpawnRate;
    public GameObject powerUp;
    int whichSpawnSide;
    float powerUpx;
    float powerUpz;
    public bool powerUpPlayer1;
    public bool powerUpPlayer2;
    public string[] powerUps;
    bool powerUpSuperSpeed;
    bool powerUpFastShot;
    bool powerUpScatterShot;
    float pickupTime;





    // Start is called before the first frame update

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        timeSinceLastSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeSinceLastSpawn + spawnRate)
        {
            spawn_powerUp();
            timeSinceLastSpawn = Time.time;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void spawn_powerUp()
    {
        whichSpawnSide = Random.Range(1, 5);
        if (whichSpawnSide == 1)
        {
            powerUpx = Random.Range(gameManager.spawnpoints[0].transform.position.x, gameManager.spawnpoints[1].transform.position.x);
            powerUpz = Random.Range(gameManager.spawnpoints[0].transform.position.z, gameManager.spawnpoints[1].transform.position.z);
            Instantiate(powerUp, new Vector3(powerUpx, 1f, powerUpz), Quaternion.identity);
        }
        else if (whichSpawnSide == 2)
        {
            powerUpx = Random.Range(gameManager.spawnpoints[1].transform.position.x, gameManager.spawnpoints[2].transform.position.x);
            powerUpz = Random.Range(gameManager.spawnpoints[1].transform.position.z, gameManager.spawnpoints[2].transform.position.z);
            Instantiate(powerUp, new Vector3(powerUpx, 1f, powerUpz), Quaternion.identity);
        }
        else if (whichSpawnSide == 3)
        {
            powerUpx = Random.Range(gameManager.spawnpoints[2].transform.position.x, gameManager.spawnpoints[3].transform.position.x);
            powerUpz = Random.Range(gameManager.spawnpoints[2].transform.position.z, gameManager.spawnpoints[3].transform.position.z);
            Instantiate(powerUp, new Vector3(powerUpx, 1f, powerUpz), Quaternion.identity);
        }
        else if (whichSpawnSide == 4)
        {
            powerUpx = Random.Range(gameManager.spawnpoints[3].transform.position.x, gameManager.spawnpoints[0].transform.position.x);
            powerUpz = Random.Range(gameManager.spawnpoints[3].transform.position.z, gameManager.spawnpoints[0].transform.position.z);
            Instantiate(powerUp, new Vector3(powerUpx, 1f, powerUpz), Quaternion.identity);
        }
    }

   


}