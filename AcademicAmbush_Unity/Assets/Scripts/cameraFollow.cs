using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player)
        {
            tempPos = transform.position;
            if (player.position.x <= 38.5 && player.position.x >= -38.5) { tempPos.x = 0.7f * player.position.x; }
            if (player.position.z <= 0.75 && player.position.z >= -10) { tempPos.z = player.position.z + 6; }
            transform.position = tempPos;
        }
    }
}
