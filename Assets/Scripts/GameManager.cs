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
    [SerializeField] private int keyObtained = 0;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image keyShadow_Image;
    [SerializeField] private Image keyShadow_Image2;
    [SerializeField] private Image keyShadow_Image3;
    [SerializeField] private Image keyShadow_Image4;
    [SerializeField] private Image keyShadow_Image5;
    [SerializeField] private Image keyShadow_Image6;
    [SerializeField] private Image keyShadow_Image7;
    [SerializeField] private Image keyObtain_Image;
    [SerializeField] private Image keyObtain_Image2;
    [SerializeField] private Image keyObtain_Image3;
    [SerializeField] private Image keyObtain_Image4;
    [SerializeField] private Image keyObtain_Image5;
    [SerializeField] private Image keyObtain_Image6;
    [SerializeField] private Image keyObtain_Image7;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {            
                PauseGame();           
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
    private void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisablePlayer();
        //ShowPauseUI
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EnablePlayer();
    }
    private void PlayerLoose()
    {
        DisablePlayer();
    }
    private void PlayerWin()
    {
        DisablePlayer();
    }
}
