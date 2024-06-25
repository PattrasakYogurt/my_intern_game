using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        } 
       else if (remainingTime <= 0)
        {
            timerText.color = Color.red;
            GameManager.instance.PlayerLoose();
        }
       int minutes = Mathf.FloorToInt(remainingTime / 60);
       int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
