using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject settingPopUp;   
    public void ShowSettingPopUp()
    {
        settingPopUp.SetActive(true);
    }
    public void ClosePopUp()
    {
        settingPopUp.SetActive(false);    
    }
}
