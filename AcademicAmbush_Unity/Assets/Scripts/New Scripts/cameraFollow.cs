using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS;

public class cameraFollow : MonoBehaviour
{
    private Vector3 tempPos;

    void LateUpdate()
    {
        if (PlayerController.Instance)
        {
            tempPos = transform.position;
            if (PlayerController.Instance.transform.position.x <= 40 && PlayerController.Instance.transform.position.x >= -40) { tempPos.x = 0.7f * PlayerController.Instance.transform.position.x; }
            if (PlayerController.Instance.transform.position.z <= -2 && PlayerController.Instance.transform.position.z >= -24.5) { tempPos.z = PlayerController.Instance.transform.position.z + 6; }
            transform.position = tempPos;
        }
    }
}