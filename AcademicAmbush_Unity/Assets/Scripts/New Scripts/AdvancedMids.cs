using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS;

public class AdvancedMids : AdvancedInteractiveObjects
{

    private int maxDistance;
    private int distance;
    private Vector3 direction;
    private float angle;
    private float RotationStep;
    // public AudioSource shotA;
    private int health;

    public Transform shotSpawnR;

    void Start()
    {
        health = 100;
        RotationStep = 80f * Time.deltaTime;
        maxDistance = 10;
    }
    void LateUpdate()
    {
        speedMax = speedMin;
        if (!PlayerController.Instance)
        {
            Vector3 final = new Vector3(155, 0, 80);
            transform.position = Vector3.MoveTowards(this.transform.position,
            final,
            speedMin * Time.deltaTime);
            direction = final - transform.position;
            direction.Normalize();
            angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

        }
        else
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //Left
                Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation); //Right
            }

            distance = Mathf.RoundToInt(Vector3.Distance(transform.position,
            PlayerController.Instance.transform.position));
            direction = PlayerController.Instance.transform.position - transform.position;
            direction.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Rotate the transform towards the target rotation by a maximum angle
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationStep);

            if (distance > maxDistance)
            {
                transform.position = Vector3.MoveTowards(this.transform.position,
                PlayerController.Instance.transform.position,
                speedMin * Time.deltaTime);
            }
            else if (distance < maxDistance)
            {
                transform.position = Vector3.MoveTowards(this.transform.position,
                PlayerController.Instance.transform.position,
                -speedMin * 0.85f * Time.deltaTime);
            }
            else if (distance == maxDistance)
            {
                transform.position = this.transform.position;
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroids")
        {
            health -= 25;
        }
        else if (other.tag == "Player")
        {
            health = 0;
        }
        else if (other.tag == "Bolt" || other.tag == "BoltEnemy" || other.tag == "BoltEnemy2")
        {
            health -= 20;
        }
        if (destroyObj(other))
        {
            applyPlayerDamage(other);
            Instantiate(explosion, transform.position, transform.rotation);
            if (health <= 0)
            {
                if (other.tag == "Bolt" || playerCheck(other))
                { GameController.Instance.AddScore(scoreValue);}
                Destroy(gameObject);
            }
        }
        Debug.Log("SimpleAsteroid.OnTriggerEnter");
    }

    public override void FireRateScaler() //difficulty
    {

    }
    public override IEnumerator spawn(Quaternion spawnRotation)
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3[] spawnPosition = new Vector3[4];
                spawnPosition[0] = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                spawnPosition[1] = new Vector3(-spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
                spawnPosition[2] = new Vector3(spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
                spawnPosition[3] = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, -spawnValues.z);

                Instantiate(this, spawnPosition[Random.Range(0, 4)], spawnRotation);
                yield return new WaitForSeconds(wait);
            }
            yield return new WaitForSeconds(5 * wait);
            if (globalGameStatus.Status == GameStatus.GAME_OVER)
            {
                break;
            }

        }
    }

}
