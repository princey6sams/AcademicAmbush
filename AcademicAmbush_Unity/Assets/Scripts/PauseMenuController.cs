using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GS;


public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (globalGameStatus.Status == GameStatus.IS_PAUSED)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        globalGameStatus.Status = GameStatus.IS_PAUSED;
        Debug.Log(globalGameStatus.Status.ToString());
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        globalGameStatus.Status = GameStatus.IN_PROGRESS;
        Debug.Log(globalGameStatus.Status.ToString());
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // public void QuitGame()
    // {
    //     Application.Quit();
    // }
}
