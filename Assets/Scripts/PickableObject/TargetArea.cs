using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public PickableObjects.ObjectType areaType;
    public ParticleSystem correct_Particle;
    public bool correctTik = false;
    void Start()
    {
        correct_Particle.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
       PickableObjects item = other.GetComponent<PickableObjects>(); 
        if(item == null)
        {
            return;
        }
        else
        {
            if(item.objectType == areaType)
            {
                correct_Particle.Play();
                correctTik = true;
                GameManager.instance.pickCheck++;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PickableObjects item = other.GetComponent<PickableObjects>();
        if (item == null)
        {
            return;
        }
        else
        {
            if (item.objectType == areaType)
            {
                correct_Particle.Stop();
            }
        }
    }
}