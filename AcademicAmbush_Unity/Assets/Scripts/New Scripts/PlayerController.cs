using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GS;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }
    void Awake()
    {
        // If there is no instance, set this as the instance
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
    }
    public GameObject explosion;
    public Boundary boundary;
    public SimpleBolt shot;
    public AudioSource shotA;
    public TMP_Text lifeText;
    public TMP_Text playerHealthText;
    public float speed, tilt, yaw, nextFire;
    public uint lifeCount;
    public byte playerHealth;
    public Transform shotSpawnC, shotSpawnL, shotSpawnR;
    public float gunCount = 0; //powerup
    public float fireRate = 1; //powerup combined w/ gunCount

    void Start()
    {
        lifeCount = 4;
        playerHealth = 100;
        lifeText.text = "LIVES: " + lifeCount;
        playerHealthText.text = "HEALTH: " + playerHealth + "%";
    }
    // Update is called once per frame
    void Update()
    {
        // if (!PauseMenuController.isPaused)
        // {
        //     if (Time.time > nextFire)
        //     {
        //         nextFire = Time.time + fireRate;
        //         setGun();
        //         shotA.Play();
        //     }
        // }
        //If statement + Input.GetButton() for bomb
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            setGun();
            shotA.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        if (GetComponent<Rigidbody>().velocity.z >= 0)
        {
            yaw = 0;
        }
        else
        {
            yaw = GetComponent<Rigidbody>().velocity.z * 0.1f * tilt;
        }

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(yaw, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Powerup1"))
    //     {
    //         pwrA.Play();
    //     }
    // }
    public void setGunCount()
    {
        gunCount += 1;
    }

    public void setFireRate()
    {
        if (gunCount == 0)
        {
            fireRate = 0.75f;
        }
        else if (gunCount == 1 || gunCount == 2)
        {
            fireRate = 0.45f;
        }
        else if (gunCount == 3 || gunCount == 4)
        {
            fireRate = 0.25f;
        }
        else if (gunCount == 5 || gunCount == 6 || gunCount == 7)
        {
            fireRate *= 0.9f;
            shot.setSpeed(1.25f);
        }
        else if (gunCount >= 8)
        {
            fireRate *= 0.95f;
            shot.setSpeed(1.15f);
        }
    }

    void setGun()
    {
        if (gunCount == 0 || gunCount == 1)
        {
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation); // Center
        }
        else if (gunCount == 2 || gunCount == 3)
        {
            Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation); //Left
            Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation); //Right
        }
        else
        {
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation); //Center
            Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation); //Left
            Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation); //Right
        }
    }

    public void setLife(byte damage)
    {
        if (playerHealth - damage < 0)
        {
            playerHealth = 0;
        }
        else
        {
            playerHealth -= damage;
        }
        lifeText.text = "LIVES: " + lifeCount;
        playerHealthText.text = "HEALTH: " + playerHealth + "%";

        if (playerHealth == 0 && lifeCount != 0)
        {
            playerHealth = 100;
            lifeCount -= 1;
            lifeText.text = "LIVES: " + lifeCount;
            playerHealthText.text = "HEALTH: " + playerHealth + "%";
        }
        if (lifeCount == 0)
        {
            lifeText.text = "LIVES: " + (lifeCount);
            playerHealthText.text = "HEALTH: " + playerHealth + "%";
            if (playerHealth == 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                globalGameStatus.Status = GameStatus.GAME_OVER;
            }
            Debug.Log(globalGameStatus.Status.ToString());
        }

    }
    public void receiveLife()
    {
        if (lifeCount <= 3)
        {
            lifeCount += 1;
        }
        else if (lifeCount == 4 && playerHealth < 100)
        {
            playerHealth = 100;
        }
        lifeText.text = "LIVES: " + (lifeCount);
        playerHealthText.text = "HEALTH: " + playerHealth + "%";
    }
    // public void applyPlayerDamage(Collider player) THOUGHTS???
    // {
    //     GameController.Instance.AddScore(scoreValue);
    //     if (playerCheck(player))
    //     {
    //         setLife(damageValue);
    //         if (lifeCount == 0 && playerHealth <= 0)
    //         {
    //             GameController.Instance.GameOver();
    //         }
    //     }
    // }
}
