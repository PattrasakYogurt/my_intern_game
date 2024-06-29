using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource keySound;
    
    public string GetInteractionText()
    {
        return "Press E to collect";
    }

    public void Interact()
    {
        GameManager.instance.keyObtained += 1;       
        keySound.Play();     
        Destroy(gameObject, 1f);
    }

}
