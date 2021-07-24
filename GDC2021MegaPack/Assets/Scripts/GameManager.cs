using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    
    public bool tank1Destroyed, tank2Destroyed;

    int point1, point2;
    float timer;
    public float gameTime;
    public TextMeshProUGUI pointText1, pointText2;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        timer = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (tank1Destroyed)
        {
            point2++;
            pointText2.text = point2.ToString();
            print(point2);
            tank1Destroyed = false;
        }

        if (tank2Destroyed)
        {
            point1++;
            pointText1.text = point1.ToString();
            tank2Destroyed = false;
        }

        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F0");
    }
}
