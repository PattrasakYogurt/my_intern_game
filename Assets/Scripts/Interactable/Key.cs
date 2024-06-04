using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource keySound;
    [SerializeField] private Image keyImageShadow;
    [SerializeField] private Image keyImage;
    public string GetInteractionText()
    {
        return "Press E to collect";
    }

    public void Interact()
    {
        GameManager.instance.keyObtained += 1;       
        keySound.Play();
        keyImageShadow.enabled = false;
        keyImage.enabled = true;
        Destroy(gameObject, 0.5f);
    }

}
