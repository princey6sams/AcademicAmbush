using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabController : MonoBehaviour
{
    public float fireRateL;
    public float nextFireL;
    public GameObject shotL;
    public Transform shotSpawnL;
    void Update()
    {
        if (Time.time > nextFireL)
        {
            nextFireL = Time.time + fireRateL;
            Instantiate(shotL, shotSpawnL.position, shotSpawnL.rotation);
            GetComponent<AudioSource>().Play();
        }
    }
}
