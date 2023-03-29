using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GS;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }
    private void Awake()
    {
        // If there is no instance, set this as the instance
        if (instance == null)
        {
            instance = this;
        }
        // else
        // {
        //     // Destroy the duplicate instance
        //     Destroy(gameObject);
        // }
        // DontDestroyOnLoad(gameObject);
    }
    public TMP_Text scoreText;

    public SimpleAsteroid asteroid;
    public AdvancedLabs labs;
    public SimpleGunPowerUp powerUp;
    public SimpleLifePowerUp powerUp2;
    public ulong score;
    public Transform labSpawn;

    void Start()
    {
        ToggleCursor(false);
        globalGameStatus.Status = GameStatus.IN_PROGRESS;
        Debug.Log(globalGameStatus.Status.ToString());

        score = 0; //Convert to 000
        updateScore();
        StartCoroutine(asteroid.spawn(Quaternion.identity));
        StartCoroutine(labs.spawn(labSpawn.rotation));
        StartCoroutine(powerUp.spawn(Quaternion.identity));
        StartCoroutine(powerUp2.spawn(Quaternion.identity));
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void AddScore(ulong newScore)
    {
        score += newScore;
        updateScore();
    }
    public void GameOver()
    {
        globalGameStatus.Status = GameStatus.GAME_OVER;
        Debug.Log(globalGameStatus.Status.ToString());
    }
    public void ToggleCursor(bool flag)
    {
        Cursor.visible = flag;
        Cursor.lockState = flag ? CursorLockMode.None : CursorLockMode.Locked;
    }
}