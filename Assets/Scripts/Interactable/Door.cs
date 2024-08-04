using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Collider doorWingCollider;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private ParticleSystem doorParticles;
    private bool _isOpen;

    void Start()
    {
        doorParticles.Stop();
    }
    public string GetInteractionText()
    {
        if(_isOpen)
        {
            return "Close the door";
        }
        else
        {
            return "Press E to open";
        }
    }

    public void Interact()
    {
        _isOpen = !_isOpen;
        doorAnimator.SetBool("IsOpen", _isOpen);
        doorSound.Play();
        if(_isOpen)
        {
            doorParticles.Play();
        }
        StartCoroutine (DisableDoorCollider());
    }
    IEnumerator DisableDoorCollider()
    {
        Collider doorCollider = GetComponent<Collider> ();
        doorCollider.enabled = false;
        doorWingCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        doorCollider.enabled = true;
        doorWingCollider.enabled = true;
    }
}
