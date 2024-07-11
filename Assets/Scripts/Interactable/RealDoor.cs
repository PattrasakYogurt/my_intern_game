using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RealDoor : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI needMoreKeyText;
    public SetTimer setTimer;
    [SerializeField] private Collider doorWingCollider;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private GameObject keyArrange_Level1;
    [SerializeField] private GameObject keyArrange_Level2;
    private bool isOpen;
    public string GetInteractionText()
    {
        return "Press E to open";
    }

    public void Interact()
    {
        if(GameManager.instance.isKeyObtainded == false)
        {
            StartCoroutine(FindMoreKey());
        }
        if(GameManager.instance.isKeyObtainded == true)
        {          
            setTimer.remainingTime += 150f;
            isOpen = !isOpen;
            doorAnimator.SetBool("IsOpen", isOpen);
            doorSound.Play();
            StartCoroutine(DisableColliderDoor());
            keyArrange_Level1.SetActive(false);
            keyArrange_Level2.SetActive(true);
        }
    }
    IEnumerator FindMoreKey()
    {
        needMoreKeyText.enabled = true;
        yield return new WaitForSeconds(3f);
        needMoreKeyText.enabled = false;
    }
    IEnumerator DisableColliderDoor()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        doorWingCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        collider.enabled = true;
        doorWingCollider.enabled = true;
    }
}
