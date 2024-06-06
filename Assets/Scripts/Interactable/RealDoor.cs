using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RealDoor : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI needMoreKeyText;
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
            GameManager.instance.PlayerWin();
        }
    }
    IEnumerator FindMoreKey()
    {
        needMoreKeyText.enabled = true;
        yield return new WaitForSeconds(3f);
        needMoreKeyText.enabled = false;

    }
}
