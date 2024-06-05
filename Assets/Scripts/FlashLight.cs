using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light light;
    public bool isTurnOn;

    void Start()
    {
        light.enabled = isTurnOn;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {

            isTurnOn = !isTurnOn;
            light.enabled = isTurnOn;
        }
    }
}
