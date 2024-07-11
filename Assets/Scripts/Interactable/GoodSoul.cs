using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSoul : MonoBehaviour , IInteractable
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioSource soulSound;
    public bool isUse = false;
    public ParticleSystem particle;

    public string GetInteractionText()
    {
        return "Press E to Heal";
    }

    public void Interact()
    {
        playerController.GetComponent<PlayerController>();
        if(isUse == false)
        {
           playerController.Heal(20);
            isUse = true;
            soulSound.Play();
        }
        ParticleSystem spawnParticle = Instantiate(particle, transform.position, Quaternion.identity);
        spawnParticle.Play();
        Destroy(spawnParticle.gameObject,spawnParticle.main.duration + spawnParticle.main.startLifetime.constantMax);
        Destroy(gameObject, 0.5f);
    }

}
