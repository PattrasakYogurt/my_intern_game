using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winUI;
    public GameObject looseUI;
    public GameObject pauseUI;
    public TextMeshProUGUI timerText;
  //public TextMeshProUGUI timeRecordText;
    public float timeRemaining;
    public bool isKeyObtainded = false;
    public bool isKeyObtained_level2 = false;
    public bool isKeyObtained_level3 = false;
    public int keyObtained = 0;
    public int pickCheck = 0;
    public bool isPickCheck = false;
    public GameObject keyStand;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image keyImageShadow1;
    [SerializeField] private Image keyImage1;
    [SerializeField] private Image keyImageShadow2;
    [SerializeField] private Image keyImage2;
    [SerializeField] private Image keyImageShadow3;
    [SerializeField] private Image keyImage3;
    [SerializeField] private Image keyImageShadow4;
    [SerializeField] private Image keyImage4;
    [SerializeField] private Image keyImageShadow5;
    [SerializeField] private Image keyImage5;
    [SerializeField] private Image keyImageShadow6;
    [SerializeField] private Image keyImage6;
    [SerializeField] private Image keyImageShadow7;
    [SerializeField] private Image keyImage7;
    [SerializeField] private Image keyImageShadow8;
    [SerializeField] private Image keyImage8;
    [SerializeField] private Image keyImageShadow9;
    [SerializeField] private Image keyImage9;
    [SerializeField] private Image keyImageShadow10;
    [SerializeField] private Image keyImage10;
    [SerializeField] private Image keyImageShadow11;
    [SerializeField] private Image keyImage11;
    [SerializeField] private Image keyImageShadow12;
    [SerializeField] private Image keyImage12;
    [SerializeField] private Image keyImageShadow13;
    [SerializeField] private Image keyImage13;
    [SerializeField] private Image keyImageShadow14;
    [SerializeField] private Image keyImage14;
    [SerializeField] private Image keyImageShadow15;
    [SerializeField] private Image keyImage15;


    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }  
        if(keyObtained == 1 )
        {
            keyImageShadow1.enabled = false;
            keyImage1.enabled = true;
        }
        if (keyObtained == 2)
        {
            keyImageShadow2.enabled = false;
            keyImage2.enabled = true;
        }
        if (keyObtained == 3)
        {
            keyImageShadow3.enabled = false;
            keyImage3.enabled = true;
        }
        if (keyObtained == 4)
        {
            keyImageShadow4.enabled = false;
            keyImage4.enabled = true;
        }
        if (keyObtained == 5)
        {
            keyImageShadow5.enabled = false;
            keyImage5.enabled = true;
        }
        if (keyObtained == 6)
        {
            keyImageShadow6.enabled = false;
            keyImage6.enabled = true;
        }
        if (keyObtained == 7 )
        {
            keyImageShadow7.enabled = false;
            keyImage7.enabled = true;
            isKeyObtainded = true;
        }
        if (keyObtained == 8)
        {
            keyImageShadow8.enabled = false;
            keyImage8.enabled = true;
        }
        if (keyObtained == 9)
        {
            keyImageShadow9.enabled = false;
            keyImage9.enabled = true;
        }
        if (keyObtained == 10)
        {
            keyImageShadow10.enabled = false;
            keyImage10.enabled = true;
        }
        if (keyObtained == 11)
        {
            keyImageShadow11.enabled = false;
            keyImage11.enabled = true;
        }
        if (keyObtained == 12)
        {
            keyImageShadow12.enabled = false;
            keyImage12.enabled = true;
            isKeyObtained_level2 = true;
        }
        if (keyObtained == 13)
        {
            keyImageShadow13.enabled = false;
            keyImage13.enabled = true;
        }
        if (keyObtained == 14)
        {
            keyImageShadow14.enabled = false;
            keyImage14.enabled = true;
        }
        if (keyObtained == 15)
        {
            keyImageShadow15.enabled = false;
            keyImage15.enabled = true;
            isKeyObtained_level3 = true;
        }
        if (pickCheck >= 3 )
        {
            isPickCheck = true;
            keyStand.SetActive(false);
        }
        
    }
    public void DisablePlayer()
    {
        playerController.enabled = false;
    }
    public void EnablePlayer()
    {
        playerController.enabled = true;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisablePlayer();
        pauseUI.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EnablePlayer();
        pauseUI.SetActive(false);
    }
    public void PlayerLoose()
    {
        DisablePlayer();
        Time.timeScale = 0;
        looseUI.SetActive(true);
    }
    public void PlayerWin()
    {
        DisablePlayer();
        Time.timeScale = 0;
        winUI.SetActive(true);
    }
}
