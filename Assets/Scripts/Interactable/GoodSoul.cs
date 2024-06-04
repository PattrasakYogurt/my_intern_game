using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSoul : MonoBehaviour , IInteractable
{
    [SerializeField] private PlayerController playerController;
    //[SerializeField] private AudioSource soulSound;
    public string GetInteractionText()
    {
        return "Press E to get";
    }

    public void Interact()
    {
        playerController.GetComponent<PlayerController>();
        playerController.Heal(20);
        //soulSound.Play();
        Destroy(gameObject, 0.5f);
    }

}
