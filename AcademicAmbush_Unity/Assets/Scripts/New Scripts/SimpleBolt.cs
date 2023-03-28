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
            if (playerCheck(other))
            {
                PlayerController.Instance.setLife(damage);
                if (PlayerController.Instance.lifeCount == 0)
                {
                    GameController.Instance.GameOver();
                }
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        Debug.Log("SimpleBolt.OnTriggerEnter");
    }
}
