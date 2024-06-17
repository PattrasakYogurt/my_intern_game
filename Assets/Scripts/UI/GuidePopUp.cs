using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePopUp : MonoBehaviour
{
    public GameObject guideImage;
    public PlayerController playerController;
    private float playerWalkSpeed = 7f;
    void Start()
    {
        guideImage.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            guideImage.SetActive(true); 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            GameManager.instance.DisablePlayer();
        }
    }
    public void CloseGuidePopUp()
    {
        guideImage.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        GameManager.instance.EnablePlayer();
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        playerController.SetSpeed(playerWalkSpeed);
    }
}
