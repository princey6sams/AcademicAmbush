using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAsteroid : SimpleInteractiveObjects // Incorporate RandomRotator
{
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    public override IEnumerator spawn(Quaternion spawnRotation)
    {
        yield return base.spawn(spawnRotation);
    }
}
