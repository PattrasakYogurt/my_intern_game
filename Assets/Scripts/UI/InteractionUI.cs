using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public TMP_Text interactionText;
    
    public void Show(string message)
    {
        interactionText.enabled = true;
        interactionText.text = message;
    }
    public void Hide()
    {
        interactionText.enabled = false;
    }
}
