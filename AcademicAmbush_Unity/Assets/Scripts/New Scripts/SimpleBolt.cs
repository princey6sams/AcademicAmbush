using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBolt : SimpleInteractiveObjects
{
    private float lifeTime;
    public override void Start()
    {
        speedMin = 20;
        speedMax = speedMin;
        lifeTime = 10;
        if (tag == "Bolt") { speedMin = 35; speedMax = speedMin; }
        if (tag == "BoltEnemy") { speedMin = -20; speedMax = speedMin; }
        if (tag == "BoltEnemy2") { speedMin = 20; speedMax = speedMin; }
        base.Start();
    }
    public override void moveObj(params object[] args)
    {
        base.moveObj(speedMin, speedMax);
    }
    public override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);

    }
    public void setSpeed(float multiplier)
    {
        if (tag == "Bolt")
        {
            this.speedMin *= multiplier;
            this.speedMax *= multiplier;
        }
    }

    public void Update()
    {
        if (!PlayerController.Instance)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}