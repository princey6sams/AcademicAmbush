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
        if (Input.GetKeyDown(KeyCode.Escape) && globalGameStatus.Status != GameStatus.GAME_OVER)
        {
            if (globalGameStatus.Status == GameStatus.IS_PAUSED)
            {
                ResumeGame();
                GameController.Instance.ToggleCursor(false);
            }
            else
            {
                PauseGame();
                GameController.Instance.ToggleCursor(true);
            }
        }

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        globalGameStatus.Status = GameStatus.IS_PAUSED;
        Debug.Log(globalGameStatus.Status.ToString());
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        globalGameStatus.Status = GameStatus.IN_PROGRESS;
        Debug.Log(globalGameStatus.Status.ToString());
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        GameController.Instance.ToggleCursor(true);
        SceneManager.LoadScene("Menu");
    }

    // public void QuitGame()
    // {
    //     Application.Quit();
    // }
}
