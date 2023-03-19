using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text restartText;
    public TMP_Text gameOverText;

    public GameObject hazard;
    public GameObject labs;
    public GameObject powerUp;

    public Vector3 spawnValues;
    private uint score;
    public int hazardCount;
    public int labsCount;

    public float spawnWait;
    public float startWait;
    public float startLWait;
    public float waveWait;
    public float spawnLWait;
    public float spawnPWait;

    private bool gameOver;
    private bool restart;

    public Transform labSpawn;

    void Start()
    {
        gameOver = false;
        gameOverText.text = "";
        restart = false;
        restartText.text = "";

        score = 0; //Convert to 000
        updateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnLabs());
        StartCoroutine(SpawnPowerUps());
    }
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //Assignments
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            if (gameOver)
            {
                restartText.text = "Restart";
                restart = true;
                break;
            }
        }
    }
    IEnumerator SpawnLabs()
    {
        yield return new WaitForSeconds(startLWait);
        while (true)
        {
            for (int i = 0; i < labsCount; i++)
            {
                Vector3 spawnPositionL = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //Labs
                Instantiate(labs, spawnPositionL, labSpawn.rotation);
                yield return new WaitForSeconds(spawnLWait);
            }
            yield return new WaitForSeconds(5 * spawnLWait);

            if (gameOver)
            {
                break;
            }
        }
    }
    IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(startWait);
        float powerUpDelay = spawnPWait;
        for (int i = 0; i < 20; i++)
        {
            Vector3 spawnPositionP = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //Power Ups
            Quaternion spawnRotationP = Quaternion.identity;
            Instantiate(powerUp, spawnPositionP, spawnRotationP);
            yield return new WaitForSeconds(powerUpDelay);
            powerUpDelay += 5;

            if (gameOver)
            {
                break;
            }
        }
    }
    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void AddScore(uint newScore)
    {
        score += newScore;
        updateScore();
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}