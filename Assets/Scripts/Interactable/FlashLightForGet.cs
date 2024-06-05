using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightForGet : MonoBehaviour, IInteractable
{
    public GameObject realFlashLight;
    public string GetInteractionText()
    {
        return "Press E to keep";
    }

    public void Interact()
    {
        realFlashLight.SetActive(true);
        Destroy(gameObject);
    }

}
