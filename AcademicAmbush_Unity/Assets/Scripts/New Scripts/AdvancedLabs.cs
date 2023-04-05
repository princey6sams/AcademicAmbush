using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdvancedLabs : AdvancedInteractiveObjects
{
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (destroyObj(other))
        {
            applyPlayerDamage(other);
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag == "Bolt" || playerCheck(other))
            { GameController.Instance.AddScore(scoreValue);}
            Destroy(gameObject);
        }
        Debug.Log("SimpleAsteroid.OnTriggerEnter");
    }
    public override void FireRateScaler() //difficulty
    {
        // GameController.Instance.score;
    }

    public override IEnumerator spawn(Quaternion spawnRotation)
    {
        yield return base.spawn(spawnRotation);
    }
}
