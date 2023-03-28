using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBolt : SimpleInteractiveObjects
{
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (tag == "Bolt" && other.tag == "Player")
        {
            return;
        }
        if (destroyObj(other))
        {
            applyPlayerDamage(other);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        Debug.Log("SimpleBolt.OnTriggerEnter");
    }
}