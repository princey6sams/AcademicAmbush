using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    public Boundary boundary;
    public GameObject shot;
    public AudioSource shotA, pwrA;
    public float speed, tilt, yaw, nextFire;
    public Transform shotSpawnC, shotSpawnL, shotSpawnR;
    public float gunCount = 0; //powerup
    public float fireRate = 1; //powerup combined w/ gunCount

    // Update is called once per frame
    void Update()
    {
        //If statement + Input.GetButton() for bomb
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            setGun();
            shotA.Play();
        }
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        if (GetComponent<Rigidbody>().velocity.z >= 0)
        {
            yaw = 0;
        }
        else
        {
            yaw = GetComponent<Rigidbody>().velocity.z * 0.2f * tilt;
        }

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup1"))
        {
            pwrA.Play();
        }
    }
    public void setGunCount()
    {
        gunCount += 1;
    }

    public void setFireRate()
    {
        if (gunCount == 0)
        {
            fireRate = 1.0f;
        }
        else if (gunCount == 1 || gunCount == 2)
        {
            fireRate = 0.5f;
        }
        else if (gunCount == 3 || gunCount == 4)
        {
            fireRate = 0.2f;
        }
        else if (gunCount >= 5)
        {
            fireRate *= 0.9f;
        }
    }

    void setGun()
    {
        if (gunCount == 0 || gunCount == 1)
        {
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation); // Center
        }
        else if (gunCount == 2 || gunCount == 3)
        {
            Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation); //Left
            Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation); //Right
        }
        else
        {
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation); //Center
            Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation); //Left
            Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation); //Right
        }
    }
}
