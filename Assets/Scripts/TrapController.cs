using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private AudioSource trapSound;
    [SerializeField]private float reducedSpeed = 2f; // The speed to reduce to when in the trap
    [SerializeField]private float normalSpeed = 12f; // The normal walking speed of the player
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = GetComponent<PlayerController>();
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
            PlayerController playerController = GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetSpeed(normalSpeed);
            }
        }
    }
}