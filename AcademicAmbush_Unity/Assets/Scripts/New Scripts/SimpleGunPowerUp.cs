using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGunPowerUp : SimpleInteractiveObjects
{
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Boundary")
        {
            PlayerController.Instance.setGunCount();
            PlayerController.Instance.setFireRate();
            // if (!playerCheck(other))
            // {
            //     Destroy(other.gameObject); // To Destroy Bullet but not Player
            // }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public override IEnumerator spawn(Quaternion spawnRotation)
    {
        if (PlayerController.Instance)
        {
            yield return base.spawn(spawnRotation);
        }
    }
}


