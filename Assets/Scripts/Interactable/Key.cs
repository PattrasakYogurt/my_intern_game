using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource keySound;
    public ParticleSystem key_particle_keep;
    bool isKeep = false;

    void Start()
    {
        key_particle_keep.Stop();
    }
    public string GetInteractionText()
    {
        return "Press E to collect";
    }

    public void Interact()
    {
        if (isKeep == false)
        {
            isKeep = true;
            GameManager.instance.keyObtained += 1;
            keySound.Play();
            key_particle_keep.Play();
        }           
        Destroy(gameObject, 1f);
    }

}
