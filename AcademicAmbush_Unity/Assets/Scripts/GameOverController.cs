using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverMenu;
    private GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameOver == true)
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
