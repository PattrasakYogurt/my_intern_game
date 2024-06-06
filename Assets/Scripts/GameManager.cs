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
    public float timeRemaining;
    public bool isKeyObtainded = false;
    public int keyObtained = 0;
    public int pickCheck = 0;
    public bool isPickCheck = false;
    public GameObject keyStand;
    [SerializeField] private PlayerController playerController;
    

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
        
        if(Input.GetKeyUp(KeyCode.Escape))
        {            
           PauseGame();           
        }
        if(keyObtained >= 7 )
        {
            isKeyObtainded = true;
        }
        if(pickCheck >= 3 )
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
