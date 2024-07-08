using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene(1);
    }
}
