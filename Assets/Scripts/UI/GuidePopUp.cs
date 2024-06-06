using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePopUp : MonoBehaviour
{
    public Image guideImage;
    void Start()
    {
        guideImage.enabled = false; 
    }
    private void OnTriggerEnter(Collider other)
    {
        guideImage.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        guideImage.enabled = false;
    }
}
