using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GS;

public class GameController : MonoBehaviour
{
    public TMP_Text scoreText;

    public GameObject hazard;
    public GameObject labs;
    public GameObject powerUp;

    public Vector3 spawnValues;
    private ulong score;
    public int hazardCount;
    public int labsCount;

    public float spawnWait;
    public float startWait;
    public float startLWait;
    public float waveWait;
    public float spawnLWait;
    public float spawnPWait;

    public Transform labSpawn;

    void Start()
    {
        globalGameStatus.Status = GameStatus.IN_PROGRESS;
        Debug.Log(globalGameStatus.Status.ToString());

        score = 0; //Convert to 000
        updateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnLabs());
        StartCoroutine(SpawnPowerUps());
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
            if (globalGameStatus.Status == GameStatus.GAME_OVER)
            {
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

            if (globalGameStatus.Status == GameStatus.GAME_OVER)
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

            if (globalGameStatus.Status == GameStatus.GAME_OVER)
            {
                break;
            }
        }
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
}