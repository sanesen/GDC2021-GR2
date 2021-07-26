using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //defining variables
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
    bool pauseState = false;
    public TextMeshProUGUI pausedText;
    public Image pauseBackground;



        //GM reset
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);

            if (_instance != null)
            {
                _instance.OnDisable();
                Destroy(_instance);
            }

            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            timer = gameTime;
            winner.enabled = false;
            rematch.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
            pausedText.enabled = false;
            pauseBackground.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            points();
            time_left();
            menu();
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
        {//points system
            if (tank1Destroyed)
            {
                point2++;
                pointText2.text = point2.ToString();
                tank1Destroyed = false;
                randomSpawnpoint = Random.Range(0, 4);
                tank1.transform.position = spawnpoints[randomSpawnpoint].transform.position;
                tank1.gameObject.SetActive(true);
            }

            if (tank2Destroyed)
            {
                point1++;
                pointText1.text = point1.ToString();
                tank2Destroyed = false;
                randomSpawnpoint = Random.Range(0, 4);
                tank2.transform.position = spawnpoints[randomSpawnpoint].transform.position;
                tank2.gameObject.SetActive(true);
            }
        }

        //time stamp (game time)
        void time_left()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F0");
            }


            if (timer <= 0 && !(SceneManager.GetActiveScene().buildIndex == 1))
            {//winner display
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

        //Rematch setup
        public void rematch_button()
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
            timer = gameTime;
            winner.enabled = false;
            rematch.gameObject.SetActive(false);
            quit.gameObject.SetActive(false);
            Destroy(canvas);
        }


        //quit button
        public void quit_button()
        {
            Application.Quit();
        }

        //menu system / pause system
        void menu()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (pauseState == false)
                {
                    Time.timeScale = 0;
                    pauseState = true;
                    pauseBackground.enabled = true;
                    pausedText.enabled = true;
                    rematch.gameObject.SetActive(true);
                    quit.gameObject.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    pauseState = false;
                    pausedText.enabled = false;
                    pauseBackground.enabled = false;
                    rematch.gameObject.SetActive(false);
                    quit.gameObject.SetActive(false);
                }
            }
        }

    }

