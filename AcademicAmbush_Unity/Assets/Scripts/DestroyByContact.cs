using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public byte scoreValue;
    public float damageValue;

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Powerup1" && tag == "Bolt") ||
            // (tag == "Powerup1" && other.tag == "Bolt") ||
            (other.tag == "Player" && tag == "Powerup1"))
        { // Clearly Defined Behavior
            Destroy(gameObject);
            PlayerController.Instance.setGunCount();
            PlayerController.Instance.setFireRate();
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag != "Player")
            {
                Destroy(other.gameObject); // To Destroy Bullet but not Player
            }
            return;
        }

        else if (other.tag == "Powerup1" ||
            other.tag == "BoltEnemy" ||
            other.tag == "Enemy" ||
            other.tag == "BG" ||
            (other.tag == "Powerup1" && tag == "Asteroids") || // Only because asteriods are permitted to collide with some things
            (other.tag == "Asteroids" && tag == "Powerup1") ||
            (other.tag == "Asteroids" && tag == "Asteroids") ||
            (other.tag == "Bolt" && tag == "Bolt") ||
            other.tag == "Boundary" ||
            (other.tag == "Player" && tag == "Bolt"))
        {
            return; // Ignore List for users of DestroyByContact
        }

        else if (other.tag == "Player") // Game Over & Life Condition
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            PlayerController.Instance.setLife(damageValue);
            if (PlayerController.Instance.lifeCount == 0)
            {
                Destroy(other.gameObject);
                GameController.Instance.GameOver();
            }
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(other.gameObject);
        GameController.Instance.AddScore(scoreValue);
    }
}
