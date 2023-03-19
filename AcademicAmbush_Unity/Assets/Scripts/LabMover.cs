using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMover : MonoBehaviour
{
    public float speedMin;
    public float speedMax;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = -(transform.forward) * Random.Range(speedMin, speedMax);
    }

}
