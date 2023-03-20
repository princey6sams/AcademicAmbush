using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speedMin;
    public float speedMax;
    void Start()
    {
        if (tag == "Bolt")
        {
            speedMin = 35;
            speedMax = speedMin;
        }
        if (tag == "Enemy")
        {
            GetComponent<Rigidbody>().velocity = -(transform.forward) * Random.Range(speedMin, speedMax);
            return;
        }
        GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(speedMin, speedMax);
    }

    // public float[] getSpeed()
    // {
    //     float[] speed = new float[2];
    //     speed[0] = speedMin;
    //     speed[1] = speedMax;
    //     return speed;
    // }
    public void setSpeed(float multiplier)
    {
        this.speedMin *= multiplier;
        this.speedMax *= multiplier;
    }
}

