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
            gameOverMenu.SetActive(true);
        }
    }
    public void RestartGame()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene("Main");
    }
    public void GoToMenu()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
