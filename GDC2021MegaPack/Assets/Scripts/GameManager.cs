using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool tank1Destroyed, tank2Destroyed;
    public Scene gameScene;
    int point1, point2;
    float timer;
    public float gameTime;
    public TextMeshProUGUI pointText1, pointText2;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winner;
    public GameObject canvas;
    public Button rematch;
    public Button quit;
    public GameObject tank1, tank2;
    int randomSpawnpoint;
    public GameObject[] spawnpoints;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);

        if (_instance != null)
        {
            Destroy(_instance);
        }

        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = gameTime;
        winner.enabled = false;
        rematch.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        //spawnpoints = new GameObject[amountOfSpawnpoints];
        //foreach (GameObject gameObject in spawnpoints)
        //{
        //    int i = 0;
        //    spawnpoints[i].name = "spawnpoint" + i + 1;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        points();
        time_left();
    }

    private void OnEnable()
    {
        rematch.onClick.AddListener(rematch_button);
        quit.onClick.AddListener(quit_button);
    }

    private void OnDisable()
    {
        rematch.onClick.RemoveListener(rematch_button);
        quit.onClick.RemoveListener(quit_button);
    }

    void points()
    {
        if (tank1Destroyed)
        {
            point2++;
            pointText2.text = point2.ToString();
            tank1Destroyed = false;
            randomSpawnpoint = Random.Range(0, 4);
            tank1.transform.position = spawnpoints[randomSpawnpoint].transform.position;
        }

        if (tank2Destroyed)
        {
            point1++;
            pointText1.text = point1.ToString();
            tank2Destroyed = false;
            randomSpawnpoint = Random.Range(0, 4);
            tank2.transform.position = spawnpoints[randomSpawnpoint].transform.position;
        }
    }

    void time_left()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F0");
        }


        if (timer <= 0 && !(SceneManager.GetActiveScene().buildIndex == 1))
        {
            if (point1 > point2)
            {
                winner.text = "Player 1 wins!";
                winner.color = Color.red;
            }
            else if (point2 > point1)
            {
                winner.text = "Player 2 wins";
                winner.color = Color.blue;
            }
            else
            {
                winner.text = "Draw. Try again!";
                winner.color = Color.grey;
            }
            SceneManager.LoadScene(1);
            winner.enabled = true;
            rematch.gameObject.SetActive(true);
            quit.gameObject.SetActive(true);
        }
    }

    public void rematch_button()
    {
        SceneManager.LoadScene(0);
        timer = gameTime;
        winner.enabled = false;
        rematch.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        Destroy(canvas);
    }

    public void quit_button()
    {
        Application.Quit();
    }
}

