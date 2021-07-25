using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button rematch;
    public Button quit;


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


    public void rematch_button()
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(0);
    }

    public void quit_button()
    {
        Application.Quit();
    }
}
