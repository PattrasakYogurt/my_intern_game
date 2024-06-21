using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    //[SerializeField] private AudioSource trapSound;
    [SerializeField]private float reducedSpeed = 3f; // The speed to reduce to when in the trap
    [SerializeField]private float normalSpeed = 5f; // The normal walking speed of the player
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.SetSpeed(reducedSpeed);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetSpeed(normalSpeed);
            }
        }
    }
}
