using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RealDoorLevel2 :MonoBehaviour, IInteractable
{
    public TextMeshProUGUI needMoreKeyText;
    public SetTimer setTimer;
    [SerializeField] private Collider doorWingCollider;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private GameObject keyArrange_Level2;
    [SerializeField] private GameObject keyArrange_Level3;
    private bool isOpen;
    public string GetInteractionText()
    {
        return "Press E to open";
    }

    public void Interact()
    {
        if (GameManager.instance.isKeyObtained_level2 == false)
        {
            StartCoroutine(FindMoreKey_Level2());
        }
        if (GameManager.instance.isKeyObtained_level2 == true)
        {
            setTimer.remainingTime += 200f;
            isOpen = !isOpen;
            doorAnimator.SetBool("IsOpen", isOpen);
            StartCoroutine(DisableColliderDoor());
            doorSound.Play();
            keyArrange_Level2.SetActive(false);
            keyArrange_Level3.SetActive(true);
        }
    }
    IEnumerator FindMoreKey_Level2()
    {
        needMoreKeyText.enabled = true;
        yield return new WaitForSeconds(3f);
        needMoreKeyText.enabled = false;
    }
    IEnumerator DisableColliderDoor()
    {
        Collider door = GetComponent<Collider>();
        door.enabled = false;
        doorWingCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        door.enabled = true;
        doorWingCollider.enabled = true;
    }
}
