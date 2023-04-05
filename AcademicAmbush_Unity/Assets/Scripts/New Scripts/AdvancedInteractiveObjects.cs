using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS;

public abstract class AdvancedInteractiveObjects : MonoBehaviour, InteractiveObjects, SpawnSettings
{
    public SimpleBolt shot;
    public GameObject explosion;
    public Transform shotSpawn;
    public Vector3 spawnValues;
    public uint count;
    public uint wait;
    public uint startWait;
    public byte scoreValue;
    public byte damageValue;
    public float speedMin;
    public float speedMax;
    public float fireRate;
    public float nextFire;
    public abstract void OnTriggerEnter(Collider other);
    // Start is called before the first frame update
    public virtual void Start()
    {
        DifficultyScaler();
        moveObj(speedMin, speedMax);
    }
    public virtual IEnumerator spawn(Quaternion spawnRotation)
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i <= count; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(this, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(wait);
            }
            yield return new WaitForSeconds(5 * wait);
            if (globalGameStatus.Status == GameStatus.GAME_OVER)
            {
                break;
            }

        }
    }
    public virtual void moveObj(params object[] args)
    {
        speedMin = (float)args[0];
        speedMax = (float)args[1];
        GetComponent<Rigidbody>().velocity = -(transform.forward) * Random.Range(speedMin, speedMax); // only enemy uses this
    }
    public bool playerCheck(Collider other)
    {
        if (other.tag == "Player") // Game Over & Life utilization
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            return true;
        }
        return false;
    }
    public void applyPlayerDamage(Collider other) // Send to player?
    {

        if (playerCheck(other))
        {
            PlayerController.Instance.setLife(damageValue);
            if (PlayerController.Instance.lifeCount == 0 && PlayerController.Instance.playerHealth == 0)
            {
                GameController.Instance.GameOver();
            }
        }
    }
    public virtual void DifficultyScaler()
    {
        if (GameController.Instance.score >= 500 && GameController.Instance.score < 1000)
        {
            fireRate = 1f;
        }
        else if (GameController.Instance.score >= 1000 && GameController.Instance.score < 1500)
        {
            fireRate = 0.95f;
        }
        else if (GameController.Instance.score >= 1500 && GameController.Instance.score < 2000)
        {
            fireRate = 0.925f;
        }
        else if (GameController.Instance.score >= 2000 && GameController.Instance.score < 3000)
        {
            fireRate = 0.9f;
        }
        else if (GameController.Instance.score >= 3000 && GameController.Instance.score < 4500)
        {
            fireRate = 0.85f;
        }
        else if (GameController.Instance.score >= 4500 && GameController.Instance.score < 6000)
        {
            fireRate = 0.8f;
        }
        else if (GameController.Instance.score >= 6000)
        {
            fireRate = 0.7f;
        }
    }
}
