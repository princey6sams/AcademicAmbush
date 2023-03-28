using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleInteractiveObjects : MonoBehaviour, InteractiveObjects
{
    public GameObject explosion;
    public byte scoreValue;
    public float speedMin;
    public float speedMax;
    public byte value;
    public byte damage;
    protected bool destroyCheck;
    public abstract void OnTriggerEnter(Collider other);

    // Start is called before the first frame update
    public virtual void Start()
    {
        destroyCheck = false;
        moveObj();
        Debug.Log("SimpleInteractiveObjects.Start()");
    }
    public virtual void moveObj(params object[] args)
    {
        speedMin = (float)args[0];
        speedMax = (float)args[1];
        GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(speedMin, speedMax);
        Debug.Log("SimpleInteractiveObjects.moveObj");
    }
    public virtual bool destroyObj(Collider other)
    {
        if (playerCheck(other) || BoltPowerUpCheck(other))
        {
            destroyCheck = true;
        }
        Debug.Log("SimpleInteractiveObjects.destroyObj");
        return destroyCheck;
    }
    public bool playerCheck(Collider other)
    {
        if (other.tag == "Player") // Game Over & Life utilization
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            return true;
        }
        return false;
    }
    public bool BoltPowerUpCheck(Collider other)
    {
        if ((other.tag == "Powerup1" && tag == "Bolt") ||
            (tag == "Powerup1" && other.tag == "Bolt") ||
            (other.tag == "Player" && tag == "Powerup1"))
        {
            Debug.Log(gameObject.name + "and" + other.gameObject.name);
            return true;
        }
        return false;
    }
}
