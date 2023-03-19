using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speedMin;
    public float speedMax;
    void Start()
    {
        if (tag == "Enemy")
        {
            GetComponent<Rigidbody>().velocity = -(transform.forward) * Random.Range(speedMin, speedMax);
            return;
        }
        GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(speedMin, speedMax);
    }
}
