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
    protected bool destroyCheck;
    public abstract void OnTriggerEnter(Collider other);
    public abstract void FireRateScaler();
    // Start is called before the first frame update
    void Start()
    {
        destroyCheck = false;
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
    public virtual bool destroyObj(Collider other)
    {
        if (playerCheck(other) || !ignoreListCheck(other))//NOT
        {
            destroyCheck = true;
        }
        return destroyCheck;
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
    public bool ignoreListCheck(Collider other)
    {
        if (other.tag == "Powerup1" ||
            other.tag == "BoltEnemy" ||
            other.tag == "Enemy" ||
            other.tag == "Boundary")
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            return true;
        }
        return false;
    }
    public void applyPlayerDamage(Collider other) // Send to player?
    {
        GameController.Instance.AddScore(scoreValue);
        if (playerCheck(other))
        {
            PlayerController.Instance.setLife(damageValue);
            if (PlayerController.Instance.lifeCount == 0 && PlayerController.Instance.playerHealth == 0)
            {
                GameController.Instance.GameOver();
            }
        }
    }
}
