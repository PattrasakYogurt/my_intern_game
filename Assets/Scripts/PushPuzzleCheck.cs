using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPuzzleCheck : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject cubeCheck;
    public ParticleSystem correctParticles;
    
    // Start is called before the first frame update
    void Start()
    {
        puzzle.SetActive(true);
        correctParticles.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectForPush"))
        {
            puzzle.SetActive(false);
            correctParticles.Play();
            Destroy(cubeCheck, 2.1f);
        }
    }
}
