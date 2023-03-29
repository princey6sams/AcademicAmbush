using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GS;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (globalGameStatus.Status == GameStatus.GAME_OVER)
        {
            GameController.Instance.ToggleCursor(true);
            gameOverMenu.SetActive(true);
        }
    }
    public void RestartGame()
    {
        GameController.Instance.GetComponent<AudioSource>().Play();
        gameOverMenu.SetActive(false);
        GameController.Instance.ToggleCursor(false);
        SceneManager.LoadScene("Main");
    }
    public void GoToMenu()
    {
        gameOverMenu.SetActive(false);
        GameController.Instance.ToggleCursor(true);
        SceneManager.LoadScene("Menu");
    }
}
