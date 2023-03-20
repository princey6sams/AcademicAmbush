using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject bulletExplosion;
    public uint scoreValue;
    // public AudioSource pwrA;
    private GameController gameController;
    private PlayerController playerController;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (!gameController)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Powerup1" && tag == "Bolt") ||
            (tag == "Powerup1" && other.tag == "Bolt") ||
            (other.tag == "Player" && tag == "Powerup1"))
        { // Clearly Defined Behavior
            // pwrA.Play();
            Destroy(gameObject);
            playerController.setGunCount();
            playerController.setFireRate();
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag != "Player")
            {
                Destroy(other.gameObject);
            }
            return;
        }
        else if (other.tag == "Powerup1" ||
            other.tag == "BoltEnemy" ||
            other.tag == "Enemy" ||
            (other.tag == "Powerup1" && tag == "Asteroids") ||
            (other.tag == "Asteroids" && tag == "Powerup1") ||
            (other.tag == "Bolt" && tag == "Bolt") ||
            other.tag == "Boundary" ||
            (other.tag == "Player" && tag == "Bolt"))
        {
            return; // Ignore List for users of DestroyByContact
        }

        else if (other.tag == "Player") // Game Over Condition
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameController.GameOver();
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(other.gameObject);
        gameController.AddScore(scoreValue);
    }
}
