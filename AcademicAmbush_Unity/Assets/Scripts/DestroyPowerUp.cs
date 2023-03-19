// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DestroyPowerUp : MonoBehaviour
// {
//     public GameObject bulletExplosion;
//     private PlayerController playerController;

//     void Start()
//     {
//         GameObject playerControllerObject = GameObject.FindWithTag("Player");
//         if (playerControllerObject)
//         {
//             playerController = playerControllerObject.GetComponent<PlayerController>();
//         }
//     }
//     // void OnTriggerEnter(Collider other)
//     // {
//     //     if (tag == "Player" && other.tag == "Bolt")
//     //     {
//     //         Instantiate(bulletExplosion, other.transform.position, other.transform.rotation);
//     //         playerController.setGunCount();
//     //         playerController.setFireRate();
//     //         Destroy(other.gameObject);
//     //     }
//     // }
// }