using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGunPowerUp : SimpleInteractiveObjects
{
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (destroyObj(other))
        {
            PlayerController.Instance.setGunCount();
            PlayerController.Instance.setFireRate();
            if (!playerCheck(other))
            {
                Destroy(other.gameObject); // To Destroy Bullet but not Player
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        Debug.Log("SimpleGunPowerUp.OnTriggerEnter");
    }
}
