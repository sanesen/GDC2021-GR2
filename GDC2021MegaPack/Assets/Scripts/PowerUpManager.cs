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
        if (Time.time>=timeSinceLastSpawn+spawnRate)
        {
            spawn_powerUp();
            timeSinceLastSpawn = Time.time;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void spawn_powerUp()
    {
        float powerUpx = Random.Range(gameManager.spawnpoints[0].transform.position.x, gameManager.spawnpoints[1].transform.position.x);
        float powerUpz = Random.Range(gameManager.spawnpoints[0].transform.position.z, gameManager.spawnpoints[1].transform.position.z);
        Instantiate(powerUp, new Vector3(powerUpx, 0.6f, powerUpz),Quaternion.identity);
    }
}
