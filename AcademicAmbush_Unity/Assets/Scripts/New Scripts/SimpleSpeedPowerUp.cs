using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpeedPowerUp : SimpleInteractiveObjects
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
            PlayerController.Instance.setSpeed();
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
