using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (other.tag == "BoltEnemy2")
        {
            Destroy(other.gameObject);
        }
    }
}
