using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLifePowerUp : SimpleInteractiveObjects
{
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (destroyObj(other))
        {
            PlayerController.Instance.receiveLife();
            if (!playerCheck(other))
            {
                Destroy(other.gameObject); // To Destroy Bullet but not Player
            }
            Instantiate(explosion, transform.position, transform.rotation); // Audio does not change for some reason
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
