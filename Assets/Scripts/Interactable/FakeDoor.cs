using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeDoor : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI findKeyMoreText;
    public TextMeshProUGUI fakeDoor;
    public string GetInteractionText()
    {
        return "Press E to open";
    }

    public void Interact()
    {
        if(GameManager.instance.isKeyObtainded == false)
        {
            StartCoroutine(ShowFindMoreText());           
        }
        if(GameManager.instance.isKeyObtainded == true)
        {
            StartCoroutine(ShowFakeDoorText());
        }
    }
    IEnumerator ShowFindMoreText()
    {
        
        findKeyMoreText.enabled = true;
        yield return new WaitForSeconds(3f);
        findKeyMoreText.enabled = false;

    }
    IEnumerator ShowFakeDoorText()
    {
        fakeDoor.enabled = true;
        yield return new WaitForSeconds(3f);
        fakeDoor.enabled = false;
    }
}
