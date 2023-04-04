using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS;

public abstract class SimpleInteractiveObjects : MonoBehaviour, InteractiveObjects, SpawnSettings
{
    public GameObject explosion;
    public Vector3 spawnValues;
    public float speedMin;
    public float speedMax;
    public uint incrementDelay;
    public uint count;
    public uint wait;
    public uint startWait;
    public byte scoreValue;
    public byte damageValue;
    protected bool destroyCheck;
    public uint waveWait;
    public float tumble;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (destroyObj(other))
        {
            applyPlayerDamage(other);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
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
            wait += incrementDelay;
            yield return new WaitForSeconds(waveWait);
            if (globalGameStatus.Status == GameStatus.GAME_OVER)
            {
                break;
            }

        }
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        destroyCheck = false;
        moveObj();
    }
    public virtual void moveObj(params object[] args)
    {
        speedMin = (float)args[0];
        speedMax = (float)args[1];
        GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(speedMin, speedMax);
    }
    public virtual bool destroyObj(Collider other)
    {
        if (playerCheck(other) || !ignoreListCheck(other))//NOT
        {
            destroyCheck = true;
        }
        return destroyCheck;
    }
    public bool playerCheck(Collider other) // combine checks
    {
        if (other.tag == "Player" || (other.tag == "Player" && tag == "Powerup1")) // Game Over & Life utilization
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            return true;
        }
        return false;
    }
    public bool ignoreListCheck(Collider other)
    {
        if (other.tag == "Powerup1" ||
            (other.tag == "Powerup1" && tag == "Asteroids") || // Only because asteriods are permitted to collide with some things
            (other.tag == "Asteroids" && tag == "Powerup1") ||
            (other.tag == "Asteroids" && tag == "Asteroids") ||
            (other.tag == "Bolt" && tag == "Bolt") ||
            (other.tag == "BoltEnemy2" && tag == "BoltEnemy2") ||
            (other.tag == "BoltEnemy2" && tag == "BoltEnemy") ||
            (other.tag == "BoltEnemy" && tag == "BoltEnemy2") ||
            other.tag == "Boundary" ||
            (other.tag == "BoltEnemy" && tag == "Powerup1") ||
            (tag == "BoltEnemy" && other.tag == "Powerup1") ||
            (other.tag == "BoltEnemy2" && tag == "Powerup1") ||
            (tag == "BoltEnemy2" && other.tag == "Powerup1") ||
            (other.tag == "Bolt" && tag == "Powerup1") ||
            (tag == "Bolt" && other.tag == "Powerup1") ||
            (other.tag == "Enemy" && tag == "Powerup1") ||
            (other.tag == "Player" && tag == "Bolt"))
        {
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
