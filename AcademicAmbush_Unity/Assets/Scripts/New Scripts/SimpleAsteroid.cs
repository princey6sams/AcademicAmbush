using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAsteroid : SimpleInteractiveObjects
{
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (destroyObj(other))
        {
            applyPlayerDamage(other);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        Debug.Log("SimpleAsteroid.OnTriggerEnter");
    }
}
