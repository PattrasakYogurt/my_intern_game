using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public ParticleSystem slashParticleSystem;
    public void PlaySlashEffect(Vector3 position, Quaternion rotation)
    {
        ParticleSystem instance = Instantiate(slashParticleSystem, position, rotation);
        instance.Play();
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
