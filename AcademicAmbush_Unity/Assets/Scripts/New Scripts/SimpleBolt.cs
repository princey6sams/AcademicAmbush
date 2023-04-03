using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBolt : SimpleInteractiveObjects
{
    public override void Start()
    {
        if (tag == "Bolt") { speedMin = 35; speedMax = speedMin; }
        if (tag == "BoltEnemy") { speedMin = -20; speedMax = speedMin; }
        base.Start();
    }
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
}