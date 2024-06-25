using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RealDoorLevel3 : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI needMoreKeyText;   
    public string GetInteractionText()
    {
        return "Press E to open";
    }

    public void Interact()
    {
        if(GameManager.instance.isKeyObtained_level3 == false)
        {
            StartCoroutine(NeedMoreKeyText_Co());
        }
        if(GameManager.instance.isKeyObtained_level3 == true)
        {
            GameManager.instance.PlayerWin();
        }
    }

    IEnumerator NeedMoreKeyText_Co()
    {
        needMoreKeyText.enabled = true;
        yield return new WaitForSeconds(1f);
        needMoreKeyText.enabled = false;
    }
}
