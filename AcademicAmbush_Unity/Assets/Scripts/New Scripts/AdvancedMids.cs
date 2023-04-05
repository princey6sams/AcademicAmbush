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
    private float RotationRate;
    private float RotationStep;
    // public AudioSource shotA;
    private int health;
    public Transform shotSpawnR;

    public override void Start()
    {
        health = 100;
        maxDistance = 10;
        fireRate = 1.5f;
        speedMin = 7.5f;
        RotationRate = 80f;
        wait = 8;
        DifficultyScaler();
        RotationStep = RotationRate * Time.deltaTime;
        Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
    }
    void Update()
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
        else if (other.tag == "Bolt")
        {
            health -= 20;
        }
        else if (other.tag == "BoltEnemy" || other.tag == "BoltEnemy2")
        {
            health -= 2;
        }

        applyPlayerDamage(other);
        Instantiate(explosion, transform.position, transform.rotation);
        if (health <= 0)
        {
            if (other.tag == "Bolt" || playerCheck(other))
            { GameController.Instance.AddScore(scoreValue); }
            Destroy(gameObject);
        }

    }

    public override void DifficultyScaler()
    {
        if (GameController.Instance.score >= 500 && GameController.Instance.score < 1000)
        {
            fireRate = 1f;
            RotationRate = 85f;
            speedMin = 8f;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
        else if (GameController.Instance.score >= 1000 && GameController.Instance.score < 1500)
        {
            fireRate = 0.95f;
            RotationRate = 90f;
            speedMin = 8.5f;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
        else if (GameController.Instance.score >= 1500 && GameController.Instance.score < 2000)
        {
            fireRate = 0.925f;
            RotationRate = 95f;
            speedMin = 9f;
            wait = 4;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
        else if (GameController.Instance.score >= 2000 && GameController.Instance.score < 3000)
        {
            fireRate = 0.9f;
            RotationRate = 100f;
            speedMin = 10f;
            wait = 3;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
        else if (GameController.Instance.score >= 3000 && GameController.Instance.score < 4500)
        {
            fireRate = 0.85f;
            RotationRate = 110f;
            speedMin = 11f;
            wait = 2;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
        else if (GameController.Instance.score >= 4500 && GameController.Instance.score < 6000)
        {
            fireRate = 0.8f;
            RotationRate = 125f;
            speedMin = 12f;
            wait = 1;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
        else if (GameController.Instance.score >= 6000)
        {
            fireRate = 0.7f;
            RotationRate = 150f;
            speedMin = 14f;
            wait = 0;
            Debug.Log("Fire,Rotation,Speed,wait,count" + fireRate + " " + RotationRate + " " + speedMin + " " + wait + " " + count);
        }
    }
    public override IEnumerator spawn(Quaternion spawnRotation)
    {
        Vector3[] spawnPosition = new Vector3[4];
        spawnPosition[0] = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        spawnPosition[1] = new Vector3(-spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
        spawnPosition[2] = new Vector3(spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
        spawnPosition[3] = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, -spawnValues.z);

        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < getCount(); i++)
            {
                Instantiate(this, spawnPosition[Random.Range(0, 4)], spawnRotation);
                yield return new WaitForSeconds(wait);
            }
            yield return new WaitForSeconds(7 * wait);
            if (globalGameStatus.Status == GameStatus.GAME_OVER)
            {
                break;
            }

        }
    }
    public uint getCount()
    {
        return count;
    }
}